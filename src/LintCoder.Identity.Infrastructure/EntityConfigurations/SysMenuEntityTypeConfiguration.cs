using Finbuckle.MultiTenant.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysMenuEntityTypeConfiguration : IEntityTypeConfiguration<SysMenu>
    {
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable(nameof(SysMenu));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.IsMultiTenant();
        }
    }
}
