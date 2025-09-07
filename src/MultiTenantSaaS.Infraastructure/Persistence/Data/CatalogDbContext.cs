using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Infraastructure.Persistence.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants => Set<Tenant>();
    }
}
