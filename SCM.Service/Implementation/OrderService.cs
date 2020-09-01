using SCM.Interfaces;
using SCM.Models;
using System.Collections.Generic;

namespace SCM.Implementation
{
	public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService()
		{
            _orderRepository = new OrderRepository();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }
    }
}