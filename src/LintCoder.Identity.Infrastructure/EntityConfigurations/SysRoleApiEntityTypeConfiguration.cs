using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysRoleApiEntityTypeConfiguration : IEntityTypeConfiguration<SysRoleApi>
    {
        public void Configure(EntityTypeBuilder<SysRoleApi> builder)
        {
            builder.ToTable(nameof(SysRoleApi));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
