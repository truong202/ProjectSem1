using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbHelper
    {
        private static string connectionString = "server=localhost;user id=laptop;password=vtcacademy;port=3306;database=laptop_store;";
        private static MySqlConnection connection;

        private DbHelper() { }

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
            }
            return connection;
        }
    }
}