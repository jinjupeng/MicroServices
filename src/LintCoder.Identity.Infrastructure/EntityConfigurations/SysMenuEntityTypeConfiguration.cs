using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysMenuEntityTypeConfiguration : IEntityTypeConfiguration<SysMenu>
    {
        private readonly ICurrentUser currentUser;
        public SysMenuEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable(nameof(SysMenu));

            builder.HasKey(user => user.Id);
            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
