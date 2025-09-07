using MediatR;
using MultiTenantSaaS.Application.DTOs;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Tenants.Commands
{
    public record CreateTenantCommand(CreateTenantDto Dto) : IRequest<Tenant>;
}
