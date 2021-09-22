using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;


namespace TestDAL
{
    public class GetOrderByIdTest
    {
        private OrderDAL orderDAL = new OrderDAL();
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetOrderByIdTest1(int orderId)
        {
            Order result = orderDAL.GetOrderById(orderId);
            Assert.True(result != null);
            Assert.True(result.OrderId == orderId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(30)]
        public void GetOrderByIdTest2(int orderId)
        {
            Order result = orderDAL.GetOrderById(orderId);
            Assert.True(result == null);
        }
    }
}