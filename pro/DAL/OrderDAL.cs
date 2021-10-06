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
            bool result = false;
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
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            order.CustomerInfo.CustomerId = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                        if (order.CustomerInfo.CustomerId == null)
                        {
                            command.CommandText = "call sp_createCustomer(@customerName, @address, @phone);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@customerName", order.CustomerInfo.CustomerName);
                            command.Parameters.AddWithValue("@address", order.CustomerInfo.Address ?? "");
                            command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone);
                            command.ExecuteNonQuery();
                            command.CommandText = "call sp_getNewCustomerId();";
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                order.CustomerInfo.CustomerId = reader.GetInt32("customer_id");
                            }
                            reader.Close();
                        }

                        command.CommandText = "call sp_createOrder(@customerId, @sellerId, @orderStatus);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@customerId", order.CustomerInfo.CustomerId);
                        command.Parameters.AddWithValue("@sellerId", order.Seller.StaffId);
                        command.Parameters.AddWithValue("@orderStatus", Order.UNPAID);
                        command.ExecuteNonQuery();
                        command.CommandText = "call sp_getNewOrderId();";
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            order.OrderId = reader.GetInt32("order_id");
                        }
                        reader.Close();
                        foreach (var laptop in order.Laptops)
                        {
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

                            command.CommandText = "call sp_insertToOrderDetails(@orderId, @laptopId, @price, @quantity);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@orderId", order.OrderId);
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            command.Parameters.AddWithValue("@price", laptop.Price);
                            command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                            command.ExecuteNonQuery();

                            command.CommandText = "call sp_updateQuantityInLaptopsAfterCreateOrder(@quantity, @laptopId);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        result = true;
                    }
                    catch
                    {
                        result = false;
                        try { transaction.Rollback(); }
                        catch { }
                    }
                    finally
                    {
                        command.CommandText = "unlock tables;";
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch { }
                }
            }
            return result;
        }
        public bool ChangeStatus(int status, int orderId, int staffId)
        {
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("call sp_changeOrderStatus(@status, @orderId, @staffId);", connection);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@orderId", orderId);
                command.Parameters.AddWithValue("@staffId", staffId);
                command.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return result;
        }
        public Order GetById(int orderId)
        {
            Order order = null;
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("call sp_getOrderById(@orderId);", connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    order = GetOrder(reader);
                    reader.Close();
                    command.CommandText = "call sp_getLaptopsInOrder(@orderId);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderId", orderId);
                    reader = command.ExecuteReader();
                    LaptopDAL laptopDAL = new LaptopDAL();
                    while (reader.Read())
                        order.Laptops.Add(laptopDAL.GetLaptop(reader));
                    reader.Close();
                }
                // connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return order;
        }


        public bool ConfirmPayment(Order order)
        {
            bool result = false;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_confirmPayment(@orderStatus, @orderId, @accountanceId)", connection);
                    command.Parameters.AddWithValue("@orderStatus", Order.PAID);
                    command.Parameters.AddWithValue("@orderId", order.OrderId);
                    command.Parameters.AddWithValue("@sellerId", order.Accountance.StaffId);
                    command.ExecuteNonQuery();
                    result = true;
                    connection.Close();
                }
                catch { }
            }
            return result;
        }
        public bool Payment(Order order)
        {
            if (order == null) return false;
            bool result = false;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = "lock tables orders write;";
                    command.ExecuteNonQuery();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = "call sp_updateOrderAfterPayment(@accountanceId, @orderStatus, @orderId);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@accountanceId", order.Accountance.StaffId);
                        command.Parameters.AddWithValue("@orderId", order.OrderId);
                        if (order.Status == Order.PAID)
                        {
                            command.Parameters.AddWithValue("@orderStatus", Order.PAID);
                            command.ExecuteNonQuery();
                        }
                        else if (order.Status == Order.CANCEL)
                        {
                            command.Parameters.AddWithValue("@orderStatus", Order.CANCEL);
                            command.ExecuteNonQuery();
                            foreach (var laptop in order.Laptops)
                            {
                                command.CommandText = "call sp_updateQuantityInLaptopsAfterCancelOrder(@quantity, @laptopId);";
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                                command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                        try
                        {
                            transaction.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        command.CommandText = "unlock tables;";
                        command.ExecuteNonQuery();
                        // connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();

                }
                finally
                {
                    try { connection.Close(); } catch { }
                }
            }
            return result;
        }

        public bool CancelPayment(Order order)
        {
            bool result = false;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = "lock tables orders write, laptops write;";
                    command.ExecuteNonQuery();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;
                    try
                    {
                        command.CommandText = "call sp_changeOrderStatus(@orderStatus, @orderId);";
                        command.Parameters.AddWithValue("@orderStatus", Order.CANCEL);
                        command.Parameters.AddWithValue("@orderId", order.OrderId);
                        command.ExecuteNonQuery();
                        foreach (var laptop in order.Laptops)
                        {
                            command.CommandText = "call sp_updateQuantityInLaptopsAf(@quantity, @laptopId);";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                            command.Parameters.AddWithValue("@laptopId", laptop.LaptopId);
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        result = false;
                        try { transaction.Rollback(); }
                        catch { }
                    }
                    finally
                    {
                        command.CommandText = "unlock tables;";
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch { }
            }
            return result;
        }
        public List<Order> GetOrdersUnpaid()
        {
            List<Order> orders = new List<Order>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("call sp_getOrdersUnpaid();", connection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        orders.Add(GetOrder(reader));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch { }
            }
            if (orders.Count == 0) orders = null;
            return orders;
        }

        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            StaffDAL staffDAL = new StaffDAL();
            order.OrderId = reader.GetInt32("order_id");
            order.CustomerInfo = new CustomerDAL().GetCustomer(reader);
            order.Seller.StaffId = reader.GetInt32("seller_id");
            order.Seller.StaffName = reader.GetString("seller_name");
            try
            {
                order.Accountance.StaffId = reader.GetInt32("accountance_id");
            }
            catch { }
            order.Accountance.StaffName = reader.GetString("accountance_name");
            order.Date = reader.GetDateTime("order_date");
            order.Status = reader.GetInt32("order_status");
            return order;
        }

        private Laptop GetLaptopInOrder(MySqlDataReader reader)
        {
            Laptop laptop = new Laptop();
            laptop.LaptopId = reader.GetInt32("laptop_id");
            laptop.LaptopName = reader.GetString("laptop_name");
            laptop.Quantity = reader.GetInt32("quantity");
            laptop.Price = reader.GetDecimal("unit_price");
            return laptop;
        }
    }
}