namespace EF;

public class OrderDetails
{
    public int Id { get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }

    public int Discount { get; set; }

    public Order Order { get; set; }


    public override string ToString()
    {
        return $"{Id}, {Name}, {Order}";
    }
}
