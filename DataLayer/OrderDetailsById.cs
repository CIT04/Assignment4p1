namespace DataLayer
{
    public class OrderDetailsById
    {
        public List<int> ProductIds { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }

    public class ProductDetail
    {
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
    }
}