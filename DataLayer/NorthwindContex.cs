using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class NorthwindContex : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=localhost;db=northwinddb;uid=postgres;pwd=admin");
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

        modelBuilder.Entity<Orders>().ToTable("orders");
        modelBuilder.Entity<Orders>()
            .Property(x => x.Id).HasColumnName("orderid");
        modelBuilder.Entity<Orders>()
            .Property(x => x.Date).HasColumnName("orderdate");
        modelBuilder.Entity<Orders>()
            .Property(x => x.Required).HasColumnName("requireddate");
        modelBuilder.Entity<Orders>()
            .Property(x => x.Freight).HasColumnName("freight");
        modelBuilder.Entity<Orders>()
            .Property(x => x.ShipName).HasColumnName("shipname");
        modelBuilder.Entity<Orders>()
            .Property(x => x.ShipCity).HasColumnName("shipcity");


        modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
        modelBuilder.Entity<OrderDetails>().HasKey(x =>new { x.OrderId, x.ProductId });
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.OrderId).HasColumnName("orderid");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.ProductId).HasColumnName("productid");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.Quantity).HasColumnName("quantity");
        modelBuilder.Entity<OrderDetails>()
            .Property(x => x.Discount).HasColumnName("discount");

    }
}
