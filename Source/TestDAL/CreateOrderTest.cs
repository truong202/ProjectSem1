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
                CustomerInfo = new Customer { CustomerName = "cus1", Phone = "0836984111", Address = "Ha Noi" },
                Seller = new Staff { StaffId = 1 },
                Laptops = { new Laptop { LaptopId = 6, Quantity =1},
                            new Laptop { LaptopId = 11, Quantity =2},
                            new Laptop { LaptopId = 22, Quantity =1},
                            new Laptop { LaptopId = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == true);
        }

        [Fact]
        public void CreateOrderTest2()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { CustomerName = "cus2", Phone = "0836984222", Address = "Ha Noi" },
                Seller = new Staff { StaffId = 1 },
                Laptops = { new Laptop { LaptopId = 26, Quantity =1},
                            new Laptop { LaptopId = 23, Quantity =2},
                            new Laptop { LaptopId = 2, Quantity =1},
                            new Laptop { LaptopId = 20, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == true);
        }

        [Fact]
        public void CreateOrderTest3()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { CustomerName = "cus3", Phone = "0836984333", Address = "Ha Noi" },
                Seller = new Staff { StaffId = 1 },
                Laptops = { new Laptop { LaptopId = 4, Quantity =1},
                            new Laptop { LaptopId = 12, Quantity =2},
                            new Laptop { LaptopId = 15, Quantity =2},
                            new Laptop { LaptopId = 18, Quantity =2}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == true);
        }

        [Fact]
        public void CreateOrderTest4()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { CustomerName = "cus4", Phone = "0836984344", Address = "Ha Noi" },
                Seller = new Staff { StaffId = 1 },
                Laptops = { new Laptop { LaptopId = 1000, Quantity =1},
                            new Laptop { LaptopId = 7, Quantity =1}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }

        [Fact]
        public void CreateOrderTest5()
        {
            Order order = new Order
            {
                CustomerInfo = new Customer { CustomerName = "cus5", Phone = "0836984355", Address = "Ha Noi" },
                Seller = new Staff { StaffId = 1 },
                Laptops = { new Laptop { LaptopId = 6, Quantity =1},
                            new Laptop { LaptopId = 7, Quantity =1000}}
            };
            bool result = orderDAL.CreateOrder(order);
            Assert.True(result == false);
        }
    }
}

// using System;
// using Xunit;
// using Persistance;
// using DAL;
// using System.Collections.Generic;

// namespace TestDAL
// {
//     public class CreateOrderTest
//     {
//         private static Customer customer1 = new Customer
//         {
//             CustomerName = "CustomerA",
//             Phone = "0836984302",
//             Address = "Bac Giang"
//         };
//         private static Customer customer2 = new Customer
//         {
//             CustomerName = "CustomerB",
//             Phone = "0836984301",
//             Address = "Bac Giang"
//         };

//         private static Order order1 = new Order
//         {
//             CustomerInfo = customer1,
//             Laptops = { new Laptop { LaptopId = 2, Quantity = 1 } },
//             Seller = new Staff { StaffId = 1 }
//         };
//         private static Order order2 = new Order
//         {
//             CustomerInfo = customer1,
//             Laptops = { new Laptop { LaptopId = 20, Quantity = 2 } },
//             Seller = new Staff { StaffId = 2 }
//         };
//         private static Order order3 = new Order
//         {
//             CustomerInfo = customer2,
//             Laptops = { new Laptop { LaptopId = 7, Quantity = 5 } },
//             Seller = new Staff { StaffId = 3 }
//         };
//         private static Order order4 = new Order()
//         {
//             CustomerInfo = customer2,
//             Laptops = { new Laptop { LaptopId = 7, Quantity = 100 } },
//             Seller = new Staff { StaffId = 3 }
//         };
//         private static Order order5 = new Order
//         {
//             CustomerInfo = customer2,
//             Laptops = { new Laptop { LaptopId = 6, Quantity = 1 } },
//             Seller = new Staff { StaffId = null }
//         };
//         public static IEnumerable<object[]> SplitCountData
//         {
//             get
//             {
//                 return new[]
//                 {
//                     new object[] {order1, true},
//                     new object[] {order2, true},
//                     new object[] {order3, true},
//                     new object[] {order4, false},
//                     new object[] {order5, false}
//                 };
//             }
//         }

//         // [Fact]
//         [Theory, MemberData(nameof(SplitCountData))]
//         public void CreateOrderTest1(Order order, bool expected)
//         {
//             bool result = new OrderDAL().CreateOrder(order);
//             Assert.True(result == expected);
//         }
//     }
// }