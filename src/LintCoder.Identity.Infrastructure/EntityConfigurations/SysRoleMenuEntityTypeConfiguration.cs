using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysRoleMenuEntityTypeConfiguration : IEntityTypeConfiguration<SysRoleMenu>
    {
        public void Configure(EntityTypeBuilder<SysRoleMenu> builder)
        {
            builder.ToTable(nameof(SysRoleMenu));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
