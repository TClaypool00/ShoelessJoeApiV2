using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class ChangedAddressStateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacters_States_StateId",
                table: "Manufacters");

            migrationBuilder.DropIndex(
                name: "IX_Manufacters_StateId",
                table: "Manufacters");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Manufacters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Manufacters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacters_StateId",
                table: "Manufacters",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacters_States_StateId",
                table: "Manufacters",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
