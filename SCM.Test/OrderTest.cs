using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCM.Implementation;
using System;
using System.Linq;

namespace SCM.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly OrderService _orderService;

        public OrderTest()
        {
            _orderService = new OrderService();
        }

        [TestMethod]
        public void GetOrders_ThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _orderService.GetOrders());
        }

        [TestMethod]
        public void GetOrders_ReturnsNull()
        {
            Assert.IsNull(_orderService.GetOrders());
        }

        [TestMethod]
        public void GetOrders_Success()
        {
            Assert.AreEqual(_orderService.GetOrders().Count(),4);
        }
    }
}
