using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class AddedShipped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Comments");
        }
    }
}
