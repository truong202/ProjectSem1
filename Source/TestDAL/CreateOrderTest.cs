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