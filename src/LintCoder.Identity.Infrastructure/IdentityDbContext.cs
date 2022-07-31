using LintCoder.Application.Users;
using LintCoder.Base;
using LintCoder.Identity.Domain.Entities;
using LintCoder.Identity.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.Infrastructure
{
    public class IdentityDbContext : BaseDbContext
    {
        public IdentityDbContext(DbContextOptions options, UserContext userContext) : base(options, userContext)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SysApiEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysConfigEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysDictEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysMenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysOrgEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysRoleApiEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysRoleMenuEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SysUserRoleEntityTypeConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
    }
}
