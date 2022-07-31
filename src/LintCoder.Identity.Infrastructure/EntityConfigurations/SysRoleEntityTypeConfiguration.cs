using Finbuckle.MultiTenant.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysRoleEntityTypeConfiguration : IEntityTypeConfiguration<SysRole>
    {
        public void Configure(EntityTypeBuilder<SysRole> builder)
        {
            builder.ToTable(nameof(SysRole));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.IsMultiTenant();
        }
    }
}
