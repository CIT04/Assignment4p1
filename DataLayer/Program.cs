

using EF;
using Microsoft.EntityFrameworkCore;

var db = new NorthwindContex();

//CreateCategory(db);
//DeleteCategory(db);
//UpdateCategory(db);

foreach (var entity in db.Products.Include(x => x.Category))
{
    Console.WriteLine(entity);
}



static void CreateCategory(NorthwindContex db)
{
    var category = new Category
    {
        Id = 101,
        Name = "flksdflæsfj",
        Description = "dfsdfdfs"
    };

    db.Add(category);

    db.SaveChanges();
}

static void DeleteCategory(NorthwindContex db)
{
    var category = db.Categories.FirstOrDefault(x => x.Id == 101);
    if (category != null)
    {
        db.Categories.Remove(category);
        db.SaveChanges();
    }
}

static void UpdateCategory(NorthwindContex db)
{
    var category = db.Categories.FirstOrDefault(x => x.Id == 101);
    if (category != null)
    {
        category.Name = "Updated";
        db.SaveChanges();
    }
}