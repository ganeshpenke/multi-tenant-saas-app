using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MultiTenantSaaS.Infrastructure.Persistence.Data
{
    public class TenantDbContextDesignFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

            // Provide a default connection string for design-time
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TenantDb_DesignTime;Trusted_Connection=True;", b => b.MigrationsAssembly("MultiTenantSaaS.Infrastructure"));

            return new TenantDbContext(optionsBuilder.Options);
        }
    }
}
