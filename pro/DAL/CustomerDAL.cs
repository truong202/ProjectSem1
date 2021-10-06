using System;
using Persistance;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CustomerDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Customer GetByPhone(string phone)
        {
            Customer customer = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getCustomerByPhone(@phone)", connection);
                    command.Parameters.AddWithValue("@phone", phone);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            customer = GetCustomer(reader);
                    }
                    connection.Close();
                }
                catch { }
            }
            return customer;
        }
        internal Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.CustomerId = reader.GetInt32("customer_id");
            customer.CustomerName = reader.GetString("customer_name");
            customer.Phone = reader.GetString("phone");
            customer.Address = reader.GetString("address");
            return customer;
        }
    }
}