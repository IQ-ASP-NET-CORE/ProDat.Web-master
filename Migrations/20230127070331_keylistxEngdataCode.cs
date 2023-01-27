using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxEngdataCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "KeyListxEngDataCode",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColumnNumber",
                table: "KeyListxEngDataCode",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "KeyListxEngDataCode");

            migrationBuilder.DropColumn(
                name: "ColumnNumber",
                table: "KeyListxEngDataCode");
        }
    }
}
