// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoelessJoeWebApi.DataAccess.DataModels;

namespace ShoelessJoeWebApi.DataAccess.Migrations
{
    [DbContext(typeof(ShoelessdevContext))]
    [Migration("20210307061718_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Comment", b =>
                {
                    b.Property<int>("BuyerId")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("CommentBody")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CommentHeader")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("BuyerId", "SellerId");

                    b.HasIndex("SellerId");

                    b.HasIndex("ShoeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Reply", b =>
                {
                    b.Property<int>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentBuyerId")
                        .HasColumnType("int");

                    b.Property<int>("CommentSellerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("ReplyBody")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReplyId");

                    b.HasIndex("UserId");

                    b.HasIndex("CommentBuyerId", "CommentSellerId");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BothShoes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("0");

                    b.Property<double?>("LeftSize")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Manufacter")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<double?>("RightSize")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("0");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ShoeId");

                    b.HasIndex("UserId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.ShoeImage", b =>
                {
                    b.Property<int>("ImgGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LeftShoeDown")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LeftShoeLeft")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LeftShoeRight")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LeftShoeUp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RightShoeDown")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RightShoeLeft")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RightShoeRight")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RightShoeUp")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("ImgGroupId");

                    b.HasIndex("ShoeId")
                        .IsUnique();

                    b.ToTable("ShoeImages");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Comment", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "Buyer")
                        .WithMany("BuyerComments")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "Seller")
                        .WithMany("SellerComments")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", "Shoe")
                        .WithMany("Comments")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Seller");

                    b.Navigation("Shoe");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Reply", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "User")
                        .WithMany("Replies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentBuyerId", "CommentSellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "User")
                        .WithMany("Shoes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.ShoeImage", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", "Shoe")
                        .WithOne("ShoeImage")
                        .HasForeignKey("ShoelessJoeWebApi.DataAccess.DataModels.ShoeImage", "ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shoe");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ShoeImage");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.User", b =>
                {
                    b.Navigation("BuyerComments");

                    b.Navigation("Replies");

                    b.Navigation("SellerComments");

                    b.Navigation("Shoes");
                });
#pragma warning restore 612, 618
        }
    }
}
