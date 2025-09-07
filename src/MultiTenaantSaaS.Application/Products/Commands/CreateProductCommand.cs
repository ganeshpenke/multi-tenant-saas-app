using MediatR;

namespace MultiTenantSaaS.Application.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;
}
