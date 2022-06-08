﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SinglePage.Data;

#nullable disable

namespace SinglePage.Migrations
{
    [DbContext(typeof(SinglePageContext))]
    partial class SinglePageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SinglePage.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Checked")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("shoppingListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("shoppingListId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("SinglePage.Models.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingList");
                });

            modelBuilder.Entity("SinglePage.Models.Item", b =>
                {
                    b.HasOne("SinglePage.Models.ShoppingList", null)
                        .WithMany("items")
                        .HasForeignKey("shoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SinglePage.Models.ShoppingList", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
