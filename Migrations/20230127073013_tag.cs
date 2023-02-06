using Microsoft.EntityFrameworkCore.Migrations;

namespace ProDat.Web2.Migrations
{
    public partial class tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipTypeID",
                table: "Tag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_EquipTypeID",
                table: "Tag",
                column: "EquipTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_EquipmentTypes_EquipTypeID",
                table: "Tag",
                column: "EquipTypeID",
                principalTable: "EquipmentTypes",
                principalColumn: "EquipTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_EquipmentTypes_EquipTypeID",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_EquipTypeID",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "EquipTypeID",
                table: "Tag");
        }
    }
}
