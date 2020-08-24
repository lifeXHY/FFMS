using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class update_table2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BasUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "BasUser",
                type: "char(1)",
                maxLength: 1,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "BasUser");
        }
    }
}
