using System;
using Persistance;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CustomerDAL
    {
        public Customer GetCustomer(string phone)
        {
            Customer customer = new Customer();   
            try
            {
                MySqlConnection connection = DbHelper.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand("call sp_getCustomer(@phone)", connection);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters["@phone"].Direction = System.Data.ParameterDirection.Input;
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    customer = GetCustomer(reader);
                }
                reader.Close();
                connection.Close();
            }
            catch
            {
                throw new Exception("Đã có lỗi xảy ra!");
            }
            return customer;
        }
        private Customer GetCustomer(MySqlDataReader reader)
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