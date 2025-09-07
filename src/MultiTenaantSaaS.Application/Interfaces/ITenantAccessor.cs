using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantAccessor
    {
        Tenant? Current { get; set; }
    }
}
