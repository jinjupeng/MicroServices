using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LintCoder.Identity.API.Infrastructure.Migrations
{
    public partial class AddTenantInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TennantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreatedName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    ModifiedName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantInfo_Id",
                table: "TenantInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantInfo");
        }
    }
}
