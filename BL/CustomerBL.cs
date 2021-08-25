using System;
using Persistance;
using DAL;

namespace DAL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL = new CustomerDAL();

        public Customer GetCustomer(string phone)
        {
            return customerDAL.GetCustomer(phone);
        }
    }
}