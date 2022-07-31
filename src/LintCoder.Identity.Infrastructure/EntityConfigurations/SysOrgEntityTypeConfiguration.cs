using Finbuckle.MultiTenant.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysOrgEntityTypeConfiguration : IEntityTypeConfiguration<SysOrg>
    {
        public void Configure(EntityTypeBuilder<SysOrg> builder)
        {
            builder.ToTable(nameof(SysOrg));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.IsMultiTenant();
        }
    }
}
