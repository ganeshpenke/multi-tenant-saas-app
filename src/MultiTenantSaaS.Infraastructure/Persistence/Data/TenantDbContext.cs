using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Infrastructure.Persistence.Data
{
    public class TenantDbContext : DbContext, ITenantDataContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        IQueryable<Product> ITenantDataContext.Products => Products;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
