using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantAccessor
    {
        public Tenant? Current
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
