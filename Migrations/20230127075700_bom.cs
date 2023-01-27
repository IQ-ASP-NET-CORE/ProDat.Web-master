using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class bom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelID",
                table: "EquipmentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BOM",
                columns: table => new
                {
                    BOMID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipTypeID = table.Column<int>(nullable: false),
                    ItemCatalogID = table.Column<int>(nullable: false),
                    ItemQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOM", x => x.BOMID);
                    table.ForeignKey(
                        name: "FK_BOM_EquipmentTypes_EquipTypeID",
                        column: x => x.EquipTypeID,
                        principalTable: "EquipmentTypes",
                        principalColumn: "EquipTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BOM_ItemCatalog_ItemCatalogID",
                        column: x => x.ItemCatalogID,
                        principalTable: "ItemCatalog",
                        principalColumn: "ItemCatalogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_ModelID",
                table: "EquipmentTypes",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_BOM_EquipTypeID",
                table: "BOM",
                column: "EquipTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BOM_ItemCatalogID",
                table: "BOM",
                column: "ItemCatalogID");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_Models_ModelID",
                table: "EquipmentTypes",
                column: "ModelID",
                principalTable: "Models",
                principalColumn: "ModelID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_Models_ModelID",
                table: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "BOM");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_ModelID",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "ModelID",
                table: "EquipmentTypes");
        }
    }
}
