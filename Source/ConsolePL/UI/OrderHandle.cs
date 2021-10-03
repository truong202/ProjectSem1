using System;
using System.Collections.Generic;
using BL;
using Persistance;

namespace ConsolePL
{
    public partial class OrderHandle
    {
        private Order order = new Order();
        private OrderBL orderBL = new OrderBL();
        private LaptopBL laptopBL = new LaptopBL();
        public void CreateOrder(Staff staff)
        {
            Laptop laptop;
            int id;
            Console.WriteLine("\n  ■ Input List Laptop");
            do
            {
                Console.Write("  → Input ID(input 0 to cancel): ");
                id = Utility.GetNumber("ID", 0);
                if (id == 0) break;
                laptop = laptopBL.GetById(id);
                if (laptop == null)
                {
                    Console.WriteLine("  Laptop not found!");
                }
                else
                {
                    if (laptop.Quantity <= 0)
                    {
                        Utility.PrintColor("  Laptop is out of stock, please choose another laptop!", ConsoleColor.Red, ConsoleColor.Black);
                        Console.WriteLine();
                    }
                    else
                    {
                        bool result;
                        Console.Write("  → Input quantity: ");
                        laptop.Quantity = Utility.GetNumber("quantity", 1);
                        result = AddLaptopToOrder(laptop);
                        Utility.PrintColor(result ? "  Add laptop to order completed!" : "  The store doesn't have enough laptops in stock!",
                        result ? ConsoleColor.Green : ConsoleColor.Red, ConsoleColor.Black);
                        Console.WriteLine();
                    }
                }
            } while (id != 0);
            if (order.Laptops.Count > 0)
            {
                order.Seller.Id = staff.Id;
                order.CustomerInfo = GetCustomer();
                bool result = orderBL.CreateOrder(order);
                Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine("  Create order " + (result ? "completed!" : "not complete!"));
                order = new Order();
                Console.ResetColor();
                Console.CursorVisible = false;
                Console.Write("  Press any key to back..."); Console.ReadKey(true);
            }
        }
        private Customer GetCustomer()
        {
            Console.WriteLine("\n  ■ Customer information");
            Customer customer = new Customer();
            Console.CursorVisible = true;
            Console.Write("  → Phone: ");
            customer.Phone = Utility.GetPhone();
            var cus = new CustomerBL().GetByPhone(customer.Phone);
            if (cus != null)
            {
                customer = cus;
                Console.WriteLine("  → Customer name: " + customer.CustomerName);
                Console.WriteLine("  → Address: " + customer.Address);
            }
            else
            {
                Console.Write("  → Customer name: ");
                customer.CustomerName = Utility.Standardize(Utility.GetName());
                Console.Write("  → Address: ");
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
            List<Order> orders = new List<Order>();
            ConsoleKey key = ConsoleKey.Escape;
            ConsoleKeyInfo keyInfo;
            // int offset = 0, page = 1;
            // int OrderCount = orderBL.GetCount(Order.UNPAID);
            // int pageCount = (OrderCount % 8 == 0) ? OrderCount / 8 : OrderCount / 8 + 1;
            // orders = orderBL.GetByStatus(Order.UNPAID, offset);
            Console.Clear();
            orders = orderBL.GetOrdersUnpaid();
            if (orders == null || orders.Count == 0)
            {
                Utility.PrintTitle("▬▬▬▬ PAYMENT ▬▬▬▬", true);
                Console.WriteLine("  Order not found!");
                Utility.PressAnyKey("back");
                return;
            }
            else
            {
                // bool isContinue = true;
                // do
                // {
                string input = string.Empty;
                Utility.PrintTitle("▬▬▬▬ PAYMENT ▬▬▬▬", true);
                ShowListOrder(orders);
                int id = 0;
                Console.CursorVisible = true;
                Console.Write("\n  ● Press '");
                Utility.PrintColor("ESC", ConsoleColor.Red, ConsoleColor.Black);
                Console.WriteLine("' to BACK");
                Console.Write("  → Input order id to payment: ");
                do
                {
                    id = Utility.GetNumber("Order id", 1, out keyInfo);
                    key = keyInfo.Key;
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            order = orderBL.GetById(id);
                            if (order == null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("  Order not found!");
                                Utility.PressAnyKey("continue");
                                input = string.Empty;
                            }
                            else
                            {
                                if (order.Status == Order.CANCEL)
                                {
                                    Console.WriteLine();
                                    Utility.Write("  The order has been cancelled!", ConsoleColor.Red);
                                    Console.WriteLine();
                                    Utility.PressAnyKey("continue");
                                }
                                else if (order.Status == Order.PAID)
                                {
                                    Console.WriteLine();
                                    Utility.Write("  The order has been paid!", ConsoleColor.Red);
                                    Console.WriteLine();
                                    Utility.PressAnyKey("continue");
                                }
                                else if (order.Status == Order.PROCESSING && staff.Id != order.Accountance.Id)
                                {
                                    Console.WriteLine();
                                    Utility.Write("  Order is being processed!", ConsoleColor.Red);
                                    Console.WriteLine();
                                    Utility.PressAnyKey("continue");
                                }
                                else
                                {
                                    order.Accountance = staff;
                                    orderBL.ChangeStatus(Order.PROCESSING, order.OrderId, (int)staff.Id);
                                    ShowOrder(order, "▬▬▬▬ PAYMENT ▬▬▬▬");
                                    Payment(order);
                                    // offset = 0; page = 1;
                                    // OrderCount = orderBL.GetCount(Order.UNPAID);
                                    // pageCount = (OrderCount % 8 == 0) ? OrderCount / 8 : OrderCount / 8 + 1;
                                    // orders = orderBL.GetByStatus(Order.UNPAID, offset);

                                }
                            }
                            orders = orderBL.GetOrdersUnpaid();
                            Console.Clear();
                            Utility.PrintTitle("▬▬▬▬ PAYMENT ▬▬▬▬", true);
                            if (orders == null || orders.Count == 0)
                            {
                                Console.WriteLine("  Order not found!");
                                Utility.PressAnyKey("back");
                                return;
                            }
                            ShowListOrder(orders);
                            Console.Write("\n  ● Press '");
                            Utility.PrintColor("ESC", ConsoleColor.Red, ConsoleColor.Black);
                            Console.WriteLine("' to BACK");
                            Console.Write("  → Input order id to payment: ");
                            break;
                        // case ConsoleKey.LeftArrow:
                        //     if (page > 1)
                        //     {
                        //         page--;
                        //         offset -= 8;
                        //         orders = orderBL.GetByStatus(Order.UNPAID, offset);
                        //     }
                        //     break;
                        // case ConsoleKey.RightArrow:
                        //     if (page < pageCount)
                        //     {
                        //         page++;
                        //         offset += 8;
                        //         orders = orderBL.GetByStatus(Order.UNPAID, offset);
                        //     }
                        //     break;
                        case ConsoleKey.Escape:

                            // isContinue = false;
                            break;
                    }
                } while (key != ConsoleKey.Escape);
                // } while (isContinue);
            }
        }

        public void Payment(Order order)
        {
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            decimal money;
            decimal totalPayment = 0;
            bool result;
            foreach (var laptop in order.Laptops) totalPayment += laptop.Price * laptop.Quantity;
            Console.Write("\n  ● Enter money to PAYMENT (input 0 to skip) or Press combination ");
            Utility.PrintColor("CTRL + X", ConsoleColor.Yellow, ConsoleColor.Black);
            Console.Write(" to CANCEL ORDER or press '");
            Utility.PrintColor("ESC", ConsoleColor.Red, ConsoleColor.Black);
            Console.WriteLine("' to EXIT");
            Console.Write("  → Enter money: ");
            do
            {
                money = Utility.GetMoney(out keyInfo);
                key = keyInfo.Key;
                if (key == ConsoleKey.Enter)
                {
                    if (money < totalPayment && money != 0)
                    {
                        Utility.PrintColor("  Invalid money!", ConsoleColor.Red, ConsoleColor.Black);
                        Console.Write("\n  → Enter money: ");
                    }
                    else
                    {
                        if (money == 0) money = totalPayment;
                        order.Status = Order.PAID;
                        result = orderBL.Payment(order);
                        if (result)
                        {
                            ExportInvoice(order, money);
                        }
                        else
                        {
                            Utility.Write("  PAYMENT NOT COMPLETE!", ConsoleColor.Red);
                            Console.WriteLine();
                        }
                        Utility.PressAnyKey("back");
                        return;
                    }
                }
                if ((key == ConsoleKey.X && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0))
                {
                    order.Status = Order.CANCEL;
                    result = orderBL.Payment(order);
                    Console.WriteLine();
                    Utility.Write(result ? "  CANCEL ORDER COMPLETED!" : "  CANCEL ORDER NOT COMPLETE!",
                                       result ? ConsoleColor.DarkGreen : ConsoleColor.Red);
                    Console.WriteLine();
                    Utility.PressAnyKey("back");
                    return;
                }
            } while (key != ConsoleKey.Escape);
            orderBL.ChangeStatus(Order.UNPAID, order.OrderId, (int)order.Accountance.Id);
        }
        public void ShowListOrder(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                Console.WriteLine("  Order not found!"); return;
            }

            int[] lengthDatas = { 8, 35, 10, 19, 15, 10 };
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ┌", "─", "┬", "┐"));
            Console.WriteLine("  │ {0,8} │ {1,-35} │ {2,-10} │ {3,-19} │ {4,15} │ {5,-10} │",
                            "Order ID", "Customer Name", "Phone", "Order Date", "Total cost(VNĐ)", "Status");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ├", "─", "┼", "┤"));
            foreach (var o in orders)
            {
                decimal total = 0;
                foreach (var laptop in o.Laptops)
                    total += laptop.Quantity * laptop.Price;
                string status = o.Status == Order.UNPAID ? "UNPAID" : o.Status == Order.PAID ? "PAID" : o.Status == Order.CANCEL ? "CANCEL" : "PROCESSING";
                Console.WriteLine("  │ {0,8} │ {1,-35} │ {2,10} │ {3,-19:dd/MM/yyyy h:mm tt} │ {4,15:N0} │ {5,-10} │",
                                o.OrderId, o.CustomerInfo.CustomerName, o.CustomerInfo.Phone, o.Date, total, status);
            }
            Console.WriteLine(Utility.GetLine(lengthDatas, "  └", "─", "┴", "┘"));
            // if (pageCount > 1)
            // {
            //     string nextPage = (page > 0 && page < pageCount) ? "►" : " ";
            //     string prePage = (page > 1) ? "◄" : " ";
            //     string pages = prePage + $"      [{page}/{pageCount}]      " + nextPage;
            //     int position = 120 / 2 + pages.Length / 2 + 1;
            //     Console.WriteLine(String.Format("{0," + position + "}", pages));
            // }
        }
        public void ShowOrder(Order order, string title)
        {
            Console.Clear();
            if (order == null)
            {
                Utility.PrintTitle(title, true);
                Console.WriteLine("  Order not found!");
                Utility.PressAnyKey("back");
                return;
            }
            decimal totalPayment = 0;
            Utility.PrintTitle(title, false);
            Console.WriteLine("  ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("  ║  Order ID       : {0,-94} ║", order.OrderId);
            Console.WriteLine("  ║  Customer Name  : {0,-94} ║", order.CustomerInfo.CustomerName);
            Console.WriteLine("  ║  Customer Phone : {0,-94} ║", order.CustomerInfo.Phone);
            Console.WriteLine("  ║  Address        : {0,-94} ║", order.CustomerInfo.Address);
            int[] lengthDatas = { 3, 58, 12, 8, 15 };
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ║ ┌", "─", "┬", "┐ ║"));
            Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12} │ {3,8} │ {4,15} │ ║", "NO", "Laptop Name", "Price(VNĐ)", "Quantity", "Amount(VNĐ)");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ║ ├", "─", "┼", "┤ ║"));
            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12:N0} │ {3,8} │ {4,15:N0} │ ║", i + 1,
                order.Laptops[i].LaptopName, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ║ ├─────┴────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────┤ ║");
            Console.WriteLine("  ║ │ TOTAL PAYMENT                                                                              │ {0,15:N0} │ ║", totalPayment);
            Console.WriteLine("  ║ └────────────────────────────────────────────────────────────────────────────────────────────┴─────────────────┘ ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        public void ExportInvoice(Order order, decimal money)
        {
            Console.Clear();
            string line = "══════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            Utility.PrintTitle("▬▬▬▬ Invoice ▬▬▬▬", false);
            int lengthLine = line.Length + 2;
            decimal totalPayment = 0;
            Console.WriteLine("  ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("  ║  Invoice No     : {0,-94} ║", order.OrderId);
            Console.WriteLine("  ║  Invoice Date   : {0,-94} ║", order.Date);
            Console.WriteLine("  ╟──────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢");
            Console.WriteLine("  ║  Store          : {0,-94} ║", "LAPTOP STORE");
            Console.WriteLine("  ║  Phone          : {0,-94} ║", "0999999999");
            Console.WriteLine("  ║  Address        : {0,-94} ║", "18 Tam Trinh, Minh Khai Ward, Hai Ba Trung District, Ha Noi");
            Console.WriteLine("  ║  Seller         : {0,-94} ║", order.Seller.Name);
            Console.WriteLine("  ║  Accountance    : {0,-94} ║", order.Accountance.Name);
            Console.WriteLine("  ╟──────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢");
            Console.WriteLine("  ║  Customer Name  : {0,-94} ║", order.CustomerInfo.CustomerName);
            Console.WriteLine("  ║  Customer Phone : {0,-94} ║", order.CustomerInfo.Phone);
            Console.WriteLine("  ║  Address        : {0,-94} ║", order.CustomerInfo.Address);
            int[] lengthDatas = { 3, 58, 12, 8, 15 };
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ║ ┌", "─", "┬", "┐ ║"));
            Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12} │ {3,8} │ {4,15} │ ║", "NO", "Laptop Name", "Price(VNĐ)", "Quantity", "Amount(VNĐ)");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ║ ├", "─", "┼", "┤ ║"));
            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12:N0} │ {3,8} │ {4,15:N0} │ ║", i + 1,
                order.Laptops[i].LaptopName, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ║ ├─────┴────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────┤ ║");
            Console.WriteLine("  ║ │ TOTAL PAYMENT                                                                              │ {0,15:N0} │ ║", totalPayment);
            Console.WriteLine("  ║ │ CUSTOMER PAY                                                                               │ {0,15:N0} │ ║", money);
            Console.WriteLine("  ║ │ EXCESS CASH                                                                                │ {0,15:N0} │ ║", money - totalPayment);
            Console.WriteLine("  ║ └────────────────────────────────────────────────────────────────────────────────────────────┴─────────────────┘ ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}