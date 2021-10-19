using System;
using System.Collections.Generic;
using BL;
using Persistance;
using Utilities;

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
            Console.WriteLine("\n  ■ Input List Laptop");
            do
            {
                Console.Write("  → Input ID(input 0 to cancel): ");
                id = ConsoleUtility.GetNumber("ID", 0);
                if (id == 0) break;
                laptop = laptopBL.GetById(id);
                if (laptop == null)
                {
                    ConsoleUtility.Write("  LAPTOP NOT FOUND!!!\n", ConsoleColor.Red);
                }
                else
                {
                    ConsoleUtility.Write(string.Format("  * Laptop Name: {0}\n  * Price: {1:N0} VNĐ\n", laptop.Name, laptop.Price), ConsoleColor.Cyan);
                    if (laptop.Quantity <= 0)
                    {
                        ConsoleUtility.Write("  LAPTOP IS OUT OF STOCK, PLEASE CHOOSE ANOTHER LAPTOP!!!\n", ConsoleColor.Red);
                    }
                    else
                    {
                        bool result;
                        Console.Write("  → Input quantity: ");
                        laptop.Quantity = ConsoleUtility.GetNumber("quantity", 1);
                        result = AddLaptopToOrder(laptop);
                        ConsoleUtility.Write(result ? "  ADD LAPTOP TO ORDER COMPLETED!!!\n" : "  THE STORE DOSEN'T HAVE ENOUGH LAPTOPS IN STOCK!!!\n",
                        result ? ConsoleColor.Green : ConsoleColor.Red);
                    }
                }
            } while (id != 0);
            if (order.Laptops.Count > 0)
            {
                order.Seller.ID = staff.ID;
                order.CustomerInfo = GetCustomer();
                bool result = orderBL.CreateOrder(order);
                ConsoleUtility.Write("  CREATE ORDER " + (result ? "COMPLETED!!!\n" : "NOT COMPLETE!!!\n"), result ? ConsoleColor.Green : ConsoleColor.Red);
                order = new Order();
                ConsoleUtility.PressAnyKey("back");
            }
        }
        private Customer GetCustomer()
        {
            Console.WriteLine("\n  ■ Customer information");
            Customer customer = new Customer();
            Console.CursorVisible = true;
            Console.Write("  → Phone: ");
            customer.Phone = ConsoleUtility.GetPhone();
            var cus = new CustomerBL().GetByPhone(customer.Phone);
            if (cus != null)
            {
                customer = cus;
                Console.WriteLine("  → Customer name: " + customer.Name);
                Console.WriteLine("  → Address: " + customer.Address);
            }
            else
            {
                Console.Write("  → Customer name: ");
                customer.Name = ConsoleUtility.GetName();
                Console.Write("  → Address: ");
                customer.Address = Console.ReadLine();
            }
            return customer;
        }
        public bool AddLaptopToOrder(Laptop laptop)
        {
            int count = laptopBL.GetById(laptop.ID).Quantity;
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
            List<Order> listOrder, orders;
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            int index = 0, page = 1, pageCount, orderCount;
            listOrder = orderBL.GetOrdersUnpaid();
            if (listOrder == null || listOrder.Count == 0)
            {
                Console.Clear();
                ConsoleUtility.PrintTitle("▬▬▬▬ORDER LIST ▬▬▬▬", true);
                ConsoleUtility.Write("\n  ORDER NOT FOUND!!!\n", ConsoleColor.Red);
                ConsoleUtility.PressAnyKey("back");
                return;
            }
            else
            {
                orderCount = listOrder.Count;
                pageCount = (orderCount % 10 == 0) ? orderCount / 10 : orderCount / 10 + 1;
                orders = Order.SplitList(listOrder, index, 10);
                ShowListOrder(orders, "ORDER LIST");
                ConsoleUtility.ShowPageNumber(pageCount, page);
                int id;
                Console.Write("\n  ● Press '");
                ConsoleUtility.Write("ESC", ConsoleColor.Red);
                Console.WriteLine("' to BACK");
                Console.Write("  → Input order id to payment: ");
                Console.CursorVisible = true;
                do
                {
                    id = ConsoleUtility.GetNumber("Order id", 1, out keyInfo);
                    key = keyInfo.Key;
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            order = orderBL.GetById(id);
                            if (order == null)
                            {
                                ConsoleUtility.Write("\n  ORDER NOT FOUND!!!\n", ConsoleColor.Red);
                                ConsoleUtility.PressAnyKey("continue");
                            }
                            else
                            {
                                if (order.Status == Order.CANCEL)
                                {
                                    ConsoleUtility.Write("\n  THE ORDER HAS BEEN CANCELLED!!!\n", ConsoleColor.Red);
                                    ConsoleUtility.PressAnyKey("continue");
                                }
                                else if (order.Status == Order.PAID)
                                {
                                    ConsoleUtility.Write("\n  THE ORDER HAS BEEN PAID!!!\n", ConsoleColor.Red);
                                    ConsoleUtility.PressAnyKey("continue");
                                }
                                else if (order.Status == Order.PROCESSING && staff.ID != order.Accountant.ID)
                                {
                                    ConsoleUtility.Write("\n  ORDER IS BEING PROCESSED!!!\n", ConsoleColor.Red);
                                    ConsoleUtility.PressAnyKey("continue");
                                }
                                else
                                {
                                    order.Accountant = staff;
                                    var result = orderBL.ChangeStatus(Order.PROCESSING, order.ID, (int)staff.ID);
                                    if (result)
                                        Payment(order);
                                    else
                                    {
                                        ConsoleUtility.Write("\n  AN ERROR HAS OCCURRED PLEASE TRY AGAIN LATER!!!\n", ConsoleColor.Red);
                                        ConsoleUtility.PressAnyKey("continue");
                                    }
                                }
                            }
                            listOrder = orderBL.GetOrdersUnpaid();
                            if (listOrder == null || listOrder.Count == 0)
                            {
                                Console.Clear();
                                ConsoleUtility.PrintTitle("▬▬▬▬ ORDER LIST ▬▬▬▬", true);
                                ConsoleUtility.Write("\n  ORDER NOT FOUND!!!\n", ConsoleColor.Red);
                                ConsoleUtility.PressAnyKey("back");
                                return;
                            }
                            page = 1; index = 0; orderCount = listOrder.Count;
                            pageCount = (orderCount % 10 == 0) ? orderCount / 10 : orderCount / 10 + 1;
                            orders = Order.SplitList(listOrder, index, 10);
                            ShowListOrder(orders, "ORDER LIST");
                            ConsoleUtility.ShowPageNumber(pageCount, page);
                            Console.Write("\n  ● Press '");
                            ConsoleUtility.Write("ESC", ConsoleColor.Red);
                            Console.WriteLine("' to BACK");
                            Console.Write("  → Input order id to payment: ");
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            if (page > 1 && key == ConsoleKey.LeftArrow)
                            {
                                page--; index -= 10;
                            }
                            else if (page < pageCount && key == ConsoleKey.RightArrow)
                            {
                                page++; index += 10;
                            }
                            else break;
                            orders = Order.SplitList(listOrder, index, 10);
                            ShowListOrder(orders, "ORDER LIST");
                            ConsoleUtility.ShowPageNumber(pageCount, page);
                            Console.Write("\n  ● Press '");
                            ConsoleUtility.Write("ESC", ConsoleColor.Red);
                            Console.WriteLine("' to BACK");
                            Console.Write("  → Input order id to payment: ");
                            break;
                    }
                } while (key != ConsoleKey.Escape);
            }
        }

        public void Payment(Order order)
        {
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            decimal money, totalPayment = 0;
            bool result;
            foreach (var laptop in order.Laptops) totalPayment += laptop.Price * laptop.Quantity;
            ShowOrder(order, "PAYMENT");
            Console.Write("\n  ● Enter money to PAYMENT (input 0 to skip) or Press combination ");
            ConsoleUtility.Write("CTRL + X", ConsoleColor.Yellow);
            Console.Write(" to CANCEL ORDER or press '");
            ConsoleUtility.Write("ESC", ConsoleColor.Red);
            Console.WriteLine("' to EXIT");
            Console.Write("  → Enter money: ");
            do
            {
                money = ConsoleUtility.GetMoney(out keyInfo);
                key = keyInfo.Key;
                if (key == ConsoleKey.Enter)
                {
                    if (money < totalPayment && money != 0)
                    {
                        ConsoleUtility.Write("  Invalid money!", ConsoleColor.Red);
                        Console.Write("\n  → Enter money: ");
                    }
                    else
                    {
                        if (money == 0) money = totalPayment;
                        order.Status = Order.PAID;
                        result = orderBL.Payment(order);
                        if (result)
                        {
                            ExportInvoice(order);
                            if (money != totalPayment)
                                Console.WriteLine("\n  → EXCESS CASH: {0:N0} VND\n", money - totalPayment);
                        }
                        else
                        {
                            ConsoleUtility.Write("  PAYMENT NOT COMPLETE!\n", ConsoleColor.Red);
                        }
                        ConsoleUtility.PressAnyKey("back");
                        return;
                    }
                }
                if ((key == ConsoleKey.X && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0))
                {
                    order.Status = Order.CANCEL;
                    result = orderBL.Payment(order);
                    Console.WriteLine();
                    ConsoleUtility.Write(result ? "  CANCEL ORDER COMPLETED!\n" : "  CANCEL ORDER NOT COMPLETE!\n",
                                  result ? ConsoleColor.Green : ConsoleColor.Red);
                    ConsoleUtility.PressAnyKey("back");
                    return;
                }
            } while (key != ConsoleKey.Escape);
            orderBL.ChangeStatus(Order.UNPAID, order.ID, (int)order.Accountant.ID);
        }
        public void ShowListOrder(List<Order> orders, string title)
        {
            Console.Clear();
            ConsoleUtility.PrintTitle("▬▬▬▬ " + title + " ▬▬▬▬", false);
            int[] lengthDatas = { 8, 35, 10, 19, 15, 10 };
            ConsoleUtility.PrintLine(lengthDatas, "  ╟", "─", "┬", "╢\n");
            Console.WriteLine("  ║ {0,8} │ {1,-35} │ {2,-10} │ {3,-19} │ {4,15} │ {5,-10} ║",
                            "Order ID", "Customer Name", "Phone", "Order Date", "Total cost(VNĐ)", "Status");
            ConsoleUtility.PrintLine(lengthDatas, "  ╟", "─", "┼", "╢\n");
            foreach (var o in orders)
            {
                decimal total = 0;
                foreach (var laptop in o.Laptops)
                    total += laptop.Quantity * laptop.Price;
                string status = o.Status == Order.UNPAID ? "UNPAID" : o.Status == Order.PAID ? "PAID" : o.Status == Order.CANCEL ? "CANCEL" : "PROCESSING";
                Console.WriteLine("  ║ {0,8} │ {1,-35} │ {2,10} │ {3,-19:dd/MM/yyyy h:mm tt} │ {4,15:N0} │ {5,-10} ║",
                                   o.ID, o.CustomerInfo.Name, o.CustomerInfo.Phone, o.Date, total, status);
            }
            ConsoleUtility.PrintLine(lengthDatas, "  ╚", "═", "╧", "╝\n");
        }
        public void ShowOrder(Order order, string title)
        {
            decimal totalPayment = 0;
            Console.Clear();
            ConsoleUtility.PrintTitle("▬▬▬▬ " + title + " ▬▬▬▬", false);
            Console.WriteLine("  ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("  ║  Order ID       : {0,-94} ║", order.ID);
            Console.WriteLine("  ║  Customer Name  : {0,-94} ║", order.CustomerInfo.Name);
            Console.WriteLine("  ║  Customer Phone : {0,-94} ║", order.CustomerInfo.Phone);
            Console.WriteLine("  ║  Address        : {0,-94} ║", order.CustomerInfo.Address);
            int[] lengthDatas = { 3, 58, 12, 8, 15 };
            ConsoleUtility.PrintLine(lengthDatas, "  ║ ┌", "─", "┬", "┐ ║\n");
            Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12} │ {3,8} │ {4,15} │ ║", "NO", "Laptop Name", "Price(VND)", "Quantity", "Amount(VND)");
            ConsoleUtility.PrintLine(lengthDatas, "  ║ ├", "─", "┼", "┤ ║\n");
            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12:N0} │ {3,8} │ {4,15:N0} │ ║", i + 1,
                order.Laptops[i].Name, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ║ ├─────┴────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────┤ ║");
            Console.WriteLine("  ║ │ TOTAL PAYMENT                                                                              │ {0,15:N0} │ ║", totalPayment);
            Console.WriteLine("  ║ └────────────────────────────────────────────────────────────────────────────────────────────┴─────────────────┘ ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }

        public void ExportInvoice(Order order)
        {
            Console.Clear();
            string line = "══════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            ConsoleUtility.PrintTitle("▬▬▬▬ INVOICE ▬▬▬▬", false);
            int lengthLine = line.Length + 2;
            decimal totalPayment = 0;
            Console.WriteLine("  ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("  ║  Invoice No     : {0,-94} ║", order.ID);
            Console.WriteLine("  ║  Invoice Date   : {0,-94} ║", order.Date);
            Console.WriteLine("  ╟──────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢");
            Console.WriteLine("  ║  Store          : {0,-94} ║", "LAPTOP STORE");
            Console.WriteLine("  ║  Phone          : {0,-94} ║", "0999999999");
            Console.WriteLine("  ║  Address        : {0,-94} ║", "18 Tam Trinh, Minh Khai Ward, Hai Ba Trung District, Ha Noi");
            Console.WriteLine("  ╟──────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢");
            Console.WriteLine("  ║  Customer Name  : {0,-94} ║", order.CustomerInfo.Name);
            Console.WriteLine("  ║  Customer Phone : {0,-94} ║", order.CustomerInfo.Phone);
            Console.WriteLine("  ║  Address        : {0,-94} ║", order.CustomerInfo.Address);
            int[] lengthDatas = { 3, 58, 12, 8, 15 };
            ConsoleUtility.PrintLine(lengthDatas, "  ║ ┌", "─", "┬", "┐ ║\n");
            Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12} │ {3,8} │ {4,15} │ ║", "NO", "Laptop Name", "Price(VND)", "Quantity", "Amount(VND)");
            ConsoleUtility.PrintLine(lengthDatas, "  ║ ├", "─", "┼", "┤ ║\n");
            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ │ {0,3} │ {1,-58} │ {2,12:N0} │ {3,8} │ {4,15:N0} │ ║", i + 1,
                order.Laptops[i].Name, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ║ ├─────┴────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────┤ ║");
            Console.WriteLine("  ║ │ TOTAL PAYMENT                                                                              │ {0,15:N0} │ ║", totalPayment);
            Console.WriteLine("  ║ └────────────────────────────────────────────────────────────────────────────────────────────┴─────────────────┘ ║");
            Console.WriteLine("  ║                                                                                                                  ║");
            Console.WriteLine("  ║                SELLER                             ACCOUNTANT                             CUSTOMER                ║");
            Console.WriteLine("  ║                                                                                                                  ║");
            int x = ConsoleUtility.GetPosition(order.Seller.Name, 38);
            Console.Write("  ║{0," + x + "}{1}{2," + (38 - order.Seller.Name.Length - x) + "}", "", order.Seller.Name, "");
            x = ConsoleUtility.GetPosition(order.Accountant.Name, 38);
            Console.WriteLine("{0," + (x) + "}{1}{2," + (76 - order.Accountant.Name.Length - x) + "}║", "", order.Accountant.Name, "");
            Console.WriteLine("  ║                                                                                                                  ║");
            Console.WriteLine("  ║                                                                                                                  ║");
            Console.WriteLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}