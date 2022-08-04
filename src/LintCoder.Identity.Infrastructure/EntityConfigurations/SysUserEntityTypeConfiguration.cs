using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysUserEntityTypeConfiguration : IEntityTypeConfiguration<SysUser>
    {
        private readonly ICurrentUser currentUser;
        public SysUserEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable(nameof(SysUser));

            builder.HasKey(user => user.Id);
            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
