using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Infraastructure.Persistence
{
    public class TenantAccessor : ITenantAccessor
    {
        public Tenant? Current { get; set; }
    }
}
