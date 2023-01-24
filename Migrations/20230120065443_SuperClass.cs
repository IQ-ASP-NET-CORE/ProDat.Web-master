using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class SuperClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BccCodeId",
                table: "EngDataClassxEngDataCode",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuperClassID",
                table: "EngClass",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BccCode",
                columns: table => new
                {
                    BccCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BccCodeNumber = table.Column<int>(nullable: false),
                    BccCodeDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BccCode", x => x.BccCodeId);
                });

            migrationBuilder.CreateTable(
                name: "SuperClass",
                columns: table => new
                {
                    SuperclassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuperclassName = table.Column<string>(maxLength: 50, nullable: false),
                    Superclassdescription = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperClass", x => x.SuperclassID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngDataClassxEngDataCode_BccCodeId",
                table: "EngDataClassxEngDataCode",
                column: "BccCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngClass_SuperClassID",
                table: "EngClass",
                column: "SuperClassID");

            migrationBuilder.CreateIndex(
                name: "U_SuperClass",
                table: "SuperClass",
                column: "SuperclassID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngClass_SuperClass_SuperClassID",
                table: "EngClass",
                column: "SuperClassID",
                principalTable: "SuperClass",
                principalColumn: "SuperclassID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EngDataClassxEngDataCode_BccCode_BccCodeId",
                table: "EngDataClassxEngDataCode",
                column: "BccCodeId",
                principalTable: "BccCode",
                principalColumn: "BccCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngClass_SuperClass_SuperClassID",
                table: "EngClass");

            migrationBuilder.DropForeignKey(
                name: "FK_EngDataClassxEngDataCode_BccCode_BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropTable(
                name: "BccCode");

            migrationBuilder.DropTable(
                name: "SuperClass");

            migrationBuilder.DropIndex(
                name: "IX_EngDataClassxEngDataCode_BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropIndex(
                name: "IX_EngClass_SuperClassID",
                table: "EngClass");

            migrationBuilder.DropColumn(
                name: "BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropColumn(
                name: "SuperClassID",
                table: "EngClass");
        }
    }
}
