using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LintCoder.Identity.Infrastructure.EntityConfigurations
{
    public class SysApiEntityTypeConfiguration : IEntityTypeConfiguration<SysApi>
    {
        private readonly ICurrentUser currentUser;
        public SysApiEntityTypeConfiguration(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        public void Configure(EntityTypeBuilder<SysApi> builder)
        {
            builder.ToTable(nameof(SysApi));

            builder.HasKey(user => user.Id);

            builder.HasIndex(user => new { user.Id, user.TenantId });

            builder.HasQueryFilter(x => x.TenantId == currentUser.TenantId);
        }
    }
}
