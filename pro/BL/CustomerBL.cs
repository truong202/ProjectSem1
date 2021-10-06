using System;
using Persistance;
using DAL;

namespace BL
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