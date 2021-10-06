using System;
using System.Collections.Generic;
using BL;
using Persistance;

namespace ConsolePL
{
    public class OrderHandle
    {
        public Order NewOrder { set; get; }
        private OrderBL orderBL = new OrderBL();
        private LaptopBL laptopBL = new LaptopBL();
        private CustomerBL customerBL = new CustomerBL();

        public OrderHandle()
        {
            NewOrder = new Order();
        }
        public void CreateOrder(Staff staff)
        {
            NewOrder.Seller = staff;
            string phone = NewOrder.CustomerInfo.Phone == null ? "" : NewOrder.CustomerInfo.Phone;
            string name = NewOrder.CustomerInfo.CustomerName == null ? "" : NewOrder.CustomerInfo.CustomerName;
            string address = NewOrder.CustomerInfo.Address == null ? "" : NewOrder.CustomerInfo.Address;
            string addressSplit = address.Length < 43 ? address : "..." + address.Substring(address.Length - 40, 40);
            int count = NewOrder.Laptops.Count;
            int pageCount = (count % 5 == 0) ? count / 5 : count / 5 + 1;
            int page = 1, pStatus = 1;
            int feature = 3;
            int index = 0;
            int laptopChoose = 0;
            ConsoleKey[] validKeys = { ConsoleKey.Escape, ConsoleKey.LeftArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Tab };
            ShowFormCreate(NewOrder);
            List<Laptop> laptops = new List<Laptop>();
            laptops = Laptop.Split(NewOrder.Laptops, index, 5);
            ShowLaptopInOrder(laptops);
            Console.SetCursorPosition(1, 21);
            Utility.ShowPageNumber(page, pageCount, 0, 67);
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            bool valid = false;
            while (true)
            {
                if (feature >= 3 && feature <= 5)
                {
                    Console.CursorVisible = true;
                    if (feature == 3)
                    {
                        Console.SetCursorPosition(71 + phone.Length, 10);
                        keyInfo = Utility.GetString(ref phone, 43, validKeys);
                        NewOrder.CustomerInfo.Phone = phone;
                        Console.CursorVisible = false;
                        try
                        {
                            Utility.Write(String.Format("{0,48}", ""), 69, 12);
                            Customer.CheckPhone(phone);
                            var customer = customerBL.GetByPhone(phone);
                            if (customer != null)
                            {
                                NewOrder.CustomerInfo = customer;
                                name = customer.CustomerName;
                                address = customer.Address;
                            }
                            else
                            {
                                NewOrder.CustomerInfo.CustomerId = null;
                            }
                            addressSplit = address.Length <= 43 ? address : "..." + address.Substring(address.Length - 40, 40);
                            Utility.Write(String.Format("{0,44}", ""), 71, 15);
                            Utility.Write(name, 71, 15);
                            Utility.Write(String.Format("{0,44}", ""), 71, 20);
                            Utility.Write(addressSplit, 71, 20);
                            valid = true;
                        }
                        catch (Exception e)
                        {
                            Utility.Write(e.Message, ConsoleColor.Red, 70, 12);
                            valid = false;
                        }
                    }
                    else if (feature == 4)
                    {
                        Console.SetCursorPosition(71 + name.Length, 15);
                        keyInfo = Utility.GetString(ref name, 43, validKeys);
                        Console.CursorVisible = false;
                        NewOrder.CustomerInfo.CustomerName = name;
                        try
                        {
                            Utility.Write(String.Format("{0,48}", ""), 69, 17);
                            Customer.CheckName(name);
                            valid = true;
                        }
                        catch (Exception e)
                        {
                            Utility.Write(e.Message, ConsoleColor.Red, 70, 17);
                            valid = false;
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(71 + addressSplit.Length, 20);
                        keyInfo = Utility.GetString(ref address, 43, validKeys);
                        NewOrder.CustomerInfo.Address = address;
                        addressSplit = address.Length <= 43 ? address : "..." + address.Substring(address.Length - 40, 40);
                    }
                    Console.CursorVisible = false;
                }
                else
                {
                    if (feature == 6)
                    {
                        Utility.Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄", ConsoleColor.Red, 55, 25);
                        Utility.Write(" CREATE ORDER ", ConsoleColor.White, ConsoleColor.Red, 55, 26);
                        Utility.Write("▀▀▀▀▀▀▀▀▀▀▀▀▀▀", ConsoleColor.Red, 55, 27);
                    }
                    keyInfo = Console.ReadKey(true);
                }
                key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        switch (feature)
                        {
                            case 1:
                                NewOrder.Laptops[index + laptopChoose].Quantity--;
                                if (NewOrder.Laptops[index + laptopChoose].Quantity == 0)
                                {
                                    NewOrder.Laptops.Remove(NewOrder.Laptops[index + laptopChoose]);
                                    laptops.Remove(laptops[laptopChoose]);
                                    count--;
                                    int newPageCount = (count % 5 == 0) ? count / 5 : count / 5 + 1;
                                    if (newPageCount != pageCount)
                                    {
                                        if (page == pageCount)
                                        {
                                            index -= 5;
                                            page--;
                                        }
                                        pageCount = newPageCount;
                                        laptopChoose = 4;
                                    }
                                    else if (laptopChoose > laptops.Count - 1) laptopChoose = laptops.Count - 1;
                                }
                                Utility.Clear(2, 8, 64, 15);
                                if (NewOrder.Laptops.Count > 0)
                                {
                                    laptops = Laptop.Split(NewOrder.Laptops, index, 5);
                                    ShowLaptopInOrder(laptops);
                                    Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Red, 58, laptopChoose * 2 + 11);
                                    Console.SetCursorPosition(1, 21);
                                    Utility.ShowPageNumber(page, pageCount, 0, 67);
                                }
                                else feature = 3;
                                break;
                            case 2:
                                if (pStatus == 1)
                                {
                                    index += 5;
                                    page++;
                                }
                                else
                                {
                                    index -= 5;
                                    page--;
                                }
                                if (page == pageCount && pageCount > 1)
                                    laptopChoose = NewOrder.Laptops.Count - (pageCount - 1) * 5 - 1;
                                else
                                    laptopChoose = 4;
                                pStatus = page == 1 ? 1 : page == pageCount ? -1 : pStatus == 1 ? 1 : -1;
                                laptops = Laptop.Split(NewOrder.Laptops, index, 5);
                                Utility.Clear(2, 8, 64, 15);
                                ShowLaptopInOrder(laptops);
                                Console.SetCursorPosition(1, 21);
                                Utility.ShowPageNumber(page, pageCount, pStatus, 67);
                                break;
                            case 3:
                                if (NewOrder.CustomerInfo.CustomerId == null)
                                    feature = 4;
                                else
                                {
                                    feature = 6;
                                }
                                break;
                            case 4:
                                feature = 5;
                                break;
                            case 5:
                                feature = 6;
                                break;
                            case 6:
                                if (NewOrder.Laptops.Count > 0 && valid)
                                {
                                    bool result = orderBL.CreateOrder(NewOrder);
                                    Utility.Clear(1, 5, 117, 23);
                                    string[] message = result ? new[] { "CREATE ORDER COMPLETED!" } : new[] { "CREATE ORDER NOT COMPLETE!" };
                                    Utility.ShowMessage(message, result ? ConsoleColor.Green : ConsoleColor.Red);
                                    NewOrder = new Order();
                                    return;
                                }
                                else
                                {
                                    string msg = "PLEASE SELECT AT LEAST 1 LAPTOP AND FILL IN CUSTOMER INFORMATION";
                                    Utility.Write(msg, ConsoleColor.Red, Utility.GetPosition(msg, 120), 24);
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        switch (feature)
                        {
                            case 1:
                                if (laptopChoose != 0)
                                {
                                    Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Black, 58, laptopChoose * 2 + 11);
                                    laptopChoose--;
                                    Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Red, 58, laptopChoose * 2 + 11);
                                }
                                break;
                            case 2:
                                Utility.ShowPageNumber(page, pageCount, 0, 67);
                                feature = 1;
                                Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Red, 58, laptopChoose * 2 + 11);
                                break;
                            case 4:
                                feature = 3;
                                break;
                            case 5:
                                feature = 4;
                                break;
                            case 6:
                                Utility.Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄", ConsoleColor.DarkCyan, 55, 25);
                                Utility.Write(" CREATE ORDER ", ConsoleColor.White, ConsoleColor.DarkCyan, 55, 26);
                                Utility.Write("▀▀▀▀▀▀▀▀▀▀▀▀▀▀", ConsoleColor.DarkCyan, 55, 27);
                                Utility.Write(String.Format("{0,117}", ""), 1, 24);
                                if (NewOrder.CustomerInfo.CustomerId == null)
                                    feature = 5;
                                else feature = 3;
                                break;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        switch (feature)
                        {
                            case 1:
                                Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Black, 58, laptopChoose * 2 + 11);
                                if (laptopChoose < 4 && (laptopChoose + index < NewOrder.Laptops.Count - 1))
                                {
                                    laptopChoose++;
                                    Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Red, 58, laptopChoose * 2 + 11);
                                }
                                else if (pageCount > 1)
                                {
                                    Console.SetCursorPosition(1, 21);
                                    Utility.ShowPageNumber(page, pageCount, pStatus, 67);
                                    feature = 2;
                                }
                                else
                                {
                                    feature = 6;
                                }
                                break;
                            case 2:
                                Utility.ShowPageNumber(page, pageCount, 0, 67);
                                feature = 6;
                                break;
                            case 3:
                                if (NewOrder.CustomerInfo.CustomerId == null)
                                    feature = 4;
                                else
                                    feature = 6;
                                break;
                            case 4:
                                feature = 5;
                                break;
                            case 5:
                                feature = 6;
                                break;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        switch (feature)
                        {
                            case 2:
                                pStatus = page == 1 ? 1 : page == pageCount ? -1 : pStatus == 1 ? -1 : 1;
                                Utility.ShowPageNumber(page, pageCount, pStatus, 67);
                                break;
                            case 1:
                                Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Black, 58, laptopChoose * 2 + 11);
                                feature = 3;
                                break;
                            case 3:
                            case 4:
                            case 5:
                                if (NewOrder.Laptops.Count > 0)
                                {
                                    laptopChoose = feature == 5 ? laptopChoose = laptops.Count - 1 : 0;
                                    Utility.Write("Delete", ConsoleColor.Cyan, ConsoleColor.Red, 58, laptopChoose * 2 + 11);
                                    feature = 1;
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.Tab:
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
        public void Payment(Staff staff)
        {
            Console.Clear();
            List<Order> listOrder = new List<Order>();
            List<Order> orders = new List<Order>();
            Order order = new Order();
            int index = 0, page = 1, orderChoose = 0;
            ConsoleKey key;
            listOrder = orderBL.GetOrdersUnpaid();
            int orderCount = listOrder != null ? listOrder.Count : 0;
            int pageCount = (orderCount % 8 == 0) ? orderCount / 8 : orderCount / 8 + 1;
            Utility.PrintBorder(119, 29);
            Utility.PrintNameStore(72, 1);
            Utility.Write("PAYMENT", ConsoleColor.Yellow, 3, 5);
            Utility.Write("ESC", ConsoleColor.Red, 108, 28); Console.Write(": Exit");
            if (listOrder == null || listOrder.Count == 0)
            {
                Utility.ShowMessage(new[] { "ORDER NOT FOUND!!!" }, ConsoleColor.Red);
                return;
            }
            else
            {
                orders = Order.Split(listOrder, index, 8);
                ShowListOrder(orders);
                Console.SetCursorPosition(1, orders.Count * 2 + 10);
                Utility.ShowPageNumber(page, pageCount, 0, 120);
                Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Red, 107, orderChoose * 2 + 10);
            }
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (listOrder == null) break;
                        order = orderBL.GetById(orders[orderChoose].OrderId);
                        Utility.Clear(1, 7, 117, 21);
                        if (order == null)
                        {
                            Utility.ShowMessage(new[] { "ORDER NOT FOUND!!!" }, ConsoleColor.Red);
                        }
                        else if (order.Status == Order.PAID)
                        {
                            Utility.ShowMessage(new[] { "THE ORDER HAS BEEN PAID!" }, ConsoleColor.Red);
                        }
                        else if (order.Status == Order.CANCEL)
                        {
                            Utility.ShowMessage(new[] { "THE ORDER HAS BEEN CANCELLED!" }, ConsoleColor.Red);
                        }
                        else if (order.Status == Order.PROCESSING && staff.StaffId != order.Accountance.StaffId)
                        {
                            Utility.ShowMessage(new[] { "ORDER IS BEING PROCESSED!" }, ConsoleColor.Yellow);
                        }
                        else
                        {
                            order.Accountance = staff;
                            orderBL.ChangeStatus(Order.PROCESSING, order.OrderId, (int)order.Accountance.StaffId);
                            Payment(order);
                            Console.Clear();
                            Utility.PrintBorder(119, 29);
                            Utility.PrintNameStore(72, 1);
                            Utility.Write("PAYMENT", ConsoleColor.Yellow, 3, 5);
                            Utility.Write("ESC", ConsoleColor.Red, 108, 28); Console.Write(": Exit");
                        }
                        listOrder = orderBL.GetOrdersUnpaid();
                        if (listOrder == null || listOrder.Count == 0)
                        {
                            Utility.ShowMessage(new[] { "ORDER NOT FOUND!!!" }, ConsoleColor.Red);
                            return;
                        }
                        orderCount = listOrder != null ? listOrder.Count : 0;
                        pageCount = (orderCount % 8 == 0) ? orderCount / 8 : orderCount / 8 + 1;
                        page = 1;
                        index = 0;
                        orderChoose = 0;
                        orders = Order.Split(listOrder, index, 8);
                        Utility.Clear(1, 7, 117, 21);
                        ShowListOrder(orders);
                        Console.SetCursorPosition(1, orders.Count * 2 + 10);
                        Utility.ShowPageNumber(page, pageCount, 0, 120);
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Red, 107, orderChoose * 2 + 10);
                        break;
                    case ConsoleKey.UpArrow:
                        if (listOrder == null) break;
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Black, 107, orderChoose * 2 + 10);
                        orderChoose--;
                        if (orderChoose < 0) orderChoose = orders.Count - 1;
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Red, 107, orderChoose * 2 + 10);
                        break;
                    case ConsoleKey.DownArrow:
                        if (listOrder == null) break;
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Black, 107, orderChoose * 2 + 10);
                        orderChoose++;
                        if (orderChoose > orders.Count - 1) orderChoose = 0;
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Red, 107, orderChoose * 2 + 10);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        if (listOrder == null) break;
                        if (page != 1 && key == ConsoleKey.LeftArrow)
                        {
                            page--; index -= 8;
                        }
                        if (page < pageCount && key == ConsoleKey.RightArrow)
                        {
                            page++; index += 8;
                        }
                        orderChoose = 0;
                        orders = Order.Split(listOrder, index, 8);
                        Utility.Clear(1, 7, 117, 21);
                        ShowListOrder(orders);
                        Console.SetCursorPosition(1, orders.Count * 2 + 10);
                        Utility.ShowPageNumber(page, pageCount, 0, 120);
                        Utility.Write("Payment", ConsoleColor.Cyan, ConsoleColor.Red, 107, orderChoose * 2 + 10);
                        break;
                    case ConsoleKey.Escape:
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }

        public void Payment(Order order)
        {
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            decimal money;
            decimal totalPayment = 0;
            bool result;
            ShowOrder(order, "Payment");
            foreach (var laptop in order.Laptops) totalPayment += laptop.Price * laptop.Quantity;
            Console.Write("  → Enter money: ");
            int x = Console.CursorLeft;
            Console.WriteLine("\n\n");
            int y = Console.CursorTop;
            if (y < 29)
            {
                Utility.PrintBorder(119, 29);
            }
            else
            {
                Utility.PrintBorder(119, y);
            }
            Utility.Write("CTRL + X: ", ConsoleColor.Yellow, 2, Console.CursorTop - 1);
            Utility.Write("CANCEL ORDER", ConsoleColor.Red);
            Utility.Write("ESC", ConsoleColor.Yellow, 108, Console.CursorTop);
            Console.Write(": Exit");
            Console.SetCursorPosition(x, y - 3);
            do
            {
                money = Utility.GetMoney(out keyInfo);
                key = keyInfo.Key;
                if (key == ConsoleKey.Enter)
                {
                    if (money < totalPayment)
                    {
                        Console.SetCursorPosition(1, y - 2);
                        Utility.Write("   Invalid money!", ConsoleColor.Red, ConsoleColor.Black);
                        Console.SetCursorPosition(1, y - 3);
                        Console.Write("                                                       ");
                        Console.SetCursorPosition(1, y - 3);
                        Console.Write(" → Enter money: ");
                    }
                    else
                    {
                        order.Status = Order.PAID;
                        result = orderBL.Payment(order);
                        if (result)
                        {
                            ExportInvoice(order, money);
                            Console.Write("\n Press Enter to continue...");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            Utility.Clear(1, 6, 117, 22);
                            Utility.ShowMessage(new[] { "PAYMENT NOT COMPLETE!" }, ConsoleColor.Red);
                        }
                        return;
                    }
                }
                if ((key == ConsoleKey.X && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0))
                {
                    Console.CursorVisible = false;
                    order.Status = Order.CANCEL;
                    result = orderBL.Payment(order);
                    string[] msg = result ? new[] { "CANCEL ORDER COMPLETED!" } : new[] { "CANCEL ORDER NOT COMPLETE!" };
                    Utility.Clear(1, 6, 117, 22);
                    Utility.ShowMessage(msg, result ? ConsoleColor.Green : ConsoleColor.Red);
                    return;
                }
            } while (key != ConsoleKey.Escape);
            orderBL.ChangeStatus(Order.UNPAID, order.OrderId, (int)order.Accountance.StaffId);
            Console.CursorVisible = false;
        }
        public void ShowOrder(Order order, string title)
        {
            Console.Clear();
            if (order == null)
            {
                Utility.PrintTitle(title, true);
                Console.WriteLine("  Order not found!");
                Console.ReadKey();
                return;
            }
            Utility.PrintNameStore(72, 1);
            Utility.Write("PAYMENT", ConsoleColor.Yellow, 3, 5);
            Console.WriteLine("");
            string line = "═════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            int lengthLine = line.Length + 2;
            decimal totalPayment = 0;
            Console.WriteLine("  ╔{0}╗", line);
            Console.WriteLine("  ║{0," + (lengthLine - 1) + "}", "║");
            Console.WriteLine("  ║ Order ID       : {0," + -(lengthLine - 20) + "}║", order.OrderId);
            Console.WriteLine("  ║ Customer Name  : {0," + -(lengthLine - 20) + "}║", order.CustomerInfo.CustomerName);
            Console.WriteLine("  ║ Customer Phone : {0," + -(lengthLine - 20) + "}║", order.CustomerInfo.Phone);
            Console.WriteLine("  ║ Address        : {0," + -(lengthLine - 20) + "}║", order.CustomerInfo.Address);
            int[] lengthDatas = { 3, 61, 12, 8, 15 };
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╟", "─", "┬", "╢"));
            Console.WriteLine("  ║ {0,3} │ {1,-61} │ {2,12} │ {3,8} │ {4,15} ║", "NO", "Laptop Name", "Price(VNĐ)", "Quantity", "Amount(VNĐ");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╟", "─", "┼", "╢"));

            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ {0,3} │ {1,-61} │ {2,12:N0} │ {3,8} │ {4,15:N0} ║", i + 1,
                order.Laptops[i].LaptopName, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ╟─────┴───────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────╢");
            Console.WriteLine("  ║ TOTAL PAYMENT                                                                                 │ {0,15:N0} ║", totalPayment);
            Console.WriteLine("  ╚═══════════════════════════════════════════════════════════════════════════════════════════════╧═════════════════╝");
        }
        public void ExportInvoice(Order order, decimal money)
        {
            Console.Clear();
            string address = "18 Tam Trinh, Minh Khai Ward, District Hai Ba Trung, Ha Noi";
            List<string> lineSplit;
            string line = "══════════════════════════════════════════════════════════════════════════════════════════════════════════════════";
            string line1 = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            Utility.PrintTitle("▬▬▬▬ INVOICE ▬▬▬▬", false);
            int lengthLine = line.Length + 2;
            decimal totalPayment = 0;
            Console.WriteLine("  ╟{0}╢", line1);
            Console.WriteLine("  ║ Invoice No     : {0," + -(lengthLine - 20) + "}║", order.OrderId);
            Console.WriteLine("  ║ Invoice Date   : {0," + -(lengthLine - 20) + "}║", order.Date);
            Console.WriteLine("  ╟────────────────────────────────────────────────────────┬─────────────────────────────────────────────────────────╢");
            Console.WriteLine("  ║ Store  : {0,-46}│ Customer Name : {1,-39} ║", "LAPTOP STORE", order.CustomerInfo.CustomerName);
            Console.WriteLine("  ║ Phone  : {0,-46}│ Customer Phone: {1,-39} ║", "0999999999", order.CustomerInfo.Phone);
            int y = Console.CursorTop;
            if (order.CustomerInfo.Address.Length > 45)
            {
                lineSplit = Utility.LineFormat(order.CustomerInfo.Address, 45);
                for (int i = 0; i < lineSplit.Count; i++)
                    Utility.Write(lineSplit[i] + "\n", 70, Console.CursorTop);
            }
            else
                Utility.Write(order.CustomerInfo.Address + "\n", 70, Console.CursorTop);
            Console.SetCursorPosition(0, y);
            if (address.Length > 45)
            {
                lineSplit = Utility.LineFormat(address, 45);
                Console.Write("  ║ Address: {0,-46}│ Address: ", lineSplit[0]);
                Utility.Write("║\n", 117, Console.CursorTop);
                for (int i = 1; i < lineSplit.Count; i++)
                {
                    Console.Write("  ║          {0,-46}│", lineSplit[i]);
                    Utility.Write("║\n", 117, Console.CursorTop);
                }
            }
            else
            {
                Console.Write("  ║ Address: {0,-46}│ Address: ", address);
                Utility.Write("║\n", 117, Console.CursorTop);
            }
            Console.Write("  ║ Seller      : {0,-41}│", order.Seller.StaffName);
            Utility.Write("║\n", 117, Console.CursorTop);
            Console.Write("  ║ Accountance : {0,-41}│     {1,-51} ║", order.Accountance.StaffName, "");
            Utility.Write("║\n", 117, Console.CursorTop);
            int[] lengthDatas = { 3, 62, 12, 8, 15 };
            Console.WriteLine("  ╟─────┬──────────────────────────────────────────────────┴─────────────┬──────────────┬──────────┬─────────────────╢");
            Console.WriteLine("  ║ {0,3} │ {1,-62} │ {2,12} │ {3,8} │ {4,15} ║", "NO", "Laptop Name", "Price(VNĐ)", "Quantity", "Amount(VNĐ");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╟", "─", "┼", "╢"));
            for (int i = 0; i < order.Laptops.Count; i++)
            {
                Decimal amount = (order.Laptops[i].Price * order.Laptops[i].Quantity);
                totalPayment += amount;
                Console.WriteLine("  ║ {0,3} │ {1,-62} │ {2,12:N0} │ {3,8} │ {4,15:N0} ║", i + 1,
                order.Laptops[i].LaptopName, order.Laptops[i].Price, order.Laptops[i].Quantity, amount);
            }
            Console.WriteLine("  ╟─────┴────────────────────────────────────────────────────────────────┴──────────────┴──────────┼─────────────────╢");
            Console.WriteLine("  ║ TOTAL PAYMENT                                                                                  │ {0,15:N0} ║", totalPayment);
            Console.WriteLine("  ║ CUSTOMER PAY                                                                                   │ {0,15:N0} ║", money);
            Console.WriteLine("  ║ GIVE CHANGE                                                                                    │ {0,15:N0} ║", money - totalPayment);
            Console.WriteLine("  ╚════════════════════════════════════════════════════════════════════════════════════════════════╧═════════════════╝");
            Console.CursorVisible = false;
        }
        public void ShowListOrder(List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                int posLeft = Utility.GetPosition("ORDER NOT FOUND!!!", 120);
                Utility.Write("ORDER NOT FOUND!!!", ConsoleColor.Red, posLeft, 15);
                return;
            }
            int[] lengthDatas = { 8, 40, 10, 19, 10, 7 };
            Utility.Write(Utility.GetLine(lengthDatas, "┌", "─", "┬", "┐\n"), 3, 7);
            Utility.Write(String.Format("│ {0,8} │ {1,-40} │ {2,-10} │ {3,-19} │ {4,-10} │         │\n",
                                "Order ID", "Customer name", "Phone", "Order date", "Status"), 3, Console.CursorTop);
            for (int i = 0; i < orders.Count; i++)
            {
                string status = orders[i].Status == Order.UNPAID ? "UNPAID" : orders[i].Status == Order.PAID ?
                               "PAID" : orders[i].Status == Order.CANCEL ? "CANCEL" : "PROCESSING";
                Utility.Write(Utility.GetLine(lengthDatas, "├", "─", "┼", "┤\n"), 3, Console.CursorTop);
                Utility.Write(String.Format("│ {0,8} │ {1,-40} │ {2,-10} │ {3,19:dd/MM/yyyy h:mm tt} │ {4,-10} │ ", orders[i].OrderId,
                orders[i].CustomerInfo.CustomerName, orders[i].CustomerInfo.Phone, orders[i].Date, status), 3, Console.CursorTop);
                Utility.Write("Payment", ConsoleColor.Cyan, Console.CursorLeft, Console.CursorTop);
                Console.WriteLine(" │");
            }
            Utility.Write(Utility.GetLine(lengthDatas, "└", "─", "┴", "┘\n"), 3, Console.CursorTop);
        }
        public void ShowFormCreate(Order order)
        {
            Utility.Clear(1, 5, 117, 24);
            int[] lengthDatas = { 63, 47 };
            Utility.Write(Utility.GetLine(lengthDatas, "┌", "─", "┬", "┐\n"), 1, 5);
            for (int i = 0; i < 17; i++)
            {
                Utility.Write(String.Format("│ {0,63} │ {1,47} │\n", "", ""), 1, Console.CursorTop);
            }
            Utility.Write(Utility.GetLine(lengthDatas, "└", "─", "┴", "┘\n"), 1, Console.CursorTop);

            Utility.Write("PRODUCT", 30, 6);
            Utility.Write("CUSTOMER INFORMATION\n\n", 83, 6);
            string[] cusInfo = { "Phone", "Full Name", "Address" };
            for (int i = 0; i < cusInfo.Length; i++)
            {
                Utility.Write($"{cusInfo[i]}: \n", 69, Console.CursorTop);
                Utility.PrintBox(48, 69, Console.CursorTop);
                Console.WriteLine("\n");
            }
            Utility.Write(order.CustomerInfo.Phone, 71, 10);
            Utility.Write(order.CustomerInfo.CustomerName, 71, 15);
            string address = order.CustomerInfo.Address ?? "";
            string addressSplit = address.Length < 43 ? address : "..." + address.Substring(address.Length - 40, 40);
            Utility.Write(addressSplit, 71, 20);
            Utility.Write("TAB, ESC", ConsoleColor.Red, 103, 28); Console.Write(": back");
            Utility.Write("▄▄▄▄▄▄▄▄▄▄▄▄▄▄", ConsoleColor.DarkCyan, 55, 25);
            Utility.Write(" CREATE ORDER ", ConsoleColor.White, ConsoleColor.DarkCyan, 55, 26);
            Utility.Write("▀▀▀▀▀▀▀▀▀▀▀▀▀▀", ConsoleColor.DarkCyan, 55, 27);

        }
        public void ShowLaptopInOrder(List<Laptop> laptops)
        {
            if (laptops == null || laptops.Count == 0) return;
            int[] lengthDatas = { 40, 8, 6 };
            Utility.Write(Utility.GetLine(lengthDatas, "┌", "─", "┬", "┐\n"), 2, 8);
            Utility.Write(String.Format("│ {0,-40} │ {1,8} │        │\n", "Laptop Name", "Quantity"), 2, Console.CursorTop);
            for (int i = 0; i < laptops.Count; i++)
            {
                int lengthName = 40;
                string name = (laptops[i].LaptopName.Length > lengthName) ?
                laptops[i].LaptopName.Remove(lengthName - 3, laptops[i].LaptopName.Length - lengthName + 3) + "..." : laptops[i].LaptopName;
                Utility.Write(Utility.GetLine(lengthDatas, "├", "─", "┼", "┤\n"), 2, Console.CursorTop);
                Utility.Write(String.Format("│ {0,-40} │ {1,8} │ ", name, laptops[i].Quantity), 2, Console.CursorTop);
                Utility.Write("Delete", ConsoleColor.Cyan, Console.CursorLeft, Console.CursorTop);
                Console.WriteLine(" │");
            }
            Utility.Write(Utility.GetLine(lengthDatas, "└", "─", "┴", "┘\n"), 2, Console.CursorTop);
        }
        public bool AddLaptoptoOrder(Laptop laptop)
        {
            int count = laptopBL.GetById(laptop.LaptopId).Quantity;
            int quantity = 0;
            int index = NewOrder.Laptops.IndexOf(laptop);
            if (index != -1)
            {
                quantity = laptop.Quantity + NewOrder.Laptops[index].Quantity;
                if (quantity > count) return false;
                NewOrder.Laptops[index].Quantity += laptop.Quantity;
            }
            else
            {
                if (laptop.Quantity > count) return false;
                NewOrder.Laptops.Add(laptop);
            }
            return true;
        }
    }
}


// xunit.runner.json
// {
//   "$schema": "https://xunit.net/schema/current/xunit.runner.schema.json",
//   "maxParallelThreads": 1
// }{
//   "$schema": "https://xunit.net/schema/current/xunit.runner.schema.json",
//   "maxParallelThreads": 1
// }
// DALTest.csprj
// <ItemGroup>
//     <Content Include="xunit.runner.json" CopyToOutputDirectory="PreserveNewest" />
//   </ItemGroup>