namespace DataLayer;

public class OrderDetails
{
    public int OrderId { get; set; }
    public int ProductId {  get; set; }
    public int UnitPrice { get; set; }
    public int Quantity { get; set; }

    public int Discount { get; set; }

    public Product Product { get; set; }
    public Orders Order { get; set; }


    public override string ToString()
    {
        return $"{OrderId}, {UnitPrice},{Product}, {Order}";
    }
}