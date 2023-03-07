using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxdoctypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "KeyListxDocType",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColoumnNum",
                table: "KeyListxDocType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "KeyListxDocType");

            migrationBuilder.DropColumn(
                name: "ColoumnNum",
                table: "KeyListxDocType");
        }
    }
}
