using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetExplorer.Migrations
{
    public partial class AddedIsScrapped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsScrapped",
                table: "Assets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsScrapped",
                table: "Assets");
        }
    }
}
