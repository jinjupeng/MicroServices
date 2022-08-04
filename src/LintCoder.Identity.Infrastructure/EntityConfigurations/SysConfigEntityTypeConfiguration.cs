using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysConfigEntityTypeConfiguration : IEntityTypeConfiguration<SysConfig>
    {
        private readonly ICurrentUser currentUser;
        public SysConfigEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysConfig> builder)
        {
            builder.ToTable(nameof(SysConfig));

            builder.HasKey(user => user.Id);
            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
