using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class RemovedFieldsImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftShoeDown",
                table: "ShoeImages");

            migrationBuilder.DropColumn(
                name: "LeftShoeUp",
                table: "ShoeImages");

            migrationBuilder.DropColumn(
                name: "RightShoeDown",
                table: "ShoeImages");

            migrationBuilder.DropColumn(
                name: "RightShoeUp",
                table: "ShoeImages");

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RightShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "/Shoes/generic-shoe.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeftShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AddColumn<string>(
                name: "LeftShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AddColumn<string>(
                name: "RightShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AddColumn<string>(
                name: "RightShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValueSql: "/Shoes/generic-shoe.png");
        }
    }
}
