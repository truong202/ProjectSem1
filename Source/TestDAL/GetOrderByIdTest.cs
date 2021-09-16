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

        private static Customer[] Customers =
            new[] { new Customer { CustomerId = 1, CustomerName = "Phạm Công Hưng", Phone = "0904844014" , Address = "Nam Định"},
                    new Customer { CustomerId = 2, CustomerName = "Phạm Công Hà", Phone = "0906450904", Address = "Nam Định" }};

        private static Order order1 = new Order
        {

            OrderId = 1,
            CustomerInfo = Customers[1],
            
            // Date = "2021-09-16 20:45:46"

        };
        
    }
}