using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class LaptopDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        private MySqlDataReader reader;
        public List<Laptop> Search(string searchValue, int offset)
        {
            List<Laptop> laptops = new List<Laptop>();
            lock (connection)
                try
                {
                    // connection = DbHelper.GetConnection();
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getLaptops(@searchValue, @offset)", connection);
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters["@searchValue"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@offset", offset);
                    command.Parameters["@offset"].Direction = System.Data.ParameterDirection.Input;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        laptops.Add(GetLaptop(reader));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                    throw new Exception("Không thể kết nối đến database!");
                }
            return laptops;

        }
        public Laptop GetById(int laptopId)
        {
            Laptop laptop = null;
            lock (connection)
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_GetLaptopById(@laptopId)", connection);
                    command.Parameters.AddWithValue("@laptopId", laptopId);
                    command.Parameters["@laptopId"].Direction = System.Data.ParameterDirection.Input;
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        laptop = new Laptop();
                        laptop = GetLaptop(reader);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                    throw new Exception("Không thể kết nối đến database!");
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
                    command.Parameters["@searchValue"].Direction = System.Data.ParameterDirection.Input;
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader.GetInt32("count");
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                    throw new Exception("Không thể kết nối đến database!");
                }
            return result;
        }
        private Laptop GetLaptop(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.LaptopId = reader.GetInt32("laptop_id");
            laptop.LaptopName = reader.GetString("laptop_name");
            laptop.CategoryInfor.CategoryName = reader.GetString("category_name");
            laptop.ManufactoryInfor.ManufactoryName = reader.GetString("manufactory_name");
            laptop.ManufactoryInfor.Website = reader.GetString("website");
            laptop.ManufactoryInfor.Address = reader.GetString("address");
            laptop.CPU = reader.GetString("CPU");
            laptop.Ram = reader.GetString("Ram");
            laptop.HardDrive = reader.GetString("hard_drive");
            laptop.VGA = reader.GetString("VGA");
            laptop.Display = reader.GetString("display");
            laptop.Battery = reader.GetString("battery");
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
            return laptop;
        }
    }
}
