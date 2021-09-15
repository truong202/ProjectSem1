using System;
using System.Collections.Generic;
using BL;
using Persistance;

namespace ConsolePL
{
    public class OrderHandle
    {
        private Order order = new Order();
        private OrderBL orderBL = new OrderBL();
        private LaptopBL laptopBL = new LaptopBL();
        public void CreateOrder(Staff staff)
        {
            Laptop laptop;
            int id;
            // if (order.Laptops.Count == 0)
            // {
            Console.WriteLine("\n ■ Input List Laptop");
            do
            {
                Console.Write(" → Input ID(input 0 to cannel): ");
                id = Utility.GetNumber(0);
                if (id == 0) break;
                laptop = laptopBL.GetById(id);
                if (laptop == null)
                {
                    Console.WriteLine(" Laptop not found!");
                }
                else
                {
                    if (laptop.Quantity <= 0)
                    {
                        Utility.PrintColor(" Laptop is out of stock, please choose another laptop!", ConsoleColor.Red, ConsoleColor.Black);
                        Console.WriteLine();
                    }
                    else
                    {
                        bool result;
                        // do
                        // {
                        Console.Write(" → Input quantity: ");
                        laptop.Quantity = Utility.GetNumber(1);
                        result = AddLaptopToOrder(laptop);
                        Utility.PrintColor(result ? " Add laptop to order completed!" : " The store doesn't have enough laptops in stock!",
                        result ? ConsoleColor.Green : ConsoleColor.Red, ConsoleColor.Black);
                        Console.WriteLine();
                        // } while (!result);
                        // Console.WriteLine(" Press any key to continue...");
                        // Console.ReadKey(true); 
                    }
                }
            } while (id != 0);
            if (order.Laptops.Count > 0)
            {
                order.Seller.StaffId = staff.StaffId;
                order.CustomerInfo = GetCustomer();
                bool result = orderBL.CreateOrder(order);
                Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(" Create order " + (result ? "completed!" : "not complete!"));
                order = new Order();
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Write(" Press any key to back..."); Console.ReadKey(true);
            }
        }

        private Customer GetCustomer()
        {
            Console.WriteLine("\n ■ Customer information");
            Customer customer = new Customer();
            Console.CursorVisible = true;
            Console.Write(" → Phone: ");
            customer.Phone = Utility.GetPhone();
            var cus = new CustomerBL().GetByPhone(customer.Phone);
            if (cus != null)
            {
                customer = cus;
                Console.WriteLine(" → Customer name: " + customer.CustomerName);
                Console.WriteLine(" → Address: " + customer.Address);
            }
            else
            {
                Console.Write(" → Customer name: ");
                customer.CustomerName = Utility.Standardize(Utility.GetName());
                Console.Write(" → Address: ");
                customer.Address = Console.ReadLine();
            }
            return customer;
        }
        public bool AddLaptopToOrder(Laptop laptop)
        {
            int count = laptopBL.GetById(laptop.LaptopId).Quantity;
            int quantity = 0;
            int index = order.Laptops.IndexOf(laptop);
            if (index != -1)
            {
                quantity = laptop.Quantity + order.Laptops[index].Quantity;
                if (quantity > count) return false;
                order.Laptops[index].Quantity += laptop.Quantity;
            }
            else
            {
                if (laptop.Quantity > count) return false;
                order.Laptops.Add(laptop);
            }
            return true;
        }

        public void Payment(Staff staff)
        {
            int offset = 0;
            ConsoleKey key = new ConsoleKey();
            string searchValue = "";
            int orderCount = orderBL.GetOrderCount(searchValue);
            if (orderCount == 0)
            {
                Console.WriteLine(" Order not found!");
                Console.Write(" Press any key to back..."); Console.ReadKey();
                return;
            }
            int pageCount = (orderCount % 10 == 0) ? orderCount / 10 : orderCount / 10 + 1;
            int page = (orderCount > 0) ? 1 : 0;
            var orders = orderBL.GetOrders(searchValue, offset);
            DisplayOrder(orders, page, pageCount);
            do
            {
                Console.CursorVisible = false;
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.F:
                        offset = 0; page = 1;
                        Console.WriteLine();
                        Console.Write(" → Input search value: ");
                        Console.CursorVisible = true;
                        searchValue = Console.ReadLine().Trim();
                        orderCount = orderBL.GetOrderCount(searchValue);
                        pageCount = (orderCount % 10 == 0) ? orderCount / 10 : orderCount / 10 + 1;
                        orders = orderBL.GetOrders(searchValue, offset);
                        DisplayOrder(orders, page, pageCount);
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine();
                        Console.Write(" → Input order ID(input 0 to cannel): ");
                        int id = Utility.GetNumber(1);
                        if (id != 0)
                        {
                            Order order = new OrderBL().GetOrderById(id);
                            ViewOrderDetails(order);
                        }
                        DisplayOrder(orders, page, pageCount);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            page--;
                            offset -= 10;
                            orders = orderBL.GetOrders(searchValue, offset);
                            DisplayOrder(orders, page, pageCount);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (page < pageCount)
                        {
                            page++;
                            offset += 10;
                            orders = orderBL.GetOrders(searchValue, offset);
                            DisplayOrder(orders, page, pageCount);
                        }
                        break;
                }
            } while (key != ConsoleKey.Escape);
            Console.CursorVisible = true;
        }

        public void DisplayOrder(List<Order> orders, int page, int pageCount)
        {
            Console.Clear();
            Console.CursorVisible = false;
            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine(" Order not found!");
            }
            else
            {
                List<string[]> lines = new List<string[]>();
                lines.Add(new[] { "Order ID", "Customer name", "Phone", "Date" });
                foreach (var order in orders)
                {
                    int lengthName = 30;
                    string id = order.OrderId.ToString();
                    string CusName = (order.CustomerInfo.CustomerName.Length > lengthName) ?
                    order.CustomerInfo.CustomerName.Remove(lengthName, order.CustomerInfo.CustomerName.Length - lengthName) + "..." : order.CustomerInfo.CustomerName;
                    string date = order.Date.ToString();
                    lines.Add(new[] { id, order.CustomerInfo.CustomerName,  order.CustomerInfo.Phone, date});
                }
                string[] table = Utility.GetTable(lines);
                foreach (string line in table) Console.WriteLine(" " + line);
                string nextPage = (page > 0 && page < pageCount) ? "►" : " ";
                string prePage = (page > 1) ? "◄" : " ";
                string pages = prePage + $"      [{page}/{pageCount}]      " + nextPage;
                if (page > 0 && pageCount > 1)
                {
                    int position = table[0].Length / 2 + pages.Length / 2 + 1;
                    Console.WriteLine(String.Format("{0," + position + "}", pages));
                }
            }
            Console.Write("\n ● Press '");
            // Utility.PrintColor("F", ConsoleColor.Yellow, ConsoleColor.Black);
            // Console.Write("' to search orders, '");
            Utility.PrintColor("D", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write("' to payment, '");
            Utility.PrintColor("ESC", ConsoleColor.Red, ConsoleColor.Black);
            Console.WriteLine("' to exit");
            
        }
        private void ViewOrderDetails(Order order)
        {
            Console.CursorVisible = false;
            if (order == null)
            {
                Console.WriteLine(" Order not found!");
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            string data;
            string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "Order infomation";
            int lengthLine = line.Length + 2;
            int position = lengthLine / 2 + title.Length / 2 - 1;
            Console.WriteLine(" ┌{0}┐", line);
            Console.WriteLine(" │{0," + position + "}{1," + (lengthLine - position - 1) + "}", title, "│");
            Console.WriteLine(" ├{0}┤", line);
            Console.WriteLine(" │ Order Id:   {0}{1," + (lengthLine - 15 - order.OrderId.ToString().Length) + "}", order.OrderId, "│");
            // Console.WriteLine(" │ Laptop name: {0}{1," + (lengthLine - 15 - order.Laptops.LaptopName.Length) + "}", order.Laptops.LaptopName, "│");
            Console.WriteLine(" │ Customer name: {0}{1," + (lengthLine - 15 - order.CustomerInfo.CustomerName.Length) + "}", order.CustomerInfo.CustomerName, "│");
            // Console.WriteLine(" │ Order date:    {0}{1," + (lengthLine - 15 - order.Date.DateTime.Length) + "}", order.Date, "│");
            // Console.WriteLine(" │ Unit price:         {0}{1," + (lengthLine - 15 - order.Laptops.Price.Length) + "}", order.Laptops.Price, "│");
            // Console.WriteLine(" │ Quantity:    {0}{1," + (lengthLine - 15 - order.Laptops.Count.ToString().Length) + "}", order.Laptops.Count, "│");
            // string price = order.Laptops.Price.ToString("N0") + " VNĐ";
            // Console.WriteLine(" │ Price:       {0}{1," + (lengthLine - 15 - price.Length) + "}", price, "│");
            Console.WriteLine(" └{0}┘", line);
            // Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            // Console.WriteLine((result ? " Add laptop to order completed!" : " The number of laptop in the store is not enough!"));
            // Console.ResetColor();
            // Console.Write(" Press any key to continue...");
            // Console.ReadKey(true);
        }
    }
}