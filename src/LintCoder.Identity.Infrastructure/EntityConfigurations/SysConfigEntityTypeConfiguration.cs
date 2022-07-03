using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysConfigEntityTypeConfiguration : IEntityTypeConfiguration<SysConfig>
    {
        public void Configure(EntityTypeBuilder<SysConfig> builder)
        {
            builder.ToTable(nameof(SysConfig));

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Id).ValueGeneratedOnAdd();
        }
    }
}
