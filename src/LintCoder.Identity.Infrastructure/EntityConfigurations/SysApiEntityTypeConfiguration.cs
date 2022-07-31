using Finbuckle.MultiTenant.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysApiEntityTypeConfiguration : IEntityTypeConfiguration<SysApi>
    {
        public void Configure(EntityTypeBuilder<SysApi> builder)
        {
            builder.ToTable(nameof(SysApi));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.IsMultiTenant();
        }
    }
}
