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
            Order result = orderDAL.GetById(orderId);
            Assert.True(result != null);
            Assert.True(result.ID == orderId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000)]
        public void GetOrderByIdTest2(int orderId)
        {
            Order result = orderDAL.GetById(orderId);
            Assert.True(result == null);
        }
    }
}