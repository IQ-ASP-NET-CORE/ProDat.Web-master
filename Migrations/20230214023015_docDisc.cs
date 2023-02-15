using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class docDisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocId",
                table: "EngDisc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "docDisc",
                table: "EngDisc",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocDisc",
                table: "Doc",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngDisc_DocId",
                table: "EngDisc",
                column: "DocId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngDisc_Doc_DocId",
                table: "EngDisc",
                column: "DocId",
                principalTable: "Doc",
                principalColumn: "DocID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngDisc_Doc_DocId",
                table: "EngDisc");

            migrationBuilder.DropIndex(
                name: "IX_EngDisc_DocId",
                table: "EngDisc");

            migrationBuilder.DropColumn(
                name: "DocId",
                table: "EngDisc");

            migrationBuilder.DropColumn(
                name: "docDisc",
                table: "EngDisc");

            migrationBuilder.DropColumn(
                name: "DocDisc",
                table: "Doc");
        }
    }
}
