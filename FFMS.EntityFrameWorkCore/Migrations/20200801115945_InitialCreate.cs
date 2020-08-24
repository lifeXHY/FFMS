using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 1000, nullable: true),
                    DTCerate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoLog", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoLog");
        }
    }
}
