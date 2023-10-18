namespace DataLayer;

public class Order
{
    public int Id { get; set; }
    public String OrderDate { get; set; }
    public String RequiredDate { get; set; }
    public String ShippedDate { get; set; }
    public int Freight { get; set; }
    public String ShipName { get; set; }
    public String ShipCity { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; }  

    public override string ToString()
    {
        return $"{Id}, {OrderDate}, {RequiredDate},{ShippedDate},{Freight},{ShipName}, {ShipCity}";
    }
}
