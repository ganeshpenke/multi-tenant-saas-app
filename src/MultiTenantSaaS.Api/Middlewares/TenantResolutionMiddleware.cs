using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Api.Middlewares
{
    public class TenantResolutionMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantResolutionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(
        HttpContext context,
        ITenantAccessor accessor,
        IRepository<Tenant> catalogRepo)
        {
            // Example: tenant ID from header (later can swap to claim or subdomain)
            var tenantIdHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

            if (Guid.TryParse(tenantIdHeader, out var tenantId))
            {
                var tenant = await catalogRepo.GetByIdAsync(tenantId);
                if (tenant != null)
                {
                    accessor.Current = tenant;
                }
            }

            await _next(context);
        }
    }
}
