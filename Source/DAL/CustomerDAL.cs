using System;
using Persistance;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CustomerDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        MySqlDataReader reader;
        public Customer GetByPhone(string phone)
        {
            Customer customer = null;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("call sp_getCustomerByPhone(@phone)", connection);
                command.Parameters.AddWithValue("@phone", phone);
                using (reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        customer = GetCustomer(reader);
                }
            }
            catch { }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return customer;
        }
        internal Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.ID = reader.GetInt32("customer_id");
            customer.Name = reader.GetString("customer_name");
            customer.Phone = reader.GetString("phone");
            customer.Address = reader.GetString("address");
            return customer;
        }
    }
}