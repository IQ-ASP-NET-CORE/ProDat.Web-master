using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxdoctypeUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxDocType_KeyListId",
                table: "KeyListxDocType");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxDocType_KeyListId_DocTypeId",
                table: "KeyListxDocType",
                columns: new[] { "KeyListId", "DocTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KeyListxDocType_KeyListId_DocTypeId",
                table: "KeyListxDocType");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxDocType_KeyListId",
                table: "KeyListxDocType",
                column: "KeyListId");
        }
    }
}
