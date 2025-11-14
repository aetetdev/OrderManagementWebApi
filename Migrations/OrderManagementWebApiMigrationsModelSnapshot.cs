using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace OrderManagementWebApi.Migrations
{
 [DbContext(typeof(OrderManagementWebApi.Data.AppDbContext))]
 partial class OrderManagementWebApiMigrationsModelSnapshot : ModelSnapshot
 {
 protected override void BuildModel(ModelBuilder modelBuilder)
 {
 modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

 modelBuilder.Entity("OrderManagementWebApi.Models.Product", b =>
 {
 b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("TEXT");
 b.Property<string>("Name").IsRequired().HasMaxLength(200).HasColumnType("TEXT");
 b.Property<int>("Stock").HasColumnType("INTEGER");
 b.Property<decimal>("Price").HasColumnType("decimal(18,2)");
 b.HasKey("Id");
 b.ToTable("Products");
 });

 modelBuilder.Entity("OrderManagementWebApi.Models.Order", b =>
 {
 b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("TEXT");
 b.Property<Guid>("UserId").HasColumnType("TEXT");
 b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
 b.HasKey("Id");
 b.ToTable("Orders");
 });

 modelBuilder.Entity("OrderManagementWebApi.Models.OrderItem", b =>
 {
 b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("TEXT");
 b.Property<Guid>("OrderId").HasColumnType("TEXT");
 b.Property<Guid>("ProductId").HasColumnType("TEXT");
 b.Property<string>("ProductName").IsRequired().HasMaxLength(200).HasColumnType("TEXT");
 b.Property<int>("Quantity").HasColumnType("INTEGER");
 b.Property<decimal>("UnitPrice").HasColumnType("decimal(18,2)");
 b.HasKey("Id");
 b.HasIndex("OrderId");
 b.ToTable("OrderItems");
 });

 modelBuilder.Entity("OrderManagementWebApi.Models.OrderItem", b =>
 {
 b.HasOne("OrderManagementWebApi.Models.Order").WithMany("Items").HasForeignKey("OrderId").OnDelete(DeleteBehavior.Cascade).IsRequired();
 });
 }
 }
}
