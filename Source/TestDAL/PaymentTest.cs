using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace TestDAL
{
    public class PaymentTest
    {
        private OrderDAL orderDAL = new OrderDAL();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void PaymentTest1(int orderId)
        {
            var order = orderDAL.GetById(orderId);
            order.Status = Order.PAID;
            order.Accountant.ID = 4;
            bool result = orderDAL.Payment(order);
            Assert.True(result);
            order = orderDAL.GetById(orderId);
            Assert.True(order.Status == Order.PAID);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void PaymentTest2(int orderId)
        {
            var order = orderDAL.GetById(orderId);
            order.Status = Order.CANCEL;
            order.Accountant.ID = 5;
            bool result = orderDAL.Payment(order);
            Assert.True(result);
            order = orderDAL.GetById(orderId);
            Assert.True(order.Status == Order.CANCEL);
        }
    }
}