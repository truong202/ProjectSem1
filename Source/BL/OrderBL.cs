using System;
using System.Collections.Generic;
using Persistance;
using DAL;

namespace BL
{
    public class OrderBL
    {
        private OrderBL orderBL = new OrderBL();

        public bool CreateOrder(Order order)
        {
            return orderBL.CreateOrder(order);
        }
        public bool ChangeStatus(int status)
        {
            return orderBL.ChangeStatus(status);
        }
        public List<Order> GetOrders()
        {
            return orderBL.GetOrders();
        }
    }
}