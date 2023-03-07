using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxengclassUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxEngClass_KeyListId",
                table: "KeyListxEngClass");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngClass_KeyListId_EngClassID",
                table: "KeyListxEngClass",
                columns: new[] { "KeyListId", "EngClassID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxEngClass_KeyListId_EngClassID",
                table: "KeyListxEngClass");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxEngClass_KeyListId",
                table: "KeyListxEngClass",
                column: "KeyListId");
        }
    }
}
