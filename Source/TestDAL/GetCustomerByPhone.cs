using System;
using Xunit;
using DAL;
using Persistance;
using System.Collections.Generic;

namespace TestDAL
{
    public class GetCustomerByPhoneTest
    {
        private static Customer customer1 = new Customer
        {
            CustomerId = 3,
            CustomerName = "Customer1",
            Phone = "0836984311",
            Address = "Ho Chi Minh"
        };
        private static Customer customer2 = new Customer
        {
            CustomerId = 4,
            CustomerName = "Customer2",
            Phone = "0836984312",
            Address = "Bac Giang"
        };
        private static Customer customer3 = new Customer
        {
            CustomerId = 5,
            CustomerName = "Customer3",
            Phone = "0836984313",
            Address = "Ha Noi"
        };
        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                return new[]
                {
                    new object[] {"0836984311", customer1},
                    new object[] {"0836984312", customer2},
                    new object[] {"0836984313", customer3},
                    new object[] {"", null},
                    new object[] {"0836984310", null}
                };
            }
        }
        CustomerDAL cusDAL = new CustomerDAL();
        [Theory, MemberData(nameof(SplitCountData))]
        public void GetCustomerByPhoneTest1(string phone, Customer expected)
        {
            Customer result = cusDAL.GetByPhone(phone);
            Assert.Equal(result, expected);
        }
    }
}