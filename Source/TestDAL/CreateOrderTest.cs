using System;
using Xunit;
using Persistance;
using DAL;
using System.Collections.Generic;

namespace TestDAL
{
    public class CreateOrderTest
    {
        private Order order = new Order();

        [Fact]
        public void CreateOrderTest1()
        {
            order.CustomerInfo.CustomerName = "test1";
            order.CustomerInfo.Phone = "0836984306";
            order.CustomerInfo.Address = "fasjfjak";
            order.Seller.StaffId = 1;
            order.Laptops.Add(new Laptop { LaptopId = 1, Quantity = 1 });
            order.Laptops.Add(new Laptop { LaptopId = 2, Quantity = 1 });
            bool result = new OrderDAL().CreateOrder(order);
            Assert.True(result == true);
        }
        [Fact]
        public void CreateOrderTest2()
        {
            order.CustomerInfo.CustomerName = "test2";
            order.CustomerInfo.Phone = "0836984303";
            order.CustomerInfo.Address = "fasjfjak";
            order.Seller.StaffId = 2;
            order.Laptops.Add(new Laptop { LaptopId = 4, Quantity = 100 });
            bool result = new OrderDAL().CreateOrder(order);
            Assert.True(result == false);
        }
    }
}