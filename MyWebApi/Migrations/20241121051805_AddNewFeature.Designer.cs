﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApi.Database;

#nullable disable

namespace MyWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241121051805_AddNewFeature")]
    partial class AddNewFeature
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWebApi.Models.SKU", b =>
                {
                    b.Property<int>("SKUId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SKUId"));

                    b.Property<string>("NewFeature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("SKUName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SKUQuantity")
                        .HasColumnType("int");

                    b.HasKey("SKUId");

                    b.ToTable("SKUs");
                });
#pragma warning restore 612, 618
        }
    }
}
