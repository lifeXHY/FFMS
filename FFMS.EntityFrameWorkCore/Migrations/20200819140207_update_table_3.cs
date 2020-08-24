using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class update_table_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillType",
                table: "AccountBill",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "AccountBill",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillType",
                table: "AccountBill");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "AccountBill");
        }
    }
}
