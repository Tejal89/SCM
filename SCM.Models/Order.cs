using System;
using System.Collections.Generic;

namespace SCM.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
    }
}
