using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Infrastructure.Persistence.Data;

namespace MultiTenantSaaS.Infraastructure.Persistence
{
    internal class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly ITenantAccessor _tenantAccessor;
        private readonly IConfiguration _config;

        public TenantDbContextFactory(ITenantAccessor tenantAccessor, IConfiguration config)
        {
            _tenantAccessor = tenantAccessor;
            _config = config;
        }

        public Task<ITenantDataContext> CreateDbContextAsync(CancellationToken cancellationToken = default)
        {
            var tenant = _tenantAccessor.Current ?? throw new InvalidOperationException("Tenant not resolved.");
            var sqlServer = _config["Azure:SqlServerName"]!;
            var user = _config["Azure:SqlAdminUser"]!;
            var pwd = _config["Azure:SqlAdminPassword"]!;

            var conn = $"Server=tcp:{sqlServer}.database.windows.net,1433;" +
                       $"Database={tenant.DatabaseName};User Id={user};Password={pwd};Encrypt=True;";

            var options = new DbContextOptionsBuilder<TenantDbContext>()
                .UseSqlServer(conn)
                .Options;

            ITenantDataContext context = new TenantDbContext(options);
            return Task.FromResult(context);
        }
    }
}
