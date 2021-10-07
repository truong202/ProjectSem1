using System;
using Xunit;
using Persistance;
using DAL;
using System.Collections.Generic;

namespace TestDAL
{
    public class CreateOrderTest
    {
        private OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateOrderTest1()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus1", Phone = "0836984111", Address = "Ha Noi" },
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 26, Quantity =1},
                            new Laptop { ID = 11, Quantity =1},
                            new Laptop { ID = 22, Quantity =1},
                            new Laptop { ID = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == true);
        }

        [Fact]
        public void CreateOrderTest2()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus2", Phone = "0836984222", Address = "Ha Noi" },
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 26, Quantity =1},
                            new Laptop { ID = 23, Quantity =1},
                            new Laptop { ID = 2, Quantity =1},
                            new Laptop { ID = 20, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == true);
        }

        [Fact]
        public void CreateOrderTest3()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus3", Phone = "0836984333", Address = "Ha Noi" },
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 4, Quantity =1},
                            new Laptop { ID = 12, Quantity =1},
                            new Laptop { ID = 15, Quantity =1},
                            new Laptop { ID = 18, Quantity =-4}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest4()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus4", Phone = "0836984344", Address = "Ha Noi" },
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 1000, Quantity =1},
                            new Laptop { ID = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest5()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus5", Phone = "0836984355", Address = "Ha Noi" },
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 6, Quantity =1},
                            new Laptop { ID = 7, Quantity =1000}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest6()
        {
            Order order = null;
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest7()
        {
            Order order = new Order
            {
                Seller = new Staff { ID = 1 },
                Laptops = { new Laptop { ID = 6, Quantity =1},
                            new Laptop { ID = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest8()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { Name = "cus5", Phone = "0836984355", Address = "Ha Noi" },
                Laptops = { new Laptop { ID = 1, Quantity =1},
                            new Laptop { ID = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest9()
        {
            Order order = new Order
            {
                Seller = new Staff { ID = 1 },
                CustomerInfo = new Customer { Name = "cus5", Phone = "0836984355", Address = "Ha Noi" },
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest10()
        {
            Order order = new Order
            {
                Seller = new Staff { ID = 1 },
                CustomerInfo = new Customer { Name = "cus5", Phone = "0836984355", Address = "Ha Noi" },
                Laptops = { new Laptop { ID = 1, Quantity =1},
                            new Laptop { ID = 1, Quantity =2}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

    }
}
