using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;
using MultiTenantSaaS.Infraastructure.Persistence.Data;

namespace MultiTenantSaaS.Infraastructure.Persistence
{
    internal class TenantRepository : IRepository<Tenant>
    {
        private readonly CatalogDbContext _context;

        public TenantRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {
            await _context.Tenants.AddAsync(tenant, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Tenant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Tenant tenant, CancellationToken cancellationToken = default)
        {
            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
