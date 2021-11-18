using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    public partial class AddedCommentSeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentAndSeller",
                columns: table => new
                {
                    CommentBuyerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CommentBuyerId1 = table.Column<int>(type: "int", nullable: false),
                    CommentSellerId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentAndSeller", x => x.CommentBuyerId);
                    table.ForeignKey(
                        name: "FK_CommentAndSeller_Comments_CommentBuyerId1_CommentSellerId",
                        columns: x => new { x.CommentBuyerId1, x.CommentSellerId },
                        principalTable: "Comments",
                        principalColumns: new[] { "BuyerId", "SellerId" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommentAndSeller_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAndSeller_CommentBuyerId1_CommentSellerId",
                table: "CommentAndSeller",
                columns: new[] { "CommentBuyerId1", "CommentSellerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CommentAndSeller_SellerId",
                table: "CommentAndSeller",
                column: "SellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentAndSeller");
        }
    }
}
