using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class superclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FKsuperClassIdSuperclassID",
                table: "EngClass",
                nullable: true);

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
                name: "IX_EngClass_FKsuperClassIdSuperclassID",
                table: "EngClass",
                column: "FKsuperClassIdSuperclassID");

            migrationBuilder.CreateIndex(
                name: "U_SuperClass",
                table: "SuperClass",
                column: "SuperclassID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EngClass_SuperClass_FKsuperClassIdSuperclassID",
                table: "EngClass",
                column: "FKsuperClassIdSuperclassID",
                principalTable: "SuperClass",
                principalColumn: "SuperclassID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngClass_SuperClass_FKsuperClassIdSuperclassID",
                table: "EngClass");

            migrationBuilder.DropTable(
                name: "SuperClass");

            migrationBuilder.DropIndex(
                name: "IX_EngClass_FKsuperClassIdSuperclassID",
                table: "EngClass");

            migrationBuilder.DropColumn(
                name: "FKsuperClassIdSuperclassID",
                table: "EngClass");
        }
    }
}
