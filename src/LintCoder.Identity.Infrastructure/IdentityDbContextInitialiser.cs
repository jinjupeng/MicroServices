using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.Infrastructure
{
    public class IdentityDbContextInitialiser
    {
        private readonly IdentityDbContext _context;
        private readonly ILogger<IdentityDbContextInitialiser> _logger;

        public IdentityDbContextInitialiser(IdentityDbContext context, ILogger<IdentityDbContextInitialiser> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            var tenantId = Guid.NewGuid().ToString();
            var createdBy = "1297873308628307970";
            var createdName = "admin";
            if (!_context.SysUser.Any())
            {
                _context.TenantInfo.Add(new TenantInfo
                {
                    TennantName = "lintcoder",
                    IsActive = true,
                    Id = tenantId,
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });
                _context.SysUser.Add(new SysUser
                {
                    Id = "1297873308628307970",
                    TenantId = tenantId,
                    OrgId = "1",
                    UserName = "admin",
                    Phone = "123456789",
                    Enabled = true,
                    NickName = "admin",
                    Portrait = "",
                    Password = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                    Email = "test@test.com",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });

                _context.SysUserRole.Add(new SysUserRole
                {
                    TenantId = tenantId,
                    RoleId = "1298061556168273921",
                    UserId = "1297873308628307970",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });

                _context.SysRole.AddRange(new SysRole
                {
                    TenantId = tenantId,
                    Id = "1298061556168273921",
                    RoleName = "管理员",
                    RoleDesc = "系统管理员",
                    RoleCode = "admin",
                    Sort = 1,
                    CreatedBy = createdBy,
                    CreatedName = createdName
                },
                new SysRole
                {
                    TenantId = tenantId,
                    Id = "1298063367197437954",
                    RoleName = "普通用户",
                    RoleDesc = "普通用户",
                    RoleCode = "普通用户",
                    Sort = 2,
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });

                _context.SysMenu.AddRange(new SysMenu
                {
                    Id = "1",
                    TenantId = tenantId,
                    MenuPid = "0",
                    MenuPids = "[0]",
                    IsLeaf = false,
                    MenuName = "系统根目录",
                    Url = "/",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "2",
                    TenantId = tenantId,
                    MenuPid = "1",
                    MenuPids = "[0],[1]",
                    IsLeaf = false,
                    MenuName = "系统管理",
                    Url = "/system",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "3",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "用户管理",
                    Url = "/home/sysuser",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "4",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "角色管理",
                    Url = "/home/sysrole",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "5",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "组织管理",
                    Url = "/home/sysorg",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "6",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf= true,
                    MenuName = "菜单管理",
                    Url = "/home/sysmenu",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "7",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "接口管理",
                    Url = "/home/sysapi",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "10",
                    TenantId = tenantId,
                    MenuPid = "1",
                    MenuPids = "[0],[1]",
                    IsLeaf = false,
                    MenuName = "测试用菜单",
                    Url = "/order",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "11",
                    TenantId = tenantId,
                    MenuPid = "10",
                    MenuPids = "[0],[1],[10]",
                    IsLeaf = true,
                    MenuName = "子菜单(首页)",
                    Url = "/home/firstpage",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "12",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "参数配置",
                    Url = "/home/sysconfig",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName

                }, new SysMenu
                {
                    Id = "13",
                    TenantId = tenantId,
                    MenuPid = "2",
                    MenuPids = "[0],[1],[2]",
                    IsLeaf = true,
                    MenuName = "数据字典",
                    Url = "/home/sysdict",
                    Icon = "",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });

                _context.SysRoleMenu.AddRange(new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "1",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "2",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "3",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "4",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "5",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "6",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "7",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "10",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "11",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "12",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                }, new SysRoleMenu
                {
                    TenantId = tenantId,
                    RoleId = "1298063367197437954",
                    MenuId = "13",
                    CreatedBy = createdBy,
                    CreatedName = createdName
                });
            }
            // SysRole,Role,.....

            await _context.SaveChangesAsync();
        }
    }
}
