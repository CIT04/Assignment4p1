namespace DataLayer;

public class Order
{
    public int Id { get; set; }
    public string OrderDate { get; set; }
    public string RequiredDate { get; set; }
    public string ShippedDate { get; set; }
    public int Freight { get; set; }
    public string ShipName { get; set; }
    public string ShipCity { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; }

    public override string ToString()
    {
        return $"{Id}, {OrderDate}, {RequiredDate},{ShippedDate},{Freight},{ShipName}, {ShipCity}";
    }
}