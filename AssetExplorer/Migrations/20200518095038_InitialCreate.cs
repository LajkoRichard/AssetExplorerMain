using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetExplorer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceType = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    MAC = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Knox = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    Output = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    Repair = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
