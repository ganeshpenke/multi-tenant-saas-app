namespace MultiTenantSaaS.Application.Interfaces
{
    public interface ITenantDbContextFactory
    {
        Task<ITenantDataContext> CreateDbContextAsync(CancellationToken cancellationToken = default);
    }
}
