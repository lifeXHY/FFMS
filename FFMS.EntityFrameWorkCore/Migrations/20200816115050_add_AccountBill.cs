using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class add_AccountBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountBill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    AccountMoney = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AccountDetails = table.Column<string>(maxLength: 500, nullable: false),
                    AccountDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBill", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBill");
        }
    }
}
