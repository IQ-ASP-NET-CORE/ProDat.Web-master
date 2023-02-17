using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class keylistxdoctype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyListxDocType",
                columns: table => new
                {
                    KeyListxDocTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyListId = table.Column<int>(nullable: false),
                    DocTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyListxDocType", x => x.KeyListxDocTypeID);
                    table.ForeignKey(
                        name: "FK_KeyListxDocType_DocType_DocTypeId",
                        column: x => x.DocTypeId,
                        principalTable: "DocType",
                        principalColumn: "DocTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyListxDocType_KeyList_KeyListId",
                        column: x => x.KeyListId,
                        principalTable: "KeyList",
                        principalColumn: "KeyListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxDocType_DocTypeId",
                table: "KeyListxDocType",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyListxDocType_KeyListId",
                table: "KeyListxDocType",
                column: "KeyListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyListxDocType");
        }
    }
}
