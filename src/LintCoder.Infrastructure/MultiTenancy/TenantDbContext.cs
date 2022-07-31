using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Infrastructure.MultiTenancy
{
    public class TenantDbContext : EFCoreStoreDbContext<TenantEntity>
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TenantEntity>().ToTable("Tenants");
        }
    }
}
