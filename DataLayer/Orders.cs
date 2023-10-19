namespace DataLayer
{
    public class Orders
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Required { get; set; }
        //public DateTime ShippedDate { get; set; }
        public int Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Date}, {Required}, {Freight}, {ShipName}, {ShipCity}";
        }
    }
}