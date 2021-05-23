﻿// <auto-generated />
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
    [Migration("20210520201739_RemovedSite")]
    partial class RemovedSite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("AddressId");

                    b.HasIndex("StateId");

                    b.ToTable("Addresses");
                });

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

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Friend", b =>
                {
                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("RecieverId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAccepted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.HasKey("SenderId", "RecieverId");

                    b.HasIndex("RecieverId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Manufacter", b =>
                {
                    b.Property<int>("ManufacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("ManufacterId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Manufacters");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ManufacterId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ModelId");

                    b.HasIndex("ManufacterId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentBody")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
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

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("SchoolId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("BothShoes")
                        .HasColumnType("bit");

                    b.Property<double?>("LeftSize")
                        .HasColumnType("float");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<double?>("RightSize")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ShoeId");

                    b.HasIndex("ModelId");

                    b.HasIndex("UserId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.ShoeImage", b =>
                {
                    b.Property<int>("ImgGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LeftShoeLeft")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LeftShoeRight")
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

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("ImgGroupId");

                    b.HasIndex("ShoeId")
                        .IsUnique();

                    b.ToTable("ShoeImages");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StateAbr")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("StateId");

                    b.ToTable("States");
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
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Address", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.State", "State")
                        .WithMany("Addresses")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
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

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Friend", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "Reciever")
                        .WithMany("RecieverFriends")
                        .HasForeignKey("RecieverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "Sender")
                        .WithMany("SenderFriends")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reciever");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Manufacter", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Address", "Address")
                        .WithOne("Manufacter")
                        .HasForeignKey("ShoelessJoeWebApi.DataAccess.DataModels.Manufacter", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Model", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Manufacter", "Manufacter")
                        .WithMany("Models")
                        .HasForeignKey("ManufacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacter");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Post", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
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

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.School", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Address", "Address")
                        .WithOne("School")
                        .HasForeignKey("ShoelessJoeWebApi.DataAccess.DataModels.School", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.Model", "Model")
                        .WithMany("Shoes")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.User", "User")
                        .WithMany("Shoes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");

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

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.User", b =>
                {
                    b.HasOne("ShoelessJoeWebApi.DataAccess.DataModels.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId");

                    b.Navigation("School");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Address", b =>
                {
                    b.Navigation("Manufacter");

                    b.Navigation("School");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Manufacter", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Model", b =>
                {
                    b.Navigation("Shoes");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.School", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.Shoe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ShoeImage");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.State", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ShoelessJoeWebApi.DataAccess.DataModels.User", b =>
                {
                    b.Navigation("BuyerComments");

                    b.Navigation("Posts");

                    b.Navigation("RecieverFriends");

                    b.Navigation("Replies");

                    b.Navigation("SellerComments");

                    b.Navigation("SenderFriends");

                    b.Navigation("Shoes");
                });
#pragma warning restore 612, 618
        }
    }
}
