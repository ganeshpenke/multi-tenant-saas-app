namespace MultiTenantSaaS.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; private set; }
        public string TenantName { get; private set; }
        public string Status { get; private set; }
        public string? DatabaseName { get; private set; }

        private Tenant() { }

        public Tenant(string name)
        {
            Id = Guid.NewGuid();
            TenantName = name;
            Status = "Provisioning";
        }

        public void MarkAsActive(string dbName)
        {
            DatabaseName = dbName;
            Status = "Active";
        }

        public void MarkAsFailed() => Status = "Failed";
        public void MarkAsProvisioned() => Status = "Provisioned";
        public void SetDatabaseName(string dbName) => DatabaseName = dbName;
    }
}
