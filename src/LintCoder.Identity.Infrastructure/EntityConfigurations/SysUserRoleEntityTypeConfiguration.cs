using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<SysUserRole>
    {
        private readonly ICurrentUser currentUser;
        public SysUserRoleEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysUserRole> builder)
        {
            builder.ToTable(nameof(SysUserRole));

            builder.HasKey(user => user.Id);

            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
