using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderDAL orderDAL = new OrderDAL();

        public bool CreateOrder(Order order)
        {
            return orderDAL.CreateOrder(order);
        }
        public bool ChangeStatus(int status)
        {
            return orderDAL.ChangeStatus(status);
        }
        public List<Order> GetOrders(string searchValue, int offset)
        {
            return orderDAL.GetOrders(searchValue, offset);
        }
        public Order GetOrderById(int OrderId)
        {
            return orderDAL.GetOrderById(OrderId);
        }
        public int GetOrderCount(string searchValue)
        {
            return orderDAL.GetOrderCount(searchValue);
        }
    }
}