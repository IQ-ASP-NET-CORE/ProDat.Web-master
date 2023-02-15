using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class uc4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UC4ViewColumns",
                columns: table => new
                {
                    UC4ViewColumnsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sectionOrder = table.Column<int>(nullable: false),
                    sectionName = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC4ViewColumns", x => x.UC4ViewColumnsId);
                });

            migrationBuilder.CreateTable(
                name: "UC4ViewColumnsUser",
                columns: table => new
                {
                    UC4ViewColumnsUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UC4ViewColumnsId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    height = table.Column<int>(nullable: false),
                    width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UC4ViewColumnsUser", x => x.UC4ViewColumnsUserId);
                    table.ForeignKey(
                        name: "FK_UC4ViewColumnsUser_UC4ViewColumns_UC4ViewColumnsId",
                        column: x => x.UC4ViewColumnsId,
                        principalTable: "UC4ViewColumns",
                        principalColumn: "UC4ViewColumnsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintClass_MaintClassName",
                table: "MaintClass",
                column: "MaintClassName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UC4ViewColumnsUser_UC4ViewColumnsId",
                table: "UC4ViewColumnsUser",
                column: "UC4ViewColumnsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UC4ViewColumnsUser");

            migrationBuilder.DropTable(
                name: "UC4ViewColumns");

            migrationBuilder.DropIndex(
                name: "IX_MaintClass_MaintClassName",
                table: "MaintClass");
        }
    }
}
