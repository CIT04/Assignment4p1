using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OrderDateUnitPriceQuantity
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public Orders Order { get; set; }
    }
}