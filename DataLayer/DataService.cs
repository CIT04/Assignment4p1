namespace DataLayer;

public class DataService
{
    public object GetCategories()
    {
        var db = new NorthwindContex();
        var categories = new List<Category>();
        foreach (var entity in db.Categories) categories.Add(entity);
    }
}