using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantSaaS.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));
            return services;
        }
    }
}
