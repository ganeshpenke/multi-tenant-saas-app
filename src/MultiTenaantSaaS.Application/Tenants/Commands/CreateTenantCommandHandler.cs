using MediatR;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Tenants.Commands
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Tenant>
    {
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly ITenantProvisioningService _provisioning;
        public CreateTenantCommandHandler(IRepository<Tenant> tenantRepository, ITenantProvisioningService provisioning)
        {
            _tenantRepository = tenantRepository;
            _provisioning = provisioning;
        }

        public async Task<Tenant> Handle(CreateTenantCommand command, CancellationToken cancellationToken)
        {
            var tenant = new Tenant(command.Dto.TenantName);

            await _tenantRepository.AddAsync(tenant, cancellationToken);

            try
            {
                await _provisioning.ProvisionAsync(tenant);
                tenant.MarkAsActive(tenant.DatabaseName!);
            }
            catch
            {
                tenant.MarkAsFailed();
            }

            await _tenantRepository.UpdateAsync(tenant, cancellationToken);

            return tenant;
        }
    }
}
