using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class SoldShoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Shoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Comments",
                type: "bit",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Comments");
        }
    }
}
