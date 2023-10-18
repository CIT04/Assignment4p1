

namespace EF;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{Id}, {Name}, {Description}";
    }
}
