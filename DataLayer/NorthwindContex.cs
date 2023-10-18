using Microsoft.EntityFrameworkCore;

namespace EF;

public class NorthwindContex : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=admin");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>()
            .Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>()
            .Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>()
            .Property(x => x.Description).HasColumnName("description");


        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>()
            .Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>()
            .Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>()
            .Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>()
            .Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit"); 
        modelBuilder.Entity<Product>()
            .Property(x => x.UnitsInStock).HasColumnName("unitsinstock");
        modelBuilder.Entity<Product>()
            .Property(x => x.CategoryId).HasColumnName("categoryid");

        modelBuilder.Entity<Order>().ToTable("order");
        modelBuilder.Entity<Order>()
            .Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Order>()
            .Property(x => x.OrderDate).HasColumnName("orderdate");
        modelBuilder.Entity<Order>()
            .Property(x => x.RequiredDate).HasColumnName("requireddate");
        modelBuilder.Entity<Order>()
            .Property(x => x.ShippedDate).HasColumnName("shippeddate");
        modelBuilder.Entity<Order>()
            .Property(x => x.Freight).HasColumnName("Freight");
        modelBuilder.Entity<Order>()
            .Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Order>()
            .Property(x => x.ShipCity).HasColumnName("shipcity");


        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.Id).HasColumnName("orderdetailsid");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.Discount).HasColumnName("discount");
    }
}
