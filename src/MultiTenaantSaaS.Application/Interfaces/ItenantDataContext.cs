using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantDataContext
    {
        IQueryable<Product> Products { get; }
        void AddProduct(Product product);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
