﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.DataAccess;

namespace WebApi.DataAccess.Migrations
{
    [DbContext(typeof(WebStoreContext))]
    [Migration("20190714033425_ModifyActiveFlagNullable")]
    partial class ModifyActiveFlagNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.DataAccess.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<byte[]>("Data")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AvatarId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Highlight")
                        .HasMaxLength(200);

                    b.Property<string>("Ingredient")
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("LongName")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Origin")
                        .HasMaxLength(50);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(9, 2)");

                    b.Property<string>("Producer")
                        .HasMaxLength(50);

                    b.Property<string>("ProductNumber")
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.Property<string>("Volume")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2747), new TimeSpan(0, -4, 0, 0, 0)),
                            IsActive = true,
                            Name = "Featured",
                            UpdatedOn = new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2783), new TimeSpan(0, -4, 0, 0, 0))
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2789), new TimeSpan(0, -4, 0, 0, 0)),
                            IsActive = true,
                            Name = "Hot",
                            UpdatedOn = new DateTimeOffset(new DateTime(2019, 7, 13, 23, 34, 25, 194, DateTimeKind.Unspecified).AddTicks(2793), new TimeSpan(0, -4, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.ProductFunction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<string>("Detail");

                    b.Property<int>("ProductId");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFunctions");
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.ProductImage", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("ImageId");

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.HasKey("ProductId", "ImageId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("UpdatedOn");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.Product", b =>
                {
                    b.HasOne("WebApi.DataAccess.Entities.Image", "Avatar")
                        .WithMany("Products")
                        .HasForeignKey("AvatarId");

                    b.HasOne("WebApi.DataAccess.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.ProductFunction", b =>
                {
                    b.HasOne("WebApi.DataAccess.Entities.Product", "Product")
                        .WithMany("Functions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.DataAccess.Entities.ProductImage", b =>
                {
                    b.HasOne("WebApi.DataAccess.Entities.Image", "Image")
                        .WithMany("ProductImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApi.DataAccess.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
