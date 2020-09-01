using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace SCM.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }

        public DateTime MemberShipStartDate { get; set; }
        public DateTime MemberEndStartDate { get; set; }
    }
}
