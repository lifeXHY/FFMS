using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FFMS.EntityFrameWorkCore.Migrations
{
    public partial class update_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BasUser",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "BasUser");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasUser",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "BasUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "BasUser",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "BasUser",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "BasUser",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasUser",
                table: "BasUser",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BasUser",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "BasUser");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "BasUser");

            migrationBuilder.AddColumn<long>(
                name: "UserID",
                table: "BasUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "BasUser",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "BasUser",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "BasUser",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "BasUser",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasUser",
                table: "BasUser",
                column: "UserID");
        }
    }
}
