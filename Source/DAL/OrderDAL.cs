using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        private MySqlDataReader reader;

        public bool CreateOrder(Order order)
        {
            bool result = true;
            // try
            // {
            //     connection.Open();
            //     MySqlCommand command = connection.CreateCommand();
            //     command.Connection = connection;
            //     command.CommandText = "lock tables customers write, orders write, laptops write, order_details write;";
            //     command.ExecuteNonQuery();
            //     MySqlTransaction trans = connection.BeginTransaction();
            //     command.Transaction = trans;
            //     try
            //     {
            //         //Insert new Customer
            //         command.CommandText = "call sp_createCustomer(@customerName, @address, @phone);";
            //         command.Parameters.AddWithValue("@customerName", order.CustomerInfo.CustomerName ?? "");
            //         command.Parameters["@customerName"].Direction = System.Data.ParameterDirection.Input;
            //         command.Parameters.AddWithValue("@address", order.CustomerInfo.Address ?? "");
            //         command.Parameters["@address"].Direction = System.Data.ParameterDirection.Input;
            //         command.Parameters.AddWithValue("@phone", order.CustomerInfo.Phone ?? "");
            //         command.Parameters["@phone"].Direction = System.Data.ParameterDirection.Input;
            //         command.ExecuteNonQuery();
            //         //Get new customer id
            //         command.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
            //         reader = command.ExecuteReader();
            //         if (reader.Read())
            //         {
            //             order.CustomerInfo.CustomerId = reader.GetInt32("customer_id");
            //         }
            //         reader.Close();
            //         if (order.CustomerInfo == null || order.CustomerInfo.CustomerId == null)
            //         {
            //             throw new Exception("Can't find Customer!");
            //         }
            //         //Insert Order
            //         command.CommandText = "insert into Orders(customer_id, order_status) values (@customerId, @orderStatus);";
            //         command.Parameters.Clear();
            //         command.Parameters.AddWithValue("@customerId", order.CustomerInfo.CustomerId);
            //         command.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATED);
            //         command.ExecuteNonQuery();
            //         //get new Order_ID
            //         // cmd.CommandText = "select order_id from Orders order by order_id desc limit 1;";
            //         command.CommandText = "select LAST_INSERT_ID() as order_id";
            //         reader = command.ExecuteReader();
            //         if (reader.Read())
            //         {
            //             order.OrderId = reader.GetInt32("order_id");
            //         }
            //         reader.Close();

            //         //insert Order Details table
            //         foreach (var laptop in order.Laptops)
            //         {
            //             if (laptop.LaptopId == null || laptop.Quantity <= 0)
            //             {
            //                 throw new Exception("Not Exists Item");
            //             }
            //             //get unit_price
            //             command.CommandText = "select unit_price from Items where item_id=@itemId";
            //             command.Parameters.Clear();
            //             command.Parameters.AddWithValue("@itemId", laptop.LaptopId);
            //             reader = command.ExecuteReader();
            //             if (!reader.Read())
            //             {
            //                 throw new Exception("Not Exists Item");
            //             }
            //             laptop.Price = reader.GetDecimal("unit_price");
            //             reader.Close();

            //             //insert to Order Details
            //             command.CommandText = @"insert into OrderDetails(order_id, item_id, unit_price, quantity) values 
            //                 (" + order.OrderId + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Amount + ");";
            //             command.ExecuteNonQuery();

            //             //update amount in Items
            //             command.CommandText = "update Items set amount=amount-@quantity where item_id=" + item.ItemId + ";";
            //             command.Parameters.Clear();
            //             command.Parameters.AddWithValue("@quantity", laptop.Quantity);
            //             command.ExecuteNonQuery();
            //         }
            //         trans.Commit();
            //         result = true;
            //     }
            //     catch
            //     {
            //         result = false;
            //         try
            //         { trans.Rollback(); }
            //         catch { }
            //     }
            //     finally
            //     {
            //         //unlock all tables;
            //         command.CommandText = "unlock tables;";
            //         command.ExecuteNonQuery();
            //         connection.Close();
            //     }
            // }
            // catch { }
            return result;
        }
        public bool ChangeStatus(int status)
        {
            bool result = false;

            return result;
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            return orders;
        }
        public Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();

            return order;
        }
    }
}