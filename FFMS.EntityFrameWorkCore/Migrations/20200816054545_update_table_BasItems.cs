using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class update_table_BasItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateDisPlayName",
                table: "BasItems",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateUserID",
                table: "BasItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDisPlayName",
                table: "BasItems");

            migrationBuilder.DropColumn(
                name: "CreateUserID",
                table: "BasItems");
        }
    }
}
