using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class madeNullForImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RightShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                //defaultValueSql: "notShown-pic.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                //defaultValue: "notShown-pic.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                //defaultValueSql: "notShown-pic.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                //defaultValueSql: "notShown-pic.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeLeft",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                //defaultValueSql: "notShown-shoe.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RightShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeRight",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
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
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "RightShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeUp",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
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
                nullable: false,
                defaultValue: "",
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
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");

            migrationBuilder.AlterColumn<string>(
                name: "LeftShoeDown",
                table: "ShoeImages",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValueSql: "/Shoes/generic-shoe.png");
        }
    }
}
