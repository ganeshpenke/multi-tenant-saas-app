using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantDataContext
    {
        IQueryable<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
