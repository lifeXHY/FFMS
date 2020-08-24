using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class add_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoLog");

            migrationBuilder.CreateTable(
                name: "BasUser",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PassWord = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    DisPlayName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IfAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasUser", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasUser");

            migrationBuilder.CreateTable(
                name: "InfoLog",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 1000, nullable: true),
                    DTCerate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoLog", x => x.ID);
                });
        }
    }
}
