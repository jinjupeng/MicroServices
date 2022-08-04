using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    internal class SysOrgEntityTypeConfiguration : IEntityTypeConfiguration<SysOrg>
    {
        private readonly ICurrentUser currentUser;
        public SysOrgEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Configure(EntityTypeBuilder<SysOrg> builder)
        {
            builder.ToTable(nameof(SysOrg));

            builder.HasKey(user => user.Id);
            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
