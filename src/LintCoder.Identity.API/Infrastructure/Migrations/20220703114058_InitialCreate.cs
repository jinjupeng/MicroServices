using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LintCoder.Identity.API.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysApi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiPid = table.Column<long>(type: "bigint", nullable: false),
                    ApiPids = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLeaf = table.Column<bool>(type: "bit", nullable: false),
                    ApiName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParamName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParamKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParamValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ParamDesc = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysDict",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ItemValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ItemDesc = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuPid = table.Column<long>(type: "bigint", nullable: false),
                    MenuPids = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsLeaf = table.Column<bool>(type: "bit", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysOrg",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgPid = table.Column<long>(type: "bigint", nullable: false),
                    OrgPids = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsLeaf = table.Column<bool>(type: "bit", nullable: false),
                    OrgName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysOrg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RoleDesc = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Sort = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleApi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ApiId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleApi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleMenu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Portrait = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrgId = table.Column<long>(type: "bigint", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUserRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysApi");

            migrationBuilder.DropTable(
                name: "SysConfig");

            migrationBuilder.DropTable(
                name: "SysDict");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysOrg");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysRoleApi");

            migrationBuilder.DropTable(
                name: "SysRoleMenu");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysUserRole");
        }
    }
}
