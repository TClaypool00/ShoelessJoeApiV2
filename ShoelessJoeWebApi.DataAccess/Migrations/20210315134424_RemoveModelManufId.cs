using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class RemoveModelManufId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacters_Models_ModelId",
                table: "Manufacters");

            migrationBuilder.DropIndex(
                name: "IX_Manufacters_ModelId",
                table: "Manufacters");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Manufacters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Manufacters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacters_ModelId",
                table: "Manufacters",
                column: "ModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacters_Models_ModelId",
                table: "Manufacters",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
