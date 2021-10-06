using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class LaptopDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public List<Laptop> Search(string searchValue, int offset)
        {
            List<Laptop> laptops = new List<Laptop>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_searchLaptops(@searchValue, @offset)", connection);
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@offset", offset);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            laptops.Add(GetLaptop(reader));
                    }
                    connection.Close();
                }
                catch { }
            }
            if (laptops.Count == 0) laptops = null;
            return laptops;

        }
        public Laptop GetById(int laptopId)
        {
            Laptop laptop = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_GetLaptopById(@laptopId)", connection);
                    command.Parameters.AddWithValue("@laptopId", laptopId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            laptop = GetLaptop(reader);
                    }
                    connection.Close();
                }
                catch { }
            }
            return laptop;
        }
        public int GetCount(string searchValue)
        {
            int result = 0;
            lock (connection)
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getCount(@searchValue)", connection);
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            result = reader.GetInt32("count");
                    }
                    connection.Close();
                }
                catch { }
            return result;
        }
        protected internal Laptop GetLaptop(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.LaptopId = reader.GetInt32("laptop_id");
            laptop.LaptopName = reader.GetString("laptop_name");
            laptop.CategoryInfo.Name = reader.GetString("category_name");
            laptop.ManufactoryInfo.Name = reader.GetString("manufactory_name");
            laptop.ManufactoryInfo.Website = reader.GetString("website");
            laptop.ManufactoryInfo.Address = reader.GetString("address");
            laptop.CPU = reader.GetString("CPU");
            laptop.Ram = reader.GetString("Ram");
            laptop.HardDrive = reader.GetString("hard_drive");
            laptop.VGA = reader.GetString("VGA");
            laptop.Display = reader.GetString("display");
            laptop.Battery = reader.GetString("battery");
            laptop.Weight = reader.GetString("weight");
            laptop.Materials = reader.GetString("materials");
            laptop.Ports = reader.GetString("ports");
            laptop.NetworkAndConnection = reader.GetString("network_and_connection");
            laptop.Security = reader.GetString("security");
            laptop.Keyboard = reader.GetString("keyboard");
            laptop.Audio = reader.GetString("audio");
            laptop.Size = reader.GetString("size");
            laptop.OS = reader.GetString("OS");
            laptop.Quantity = reader.GetInt32("quantity");
            laptop.Price = reader.GetDecimal("price");
            laptop.WarrantyPeriod = reader.GetString("warranty_period");
            return laptop;
        }
    }
}
