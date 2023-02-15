using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class engclassreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs");

            migrationBuilder.CreateIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs",
                column: "EngClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs");

            migrationBuilder.CreateIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs",
                column: "EngClassId",
                unique: true);
        }
    }
}
