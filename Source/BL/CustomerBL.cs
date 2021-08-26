using System;
using Persistance;
using DAL;

namespace DAL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL = new CustomerDAL();

        public Customer GetByPhone(string phone)
        {
            return customerDAL.GetByPhone(phone);
        }
    }
}