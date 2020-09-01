using SCM.Interfaces;
using SCM.Models;
using System;
using System.Collections.Generic;

namespace SCM.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository()
        {
        }

        public IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order{
                BillingAddress = "Bill Address",
                ShippingAddress = "Ship Address",
                InvoiceNo = "ORD1",
                OrderId = 1,
                OrderItems =
                new List<OrderItem>(){
                    new OrderItem{
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 300
                    }
                }
                },
                new Order{
                BillingAddress = "Bill Address",
                ShippingAddress = "Ship Address",
                InvoiceNo = "ORD2",
                OrderId = 2,
                OrderItems =
                new List<OrderItem>(){
                    new OrderItem{
                    ProductId = 2,
                    Quantity = 1,
                    UnitPrice = 1200
                    }
                }
                },
                new Order{
                BillingAddress = "Bill Address",
                ShippingAddress = "Ship Address",
                InvoiceNo = "ORD3",
                OrderId = 2,
                OrderItems =
                new List<OrderItem>(){
                    new OrderItem{
                    ProductId = 5,
                    Quantity = 1,
                    UnitPrice = 1200
                    }
                },
                MemberShipStartDate = DateTime.UtcNow.AddMonths(-1),
                MemberShipEndDate = DateTime.UtcNow.AddMinutes(5)
                },
                new Order{
                BillingAddress = "Bill Address",
                ShippingAddress = "Ship Address",
                InvoiceNo = "ORD4",
                OrderId = 3,
                OrderItems =
                new List<OrderItem>(){
                    new OrderItem{
                    ProductId = 3,
                    Quantity = 1,
                    UnitPrice = 200,
                    }
                },
                }
            };
        }
    }
}