using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class BccCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngClass_SuperClass_FKsuperClassIdSuperclassID",
                table: "EngClass");

            migrationBuilder.DropIndex(
                name: "IX_EngClass_FKsuperClassIdSuperclassID",
                table: "EngClass");

            migrationBuilder.DropColumn(
                name: "FKsuperClassIdSuperclassID",
                table: "EngClass");

            migrationBuilder.AddColumn<int>(
                name: "BccCodeId",
                table: "EngDataClassxEngDataCode",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FKBccCodeId",
                table: "EngDataClassxEngDataCode",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FKsuperClassId",
                table: "EngClass",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuperclassID",
                table: "EngClass",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BccCode",
                columns: table => new
                {
                    BccCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BccCodeDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BccCode", x => x.BccCodeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngDataClassxEngDataCode_BccCodeId",
                table: "EngDataClassxEngDataCode",
                column: "BccCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EngClass_SuperclassID",
                table: "EngClass",
                column: "SuperclassID");

            migrationBuilder.AddForeignKey(
                name: "FK_EngClass_SuperClass_SuperclassID",
                table: "EngClass",
                column: "SuperclassID",
                principalTable: "SuperClass",
                principalColumn: "SuperclassID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EngDataClassxEngDataCode_BccCode_BccCodeId",
                table: "EngDataClassxEngDataCode",
                column: "BccCodeId",
                principalTable: "BccCode",
                principalColumn: "BccCodeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngClass_SuperClass_SuperclassID",
                table: "EngClass");

            migrationBuilder.DropForeignKey(
                name: "FK_EngDataClassxEngDataCode_BccCode_BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropTable(
                name: "BccCode");

            migrationBuilder.DropIndex(
                name: "IX_EngDataClassxEngDataCode_BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropIndex(
                name: "IX_EngClass_SuperclassID",
                table: "EngClass");

            migrationBuilder.DropColumn(
                name: "BccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropColumn(
                name: "FKBccCodeId",
                table: "EngDataClassxEngDataCode");

            migrationBuilder.DropColumn(
                name: "FKsuperClassId",
                table: "EngClass");

            migrationBuilder.DropColumn(
                name: "SuperclassID",
                table: "EngClass");

            migrationBuilder.AddColumn<int>(
                name: "FKsuperClassIdSuperclassID",
                table: "EngClass",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngClass_FKsuperClassIdSuperclassID",
                table: "EngClass",
                column: "FKsuperClassIdSuperclassID");

            migrationBuilder.AddForeignKey(
                name: "FK_EngClass_SuperClass_FKsuperClassIdSuperclassID",
                table: "EngClass",
                column: "FKsuperClassIdSuperclassID",
                principalTable: "SuperClass",
                principalColumn: "SuperclassID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
