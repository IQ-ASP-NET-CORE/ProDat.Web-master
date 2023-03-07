using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxengdatacodeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxEngDataCode_KeyListId",
                table: "KeyListxEngDataCode");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngDataCode_KeyListId_EngDataCode",
                table: "KeyListxEngDataCode",
                columns: new[] { "KeyListId", "EngDataCode" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxEngDataCode_KeyListId_EngDataCode",
                table: "KeyListxEngDataCode");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngDataCode_KeyListId",
                table: "KeyListxEngDataCode",
                column: "KeyListId");
        }
    }
}
