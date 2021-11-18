using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class DroppedComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentAndSeller_Comments_CommentBuyerId1_CommentSellerId",
                table: "CommentAndSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Shoes_ShoeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_BuyerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_SellerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_CommentBuyerId_CommentSellerId",
                table: "Replies");

            migrationBuilder.DropTable(
                name: "Comments"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Shoes_ShoeId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId1",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentAndSeller_Comment_CommentId",
                table: "CommentAndSeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comment_CommentId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_CommentId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_CommentAndSeller_CommentId",
                table: "CommentAndSeller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ShoeId",
                table: "Comments",
                newName: "IX_Comments_ShoeId");

            migrationBuilder.AddColumn<int>(
                name: "CommentBuyerId1",
                table: "CommentAndSeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentSellerId",
                table: "CommentAndSeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                columns: new[] { "BuyerId", "SellerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentBuyerId_CommentSellerId",
                table: "Replies",
                columns: new[] { "CommentBuyerId", "CommentSellerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAndSeller_CommentBuyerId1_CommentSellerId",
                table: "CommentAndSeller",
                columns: new[] { "CommentBuyerId1", "CommentSellerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SellerId",
                table: "Comments",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentAndSeller_Comments_CommentBuyerId1_CommentSellerId",
                table: "CommentAndSeller",
                columns: new[] { "CommentBuyerId1", "CommentSellerId" },
                principalTable: "Comments",
                principalColumns: new[] { "BuyerId", "SellerId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Shoes_ShoeId",
                table: "Comments",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_BuyerId",
                table: "Comments",
                column: "BuyerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_SellerId",
                table: "Comments",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_CommentBuyerId_CommentSellerId",
                table: "Replies",
                columns: new[] { "CommentBuyerId", "CommentSellerId" },
                principalTable: "Comments",
                principalColumns: new[] { "BuyerId", "SellerId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
