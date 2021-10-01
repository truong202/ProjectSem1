using System;
using Xunit;
using Persistance;
using DAL;
using System.Collections.Generic;

namespace TestDAL
{
    public class GetOrdersUnpaidTest
    {
        private OrderDAL orderDAL = new OrderDAL();
        
        [Fact]
        public void GetOrdersUnpaid()
        {
            var result = orderDAL.GetOrdersUnpaid();
            if (result != null)
            {
                foreach (var order in result)
                {
                    Assert.True(order.Status == Order.UNPAID || order.Status == Order.PROCESSING);
                }
            }
        }
    }
}