using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class LaptopDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public List<Laptop> Search(string searchValue)
        {
            List<Laptop> laptops = new List<Laptop>();
            string[] filters = searchValue.Split('#');
            string filter = GetFilter(filters);
            if (filter == null || filter == "") return null;
            string query = @"SELECT l.laptop_id, l.laptop_name, c.category_id, c.category_name, m.manufactory_id,
                m.manufactory_name, IFNULL(m.website, '') AS website, IFNULL(m.address, '') AS address, l.CPU, l.Ram,
                l.hard_drive, l.VGA, l.display, l.battery, l.weight,l.materials, l.ports, l.network_and_connection,
                l.security, l.keyboard, l.audio, l.size, l.warranty_period, l.OS, l.price, l.quantity
                FROM
                    laptops l
                        INNER JOIN categories c ON l.category_id = c.category_id
                        INNER JOIN manufactories m ON l.manufactory_id = m.manufactory_id
                WHERE ";
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                switch (filter)
                {
                    case "SEARCH":
                        query += @"l.laptop_name LIKE CONCAT('%', @searchValue, '%') OR c.category_name LIKE CONCAT('%', @searchValue, '%') OR
                            m.manufactory_name LIKE  CONCAT('%', @searchValue, '%') OR l.laptop_id = @searchValue ORDER BY l.laptop_id;";
                        command.Parameters.AddWithValue("@searchValue", searchValue.Trim());
                        break;
                    case "SEARCH_DESC":
                    case "SEARCH_ASC":
                        query += @"l.laptop_name LIKE CONCAT('%', @searchValue, '%') OR c.category_name LIKE CONCAT('%', @searchValue, '%') OR
                            m.manufactory_name LIKE  CONCAT('%', @searchValue, '%') ORDER BY l.price " + (filter.Equals("SEARCH_DESC") ? "DESC;" : ";");
                        command.Parameters.AddWithValue("@searchValue", filters[0].Trim());
                        break;
                    case "MANUFACTORY_CATEGORY":
                        query += @"m.manufactory_name LIKE  CONCAT('%', @value1, '%') AND c.category_name LIKE CONCAT('%', @value2, '%') OR
                                m.manufactory_name LIKE  CONCAT('%', @value2, '%') AND c.category_name LIKE CONCAT('%', @value1, '%')
                                ORDER BY l.laptop_id;";
                        command.Parameters.AddWithValue("@value1", filters[0].Trim());
                        command.Parameters.AddWithValue("@value2", filters[1].Trim());
                        break;
                    case "MANUFACTORY_CATEGORY_DESC":
                    case "MANUFACTORY_CATEGORY_ASC":
                        query += @"m.manufactory_name LIKE  CONCAT('%', @value1, '%') AND c.category_name LIKE CONCAT('%', @value2, '%') OR
                                m.manufactory_name LIKE  CONCAT('%', @value2, '%') AND c.category_name LIKE CONCAT('%', @value1, '%')
                                ORDER BY l.price " + (filter.Equals("MANUFACTORY_CATEGORY_DESC") ? "DESC;" : ";");
                        command.Parameters.AddWithValue("@value1", filters[0].Trim());
                        command.Parameters.AddWithValue("@value2", filters[1].Trim());
                        break;
                }
                command.CommandText = query;
                laptops = GetLaptops(command);
            }
            catch { }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return laptops;
        }
        public Laptop GetById(int laptopId)
        {
            Laptop laptop = null;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("call sp_GetLaptopById(@laptopId)", connection);
                command.Parameters.AddWithValue("@laptopId", laptopId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        laptop = new Laptop();
                        laptop = GetLaptop(reader);
                    }
                }
            }
            catch { }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return laptop;
        }
        public List<Laptop> GetLaptops(MySqlCommand command)
        {
            List<Laptop> laptops = new List<Laptop>();
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    laptops.Add(GetLaptop(reader));
                }
            }
            if (laptops.Count == 0) return null;
            return laptops;
        }
        internal Laptop GetLaptop(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.ID = reader.GetInt32("laptop_id");
            laptop.Name = reader.GetString("laptop_name");
            laptop.CategoryInfo.Name = reader.GetString("category_name");
            laptop.ManufactoryInfo.Name = reader.GetString("manufactory_name");
            laptop.CPU = reader.GetString("CPU");
            laptop.Ram = reader.GetString("Ram");
            laptop.HardDrive = reader.GetString("hard_drive");
            laptop.VGA = reader.GetString("VGA");
            laptop.Display = reader.GetString("display");
            laptop.Battery = reader.GetString("battery");
            laptop.Weight = reader.GetFloat("weight");
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
        private string GetFilter(string[] filters)
        {
            string filter = "";
            int count = filters.Length;
            if (count == 1)
            {
                filter = "SEARCH";
            }
            else if (count == 2)
            {
                if (filters[count - 1].ToUpper().Trim().Equals("DESC") || filters[count - 1].ToUpper().Trim().Equals("ASC"))
                    filter = "SEARCH_" + filters[count - 1].ToUpper().Trim();
                else
                    filter = "MANUFACTORY_CATEGORY";
            }
            else if (count == 3)
            {
                filter = "MANUFACTORY_CATEGORY_";
                if (filters[count - 1].ToUpper().Trim().Equals("DESC") || filters[count - 1].ToUpper().Trim().Equals("ASC"))
                    filter += filters[count - 1].ToUpper().Trim();
                else filter = "";
            }
            else
            {
                filter = "";
            }
            return filter;
        }
    }
}
