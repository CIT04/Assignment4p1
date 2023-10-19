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

    public Product GetProduct(int productId)
    {
        var db = new NorthwindContex();
        var product = db.Products
                       .Include(p => p.Category) // Include the Category navigation property
                       .Select(p => new Product
                       {
                           Id = p.Id,
                           Name = p.Name,
                           CategoryId = p.CategoryId,
                           UnitPrice = p.UnitPrice,
                           QuantityPerUnit = p.QuantityPerUnit,
                           UnitsInStock = p.UnitsInStock,
                           CategoryName = p.Category.Name, // Populate CategoryName from Category.Name
                           Category = p.Category
                       })
                       .FirstOrDefault(p => p.Id == productId);
        return product;
    }

    /* */
}


