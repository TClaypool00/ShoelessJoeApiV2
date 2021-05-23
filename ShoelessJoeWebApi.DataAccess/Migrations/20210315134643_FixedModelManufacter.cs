using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class FixedModelManufacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufacterId",
                table: "Models",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Models_ManufacterId",
                table: "Models",
                column: "ManufacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Manufacters_ManufacterId",
                table: "Models",
                column: "ManufacterId",
                principalTable: "Manufacters",
                principalColumn: "ManufacterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Manufacters_ManufacterId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_ManufacterId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "ManufacterId",
                table: "Models");
        }
    }
}
