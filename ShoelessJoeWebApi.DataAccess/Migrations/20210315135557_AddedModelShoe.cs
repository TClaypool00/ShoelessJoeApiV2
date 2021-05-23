using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class AddedModelShoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacter",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Shoes");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ModelId",
                table: "Shoes",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Models_ModelId",
                table: "Shoes",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Models_ModelId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ModelId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Shoes");

            migrationBuilder.AddColumn<string>(
                name: "Manufacter",
                table: "Shoes",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Shoes",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");
        }
    }
}
