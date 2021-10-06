using System;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DbConfig
    {
        private static MySqlConnection connection;

        private DbConfig() { }

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection()
                {
                    ConnectionString = GetConString()
                };
            }
            // connection.ConnectionString = conString;
            return connection;
        }
        private static string GetConString()
        {
            try
            {
                string conString;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead("DbConfig.txt"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        conString = reader.ReadLine();
                    }
                }
                return conString;
            }
            catch
            {
                return "server=localhost;user id=laptop4;password=vtcacademy;port=3306;database=laptop_store1;";
            }
        }
    }
}