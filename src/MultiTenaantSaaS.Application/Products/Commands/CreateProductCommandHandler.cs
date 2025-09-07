using MediatR;
using MultiTenantSaaS.Application.Interfaces;
using MultiTenantSaaS.Domain.Entities;

namespace MultiTenantSaaS.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly ITenantDbContextFactory _factory;

        public CreateProductCommandHandler(ITenantDbContextFactory factory)
        {
            _factory = factory;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var ctx = await _factory.CreateDbContextAsync(cancellationToken);

            var product = new Product(request.Name, request.Price);
            ctx.AddProduct(product);

            await ctx.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
