using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class update_table_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateDisPlayName",
                table: "AccountBill",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateUserID",
                table: "AccountBill",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDisPlayName",
                table: "AccountBill");

            migrationBuilder.DropColumn(
                name: "CreateUserID",
                table: "AccountBill");
        }
    }
}
