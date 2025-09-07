using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantProvisioningService
    {
        Task ProvisionAsync(Tenant tenant);
    }
}
