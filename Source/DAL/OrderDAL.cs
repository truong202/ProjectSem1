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
                order.CustomerInfo == null || order.CustomerInfo.Phone == null || order.CustomerInfo.Phone == "" ||
                order.CustomerInfo.Name == null || order.CustomerInfo.Name == "" ||
                order.Seller == null || order.Seller.ID == null) return false;
            bool result = false;
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
                        order.CustomerInfo.ID = reader.GetInt32("customer_id");
                    }
                    reader.Close();
                    if (order.CustomerInfo.ID == null)
                    {
                        command.CommandText = "call sp_createCustomer(@customerName, @address, @phone);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@customerName", order.CustomerInfo.Name);
                        command.Parameters.AddWithValue("@address", order.CustomerInfo.Address ?? "");
                        command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone);
                        command.ExecuteNonQuery();

                        command.CommandText = "call sp_getNewCustomerId();";
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            order.CustomerInfo.ID = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }

                    command.CommandText = "call sp_createOrder(@customerId, @sellerId, @orderStatus);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@customerId", order.CustomerInfo.ID);
                    command.Parameters.AddWithValue("@sellerId", order.Seller.ID);
                    command.Parameters.AddWithValue("@orderStatus", Order.UNPAID);
                    command.ExecuteNonQuery();

                    command.CommandText = "call sp_getNewOrderId();";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        order.ID = reader.GetInt32("order_id");
                    }
                    reader.Close();

                    foreach (var laptop in order.Laptops)
                    {
                        if (laptop.Quantity <= 0)
                        {
                            throw new Exception("Not Exists Item");
                        }
                        command.CommandText = "call sp_getPrice(@laptopId);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@laptopId", laptop.ID);
                        reader = command.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Not Exists Item");
                        }
                        laptop.Price = reader.GetDecimal("price");
                        reader.Close();

                        command.CommandText = "call sp_insertToOrderDetails(@orderId, @laptopId, @price, @quantity);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@orderId", order.ID);
                        command.Parameters.AddWithValue("@laptopId", laptop.ID);
                        command.Parameters.AddWithValue("@price", laptop.Price);
                        command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                        command.ExecuteNonQuery();

                        command.CommandText = "call sp_updateQuantityInLaptopsAfterCreateOrder(@quantity, @laptopId);";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@quantity", laptop.Quantity);
                        command.Parameters.AddWithValue("@laptopId", laptop.ID);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    try { transaction.Rollback(); }
                    catch { }
                }
                finally
                {
                    command.CommandText = "unlock tables;";
                    command.ExecuteNonQuery();
                }
            }
            catch {}
            finally
            {
                try { connection.Close(); } catch { }
            }
            return result;
        }
        public bool Payment(Order order)
        {
            if (order == null) return false;
            bool result = false;
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
                    command.CommandText = "call sp_updateOrderAfterPayment(@accountantId, @orderStatus, @orderId);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@accountantId", order.Accountant.ID);
                    command.Parameters.AddWithValue("@orderId", order.ID);
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
                            command.Parameters.AddWithValue("@laptopId", laptop.ID);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    try { transaction.Rollback(); } catch { }
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
                try { connection.Close(); } catch { }
            }
            return result;
        }
        public List<Order> GetOrdersUnpaid()
        {
            List<Order> orders = new List<Order>();
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
                LaptopDAL laptopDAL = new LaptopDAL();
                command.CommandText = "call sp_getLaptopsInOrder(@orderId);";
                for (int i = 0; i < orders.Count; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@orderId", orders[i].ID);
                    using (reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders[i].Laptops.Add(laptopDAL.GetLaptop(reader));
                        }
                    }
                }
            }
            catch { }
            finally
            {
                try { connection.Close(); } catch { }
            }
            if (orders.Count == 0) orders = null;
            return orders;
        }
        public bool ChangeStatus(int status, int orderId, int staffId)
        {
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "sp_getOrderById(@orderId);";
                command.Parameters.AddWithValue("@orderId", orderId);
                reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    throw new Exception("Can't find Order!");
                }
                reader.Close();

                command.CommandText = "sp_getStaffById(@staffId);";
                command.Parameters.AddWithValue("@staffId", staffId);
                reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    throw new Exception("Can't find Staff!");
                }
                reader.Close();

                command.CommandText = "call sp_changeOrderStatus(@status, @orderId, @staffId);";
                command.Parameters.AddWithValue("@status", status);
                command.ExecuteNonQuery();
                result = true;
            }
            catch { }
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
            }
            catch { }
            finally
            {
                try { connection.Close(); } catch { }
            }
            return order;
        }
        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            StaffDAL staffDAL = new StaffDAL();
            order.ID = reader.GetInt32("order_id");
            order.CustomerInfo = new CustomerDAL().GetCustomer(reader);
            order.Seller.ID = reader.GetInt32("seller_id");
            order.Seller.Name = reader.GetString("seller_name");
            try
            {
                order.Accountant.ID = reader.GetInt32("accountant_id");
                order.Accountant.Name = reader.GetString("accountant_name");
            }
            catch { }
            order.Date = reader.GetDateTime("order_date");
            order.Status = reader.GetInt32("order_status");
            return order;
        }
    }
}