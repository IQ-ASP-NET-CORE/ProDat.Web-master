using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class Historian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs");

            migrationBuilder.CreateTable(
                name: "Historian",
                columns: table => new
                {
                    HistorianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportId = table.Column<int>(nullable: false),
                    EntityName = table.Column<int>(nullable: false),
                    Pk1 = table.Column<int>(nullable: false),
                    Pk2 = table.Column<int>(nullable: false),
                    AttributeName = table.Column<string>(nullable: true),
                    AttributeValue = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historian", x => x.HistorianId);
                });

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
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs",
                column: "EngClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UC4ViewColumnsUser_UC4ViewColumnsId",
                table: "UC4ViewColumnsUser",
                column: "UC4ViewColumnsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historian");

            migrationBuilder.DropTable(
                name: "UC4ViewColumnsUser");

            migrationBuilder.DropTable(
                name: "UC4ViewColumns");

            migrationBuilder.DropIndex(
                name: "IX_MaintClass_MaintClassName",
                table: "MaintClass");

            migrationBuilder.DropIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs");

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityName = table.Column<int>(type: "int", nullable: false),
                    ImportId = table.Column<int>(type: "int", nullable: false),
                    Pk1 = table.Column<int>(type: "int", nullable: false),
                    Pk2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EngClassRequiredDocs_EngClassId",
                table: "EngClassRequiredDocs",
                column: "EngClassId",
                unique: true);
        }
    }
}
