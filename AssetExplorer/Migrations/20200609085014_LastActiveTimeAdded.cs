using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetExplorer.Migrations
{
    public partial class LastActiveTimeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveTime",
                table: "Assets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActiveTime",
                table: "Assets");
        }
    }
}
