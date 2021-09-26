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
        public List<Order> GetOrders(string searchValue, int offset)
        {
            return orderDAL.GetOrders(searchValue, offset);
        }
        public Order GetOrderById(int orderId)
        {
            return orderDAL.GetOrderById(orderId);
        }
        public int GetOrderCount(string searchValue)
        {
            return orderDAL.GetOrderCount(searchValue);
        }
        public bool ConfirmPayment(Order order)
        {
            return orderDAL.ConfirmPayment(order);
        }
        public bool CancelPayment(Order order)
        {
            return orderDAL.CancelPayment(order);
        }
        public bool ChangeStatus(Order order)
        {
            return orderDAL.ChangeStatus(order);
        }

    }
}