using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Infraastructure.Persistence;
using MultiTenantSaaS.Infraastructure.Persistence.Data;
using MultiTenantSaaS.Infraastructure.Provisioning;

namespace MultiTenantSaaS.Infraastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("CatalogDb")));

            services.AddScoped<ITenantAccessor, TenantAccessor>();
            services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();
            services.AddScoped<ITenantProvisioningService, TenantProvisioningService>();
            services.AddScoped<IRepository<Tenant>, TenantRepository>();
            return services;
        }
    }
}
