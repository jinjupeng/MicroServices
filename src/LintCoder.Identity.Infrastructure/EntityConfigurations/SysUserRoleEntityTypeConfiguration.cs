using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<SysUserRole>
    {
        public void Configure(EntityTypeBuilder<SysUserRole> builder)
        {
            builder.ToTable(nameof(SysUserRole));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
