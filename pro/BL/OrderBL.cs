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
        public bool ChangeStatus(int status, int orderId, int staffId)
        {
            return orderDAL.ChangeStatus(status, orderId, staffId);
        }
        public List<Order> GetOrdersUnpaid()
        {
            return orderDAL.GetOrdersUnpaid();
        }
        public Order GetById(int orderId)
        {
            return orderDAL.GetById(orderId);
        }
        public bool Payment(Order order){
            return orderDAL.Payment(order);
        }
        public bool ConfirmPayment(Order order)
        {
            return orderDAL.ConfirmPayment(order);
        }
        public bool CancelPayment(Order order)
        {
            return orderDAL.CancelPayment(order);
        }

    }
}