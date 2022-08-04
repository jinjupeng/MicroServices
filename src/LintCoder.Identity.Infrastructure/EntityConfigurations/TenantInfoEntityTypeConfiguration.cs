using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class TenantInfoEntityTypeConfiguration : IEntityTypeConfiguration<TenantInfo>
    {
        public void Configure(EntityTypeBuilder<TenantInfo> builder)
        {
            builder.ToTable(nameof(TenantInfo));

            builder.HasKey(user => user.Id);

            builder.HasIndex(user => new { user.Id });
        }
    }
}
