using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OrderDAL
    {
        // private MySqlConnection connection;
        // private MySqlDataReader reader;

        public bool CreateOrder(Order order)
        {
            bool result = false;

            return result;
        }
        public bool ChangeStatus(int status)
        {
            bool result = false;

            return result;
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            return orders;
        }
        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();

            return order;
        }
    }
}