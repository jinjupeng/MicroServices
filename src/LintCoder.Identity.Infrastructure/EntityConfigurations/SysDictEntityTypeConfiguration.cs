using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysDictEntityTypeConfiguration : IEntityTypeConfiguration<SysDict>
    {
        public void Configure(EntityTypeBuilder<SysDict> builder)
        {
            builder.ToTable(nameof(SysDict));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
