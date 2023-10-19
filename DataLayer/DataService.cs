using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class DataService
{
    public IList<Category> GetCategories()
    {
        var db = new NorthwindContex();
        return db.Categories.ToList();
    }

    public Category GetCategory(int categoryId)
    {
        var db = new NorthwindContex();
        return db.Categories.FirstOrDefault(x => x.Id == categoryId);

    }

    public bool DeleteCategory(Category category)
    {
        return DeleteCategory(category.Id);
    }
    public bool DeleteCategory(int categoryId)
    {
        var db = new NorthwindContex();
        var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
        if (category != null)
        {
            db.Categories.Remove(category);
            return db.SaveChanges() > 0;
        }
        return false;
    }

    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContex();
        var id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };
        db.Add(category);
        db.SaveChanges();
        return category;
    }


    public bool UpdateCategory(int categoryId, string updatedName, string updatedDescription)
    {
        var db = new NorthwindContex();
        var category = db.Categories.FirstOrDefault(x => x.Id == categoryId);
        if (category != null)
        {
            category.Name = updatedName;
            category.Description = updatedDescription;

            db.Categories.Update(category);
            db.SaveChanges();
            return true;
        }
        return false;
    }

    public ProductByCategory GetProduct(int productId)
    {
        using (var db = new NorthwindContex())
        {
            var product = db.Products
                .Include(p => p.Category)
                .Where(p => p.Id == productId)
                .Select(p => new ProductByCategory
                {
                    Name = p.Name,
                    CategoryName = p.Category.Name
                })
                .FirstOrDefault();

            return product;
        }
    }

    public List<ProductByCategory> GetProductByCategory(int categoryId)
    {
        using (var db = new NorthwindContex())
        {
            var products = db.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductByCategory
                {
                    Name = p.Name,
                    CategoryName = p.Category.Name
                })
                .ToList();

            return products;
        }
    }
public List<GetProductNameTest> GetProductByName(string nameSubString)
    {
        var db = new NorthwindContex();

        var products = db.Products
                        .Include(p => p.Category)
                        .Where(p => p.Name.Contains(nameSubString))
                        .Select(p => new GetProductNameTest
                        {
           
                            CategoryName = p.Category.Name,
                            ProductName = p.Name,
                            
                        })
                        .ToList();

        return products;
    }
    public Orders GetOrder(int orderId)
    {
        using var db = new NorthwindContex();
        var order = db.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ThenInclude(p => p.Category)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            throw new Exception($"Order with ID {orderId} not found.");
        }

        return order;
    }
    public IList<Orders>  GetOrders()
    {
        using var db = new NorthwindContex();
        return db.Orders.ToList();
    }
    public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
    {
        var db = new NorthwindContex();
        using (db)
        {
            var orderDetails = db.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Order)
                .Where(od => od.OrderId == orderId)
                .Select(od => new OrderDetails
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = od.Discount,
                    Product = new Product
                    {
                        // Populate product properties from the related entity (if needed)
                        Name = od.Product.Name,
                        // Include other properties as necessary
                    },
                    Order = new Orders
                    {
                        // Populate order properties from the related entity (if needed)
                    }
                })
                .ToList();

            return orderDetails;
        }
    }




}


/* */



