﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

using PartialFoods.Services.InventoryServer.Entities;

namespace PartialFoods.Services.InventoryServer.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("PartialFoods.Services.InventoryServer.Entities.Product", b =>
                {
                    b.Property<string>("SKU")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("OriginalQuantity");

                    b.HasKey("SKU");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PartialFoods.Services.InventoryServer.Entities.ProductActivity", b =>
                {
                    b.Property<string>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityType");

                    b.Property<long>("CreatedOn");

                    b.Property<string>("OrderID");

                    b.Property<int>("Quantity");

                    b.Property<string>("SKU");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
