using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class NewComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Replies_CommentBuyerId_CommentSellerId",
                table: "Replies"
                );

            migrationBuilder.DropColumn(
                name: "CommentBuyerId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "CommentSellerId",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Replies",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentId",
                table: "Replies",
                column: "CommentId"
                );

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: false),
                    CommentBody = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comments_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BuyerId",
                table: "Comments",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ShoeId",
                table: "Comments",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAndSeller_Comments_CommentId",
                table: "CommentAndSeller",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAndSeller_Comments_CommentId",
                table: "CommentAndSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Replies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentBuyerId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentSellerId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentBody = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "ShoeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ShoeId",
                table: "Comment",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId1",
                table: "Comment",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAndSeller_Comment_CommentId",
                table: "CommentAndSeller",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comment_CommentId",
                table: "Replies",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
