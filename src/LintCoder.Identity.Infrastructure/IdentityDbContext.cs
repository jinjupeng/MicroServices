using LintCoder.Application.Common.Interfaces;
using LintCoder.Identity.Domain.Entities;
using LintCoder.Identity.Infrastructure.EntityConfigurations;
using LintCoder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.Infrastructure
{
    public class IdentityDbContext : BaseDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ICurrentUser currentUser) : base(options, currentUser)
        {
        }

        public DbSet<SysApi> SysApi { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }

        public DbSet<SysDict> SysDict { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<SysOrg> SysOrg { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysRoleApi> SysRoleApi { get; set; }
        public DbSet<SysRoleMenu> SysRoleMenu { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysUserRole> SysUserRole { get; set; }

        public DbSet<TenantInfo> TenantInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SysApiEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysConfigEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysDictEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysMenuEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysOrgEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysRoleApiEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysRoleEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysRoleMenuEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysUserEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new SysUserRoleEntityTypeConfiguration(currentUser));
            modelBuilder.ApplyConfiguration(new TenantInfoEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
