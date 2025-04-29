using Microsoft.EntityFrameworkCore;
using BizBulkApp.Models;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public AppDbContext() { }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<QuotationItem> QuotationItems { get; set; }

    // Fluent API configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define relationships for Quotation and QuotationItem
        modelBuilder.Entity<Quotation>()
            .HasMany(q => q.QuotationItems)
            .WithOne(qi => qi.Quotation)
            .HasForeignKey(qi => qi.QuotationId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for QuotationItems

        // Define relationships for Buyer and Seller (User -> Quotation)
        modelBuilder.Entity<Quotation>()
            .HasOne(q => q.Buyer)
            .WithMany() // Assuming no collection in User
            .HasForeignKey(q => q.BuyerId)
            .OnDelete(DeleteBehavior.NoAction); // Specify NoAction for the BuyerId

        modelBuilder.Entity<Quotation>()
            .HasOne(q => q.Seller)
            .WithMany() // Assuming no collection in User
            .HasForeignKey(q => q.SellerId)
            .OnDelete(DeleteBehavior.NoAction); // Specify NoAction for the SellerId

        // Define relationships for Order and OrderItem
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for OrderItems
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // If you're working with ASP.NET Core, move this configuration to Startup.cs or Program.cs
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BIZBULKDB;Integrated Security=True;");

    }
}
