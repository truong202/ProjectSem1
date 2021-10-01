using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace TestDAL
{
    public class ChangeOrderStatusTest
    {
        private OrderDAL orderDAL = new OrderDAL();

        [Theory]
        [InlineData(1, 1, 5)]
        [InlineData(2, 2, 4)]
        [InlineData(1, 7, 6)]
        [InlineData(4, 3, 6)]
        [InlineData(2, 4, 6)]
        public void ChangeOrderStatusTest1(int status, int orderId, int staffId)
        {
            var result = orderDAL.ChangeStatus(status, orderId, staffId);
            Assert.True(result == true);
            var order = orderDAL.GetById(orderId);
            Assert.True(order.Status == status);
        }
    }
}