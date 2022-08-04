using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysRoleApiEntityTypeConfiguration : IEntityTypeConfiguration<SysRoleApi>
    {
        private readonly ICurrentUser currentUser;
        public SysRoleApiEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysRoleApi> builder)
        {
            builder.ToTable(nameof(SysRoleApi));

            builder.HasKey(user => user.Id);
            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
