using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;

        public bool CreateOrder(Order order)
        {
            if (order == null || order.Laptops == null || order.Laptops.Count == 0 ||
                order.CustomerInfo == null || order.Seller.StaffId == null) return false;
            bool result = true;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = "lock tables customers write, orders write, laptops write, order_details write;";
                    command.ExecuteNonQuery();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = "call sp_getCustomerByPhone(@phone);";
                        command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            order.CustomerInfo.CustomerId = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                        if (order.CustomerInfo.CustomerId == null)
                        {
                            //Insert new Customer
                            command.CommandText = "call sp_createCustomer(@customerName, @address, @phone);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@customerName", order.CustomerInfo.CustomerName);
                            command.Parameters.AddWithValue("@address", order.CustomerInfo.Address ?? "");
                            command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone);
                            command.ExecuteNonQuery();
                            //Get new customer id
                            command.CommandText = "call sp_getNewCustomerId();";
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                order.CustomerInfo.CustomerId = reader.GetInt32("customer_id");
                            }
                            reader.Close();
                            if (order.CustomerInfo == null || order.CustomerInfo.CustomerId == null)
                            {
                                throw new Exception("Can't find Customer!");
                            }
                        }
                        //Insert Order
                        command.CommandText = "call sp_createOrder(@customerId, @sellerId, @orderStatus);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@customerId", order.CustomerInfo.CustomerId);
                        command.Parameters.AddWithValue("@sellerId", order.Seller.StaffId);
                        command.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATED);
                        command.ExecuteNonQuery();
                        //get new Order_ID
                        // cmd.CommandText = "select order_id from Orders order by order_id desc limit 1;";
                        command.CommandText = "call sp_getNewOrderId();";
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            order.OrderId = reader.GetInt32("order_id");
                        }
                        reader.Close();
                        //insert Order Details table
                        foreach (var laptop in order.Laptops)
                        {
                            if (laptop.Quantity <= 0)
                            {
                                throw new Exception("Not Exists Item");
                            }
                            //get unit_price
                            command.CommandText = "call sp_getPrice(@laptopId);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            reader = command.ExecuteReader();
                            if (!reader.Read())
                            {
                                throw new Exception("Not Exists Item");
                            }
                            laptop.Price = reader.GetDecimal("price");
                            reader.Close();

                            //insert to Order Details
                            command.CommandText = "call sp_insertToOrderDetails(@orderId, @laptopId, @price, @quantity);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@orderId", order.OrderId);
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            command.Parameters.AddWithValue("@price", laptop.Price);
                            command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                            command.ExecuteNonQuery();

                            //update quantity in laptops
                            command.CommandText = "call sp_updateQuantityInLaptops(@quantity, @laptopId);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception )
                    {
                        // Console.WriteLine(e.Message);
                        result = false;
                        try { transaction.Rollback(); }
                        catch { }
                    }
                    finally
                    {
                        //unlock all tables;
                        command.CommandText = "unlock tables;";
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch { }
            }
            return result;
        }
        public bool ChangeStatus(int status)
        {
            bool result = false;

            return result;
        }
        public Order GetOrderById(int orderId)
        {
            Order order = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getOrdersById(@orderId)", connection);
                    command.Parameters.AddWithValue("@orderId", orderId);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order = new Order();
                        order = GetOrder(reader);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                }
            }
            return order;
        }

         public int GetOrderCount(string searchValue)
        {
            int result = 0;
            lock (connection)
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getOrderCount(@searchValue)", connection);
                    command.Parameters.AddWithValue("@searchValue", searchValue);
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
                }
            return result;
        }

     
        public List<Order> GetOrders(string searchValue, int offset)
        {
            List<Order> orders = new List<Order>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getOrders(@searchValue, @offset)", connection);
                    command.Parameters.AddWithValue("@searchValue", searchValue);
                    command.Parameters.AddWithValue("@offset", offset);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(GetOrder(reader));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                }
            }
            if (orders.Count == 0) orders = null;
            return orders;

        }

        private Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderId = reader.GetInt32("order_id");
            order.CustomerInfo.CustomerName = reader.GetString("customer_name");
            order.CustomerInfo.Phone = reader.GetString("phone");
            order.Date = reader.GetDateTime("order_date");
            order.Status = reader.GetInt32("order_status");
            order.Seller.StaffId = reader.GetInt32("seller_id");
            or
            try{
            order.Accountance.StaffId = reader.GetInt32("accountance_id");
            }catch{
                order.Accountance.StaffId = null;
            }
            return order;
        }
    }
}