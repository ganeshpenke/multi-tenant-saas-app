using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Sql;
using Azure.ResourceManager.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Infrastructure.Persistence.Data;

namespace MultiTenantSaaS.Infraastructure.Provisioning
{
    internal class TenantProvisioningService : ITenantProvisioningService
    {
        private readonly IConfiguration _config;

        public TenantProvisioningService(IConfiguration config) => _config = config;

        public async Task ProvisionAsync(Tenant tenant)
        {
            string subscriptionId = _config["Azure:SubscriptionId"]!;
            string resourceGroupName = _config["Azure:ResourceGroup"]!;
            string sqlServerName = _config["Azure:SqlServerName"]!;
            string location = _config["Azure:Location"]!;

            // 1. Create unique DB name
            var databaseName = $"tenant_{tenant.Id:N}";
            tenant.SetDatabaseName(databaseName);

            // Authenticate with Managed Identity or local dev credentials
            var armClient = new ArmClient(new DefaultAzureCredential());

            ResourceGroupResource rg = armClient.GetResourceGroupResource(
                ResourceGroupResource.CreateResourceIdentifier(subscriptionId, resourceGroupName)
            );
            // Get subscription + resource group
            var subscription = armClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{subscriptionId}"));
            var rg1 = subscription.GetResourceGroup(resourceGroupName);

            // Get the SQL server resource
            var sqlServers = rg.GetSqlServers();

            SqlServerResource sqlServer = await sqlServers.GetAsync(sqlServerName);

            // Create DB definition
            var dbData = new SqlDatabaseData(location)
            {
                Sku = new SqlSku(name: "Basic")
                {
                    Tier = "Basic"
                }
            };

            // Start create DB
            Console.WriteLine($"Provisioning DB {tenant.DatabaseName}...");
            var dbLro = await sqlServer.GetSqlDatabases().CreateOrUpdateAsync(
                WaitUntil.Completed,
                tenant.DatabaseName,
                dbData
            );

            var db = dbLro.Value;
            Console.WriteLine($"DB {db.Data.Name} provisioned.");

            // In real-world: create contained SQL user or configure AAD user for this DB
            // For now, tenant.Status is just updated
            tenant.MarkAsActive(databaseName);

            // Run EF migrations (simplified)
            var connString = $"Server=tcp:{sqlServerName}.database.windows.net,1433;" +
                             $"Database={tenant.DatabaseName};" +
                             $"User Id=sql;Password=Ganesh@123;Encrypt=True;";

            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseSqlServer(connString)
                .Options;

            using var dbContext = new TenantDbContext(options);
            await dbContext.Database.MigrateAsync();
        }
    }
}
