using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace TestDAL
{
    public class Payment
    {
        private OrderDAL orderDAL = new OrderDAL();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void PaymetTest1(int orderId)
        {
            var order = orderDAL.GetById(orderId);
            order.Status = Order.PAID;
            bool result = orderDAL.Payment(order);
            Assert.True(result);
            order = orderDAL.GetById(orderId);
            Assert.True(order.Status == Order.PAID);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void PaymetTest2(int orderId)
        {
            var order = orderDAL.GetById(orderId);
            order.Status = Order.CANCEL;
            bool result = orderDAL.Payment(order);
            Assert.True(result);
            order = orderDAL.GetById(orderId);
            Assert.True(order.Status == Order.CANCEL);
        }
    }
}