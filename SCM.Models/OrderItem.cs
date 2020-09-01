using System;

namespace SCM.Models
{
    public class OrderItem
    {
        public long ProductId { get; set; } 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
