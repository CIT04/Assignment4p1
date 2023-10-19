using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer;

public class Category
{
    public int Id { get; set; }
   
    public string Description { get; set; }

  //  [Column("categoryname")]
    public string Name { get; set; }
    public override string ToString()
    {
        return $"{Id}, {Name}, {Description}";
    }
}