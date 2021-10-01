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
        public List<Order> GetByStatus(int status, int offset)
        {
            return orderDAL.GetByStatus(status, offset);
        }
        public Order GetById(int orderId)
        {
            return orderDAL.GetById(orderId);
        }
        public List<Order> GetOrdersUnpaid()
        {
            return orderDAL.GetOrdersUnpaid();
        }
        public bool ChangeStatus(int status,int orderId, int staffId)
        {
            return orderDAL.ChangeStatus(status, orderId, staffId);
        }
        public int GetCount(int status)
        {
            return orderDAL.GetCount(status);
        }
        public bool Payment(Order order)
        {
            return orderDAL.Payment(order);
        }
    }
}