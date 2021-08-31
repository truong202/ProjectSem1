﻿using System;
using Persistance;
using BL;
using System.Collections.Generic;
using System.Text;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            // while (true)
            // {
            //     ViewLaptopDetails();
            // }
            Staff staff = Login();
            int role = staff.Role;
            short choice;
            string title;
            string[] menu;
            switch (role)
            {
                case Staff.SELLER:
                    title = "MENU SELLER";
                    menu = new[] { "SEARCH LAPTOPS", "EXIT" };
                    do
                    {
                        Console.Clear();
                        choice = Menu(title, menu);
                        switch (choice)
                        {
                            case 1:
                                SearchLaptops(staff);
                                break;
                        }
                    } while (choice != menu.Length);
                    break;
                case Staff.ACCOUNTANCE:
                    Console.WriteLine("accountance");

                    break;
            }
        }

        static void SearchLaptops(Staff staff)
        {
            int offset = 0;
            ConsoleKey key = new ConsoleKey();
            LaptopBL laptopBL = new LaptopBL();
            string searchValue = "";
            int laptopCount = laptopBL.GetCount(searchValue);
            if (laptopCount == 0)
            {
                Console.WriteLine("Laptop not found!");
                return;
            }
            int pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
            int page = (laptopCount > 0) ? 1 : 0;
            var laptops = laptopBL.Search(searchValue, offset);
            Display(laptops, page, pageCount);
            do
            {
                Console.CursorVisible = false;
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.F:
                        Console.CursorVisible = true;
                        offset = 0;
                        Console.WriteLine();
                        Console.Write(" → Input search value: "); searchValue = Console.ReadLine();
                        laptopCount = laptopBL.GetCount(searchValue);
                        pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                        page = (laptopCount > 0) ? 1 : 0;
                        laptops = laptopBL.Search(searchValue, offset);
                        Display(laptops, page, pageCount);
                        break;
                    case ConsoleKey.C:
                        if (laptops.Count > 0 || laptops != null)
                        {
                            CreateOrder(staff);
                        }
                        break;
                    case ConsoleKey.D:
                        if (laptops.Count > 0 && laptops != null)
                        {
                            ViewLaptopDetails();
                            Display(laptops, page, pageCount);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            page--;
                            offset -= 10;
                            laptops = laptopBL.Search(searchValue, offset);
                            Display(laptops, page, pageCount);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (page < pageCount)
                        {
                            page++;
                            offset += 10;
                            laptops = laptopBL.Search(searchValue, offset);
                            Display(laptops, page, pageCount);
                        }
                        break;
                }
            } while (key != ConsoleKey.Escape && key != ConsoleKey.C);
            Console.CursorVisible = true;
        }
        static void CreateOrder(Staff staff)
        {
            Order order = new Order();
            order.Seller.StaffId = staff.StaffId;
            LaptopBL laptopBL = new LaptopBL();
            Laptop laptop;
            ConsoleKey isContinue = ConsoleKey.Y;
            Console.WriteLine();
            Console.Write(" * Input List Laptop"); 
            do
            {
                Console.CursorVisible = true;
                Console.WriteLine();
                laptop = laptopBL.GetById(GetNumber("ID"));
                if (laptop == null)
                {
                    Console.WriteLine(" Laptop not found!");
                }
                else
                {
                    int quantity = GetNumber("Quantity");
                    int index = order.Laptops.IndexOf(laptop);
                    if (index != -1)
                    {
                        quantity += order.Laptops[index].Quantity;
                    }
                    if (quantity > laptop.Quantity)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" The number of laptop in the store is not enough!");
                        Console.ResetColor();
                    }
                    else
                    {
                        laptop.Quantity = quantity;
                        if (index != -1) order.Laptops[index] = laptop;
                        else order.Laptops.Add(laptop);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(" Add laptop to order completed!");
                        Console.ResetColor();
                    }
                    Console.CursorVisible = false;
                    Console.Write(" Would you like to add another laptop to this order? [Y/N]");
                    isContinue = PressYN();
                    Console.WriteLine();
                }
            } while (isContinue == ConsoleKey.Y);
            if (order.Laptops.Count > 0)
            {
                Console.WriteLine("\n * Input Customer information");
                Console.CursorVisible = true;
                Console.Write(" → Phone: "); order.CustomerInfo.Phone = GetPhone();
                Customer customer = new CustomerBL().GetByPhone(order.CustomerInfo.Phone);
                if (customer != null)
                {
                    order.CustomerInfo = customer;
                    Console.WriteLine(" → Customer name: " + order.CustomerInfo.CustomerName);
                    Console.WriteLine(" → Address: " + order.CustomerInfo.Address);
                }
                else
                {
                    Console.Write(" → Customer name: "); order.CustomerInfo.CustomerName = Utility.Standardize(GetName());
                    Console.Write(" → Address: "); order.CustomerInfo.Address = Console.ReadLine();
                }
                bool result = new OrderBL().CreateOrder(order);
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Create order completed!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Create order not complete!");
                    Console.ResetColor();
                }
                Console.CursorVisible = false;
                Console.Write(" Press any key to back main menu..."); Console.ReadKey(true);
            }
        }
        static void ViewLaptopDetails()
        {
            Console.CursorVisible = true;
            Laptop laptop = new LaptopBL().GetById(GetNumber("ID"));
            if (laptop == null) Console.WriteLine(" Laptop not found!");
            else
            {
                Console.Clear();
                string data;
                string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
                string title = "Laptop infomation";
                int lengthLine = line.Length + 2;
                int position = lengthLine / 2 + title.Length / 2 - 1;
                Console.WriteLine(" ┌{0}┐", line);
                Console.WriteLine(" │{0," + position + "}{1," + (lengthLine - position - 1) + "}", title, "│");
                Console.WriteLine(" ├{0}┤", line);
                Console.WriteLine(" │{0," + (lengthLine - 1) + "}", "│");
                Console.WriteLine(" │ Laptop Id:   {0}{1," + (lengthLine - 15 - laptop.LaptopId.ToString().Length) + "}", laptop.LaptopId, "│");
                Console.WriteLine(" │ Laptop name: {0}{1," + (lengthLine - 15 - laptop.LaptopName.Length) + "}", laptop.LaptopName, "│");
                Console.WriteLine(" │ Manufactory: {0}{1," + (lengthLine - 15 - laptop.ManufactoryInfo.ManufactoryName.Length) + "}", laptop.ManufactoryInfo.ManufactoryName, "│");
                Console.WriteLine(" │ Category:    {0}{1," + (lengthLine - 15 - laptop.CategoryInfo.CategoryName.Length) + "}", laptop.CategoryInfo.CategoryName, "│");
                Console.WriteLine(" │ CPU:         {0}{1," + (lengthLine - 15 - laptop.CPU.Length) + "}", laptop.CPU, "│");
                Console.WriteLine(" │ RAM:         {0}{1," + (lengthLine - 15 - laptop.Ram.Length) + "}", laptop.Ram, "│");
                Console.WriteLine(" │ Hard drive:  {0}{1," + (lengthLine - 15 - laptop.HardDrive.Length) + "}", laptop.HardDrive, "│");
                Console.WriteLine(" │ VGA:         {0}{1," + (lengthLine - 15 - laptop.VGA.Length) + "}", laptop.VGA, "│");
                data = laptop.Display;
                if (data.Length > 99)
                {
                    var lines = Utility.LineFormat(data, 99);
                    Console.WriteLine(" │ Display:     {0}{1," + (lengthLine - 15 - lines[0].Length) + "}", lines[0], "│");
                    for (int i = 1; i < lines.Count; i++)
                        Console.WriteLine(" │              {0}{1," + (lengthLine - 15 - lines[i].Length) + "}", lines[i], "│");
                }
                else
                {
                    Console.WriteLine(" │ Display:     {0}{1," + (lengthLine - 15 - data.Length) + "}", data, "│");
                }
                Console.WriteLine(" │ Battery:     {0}{1," + (lengthLine - 15 - laptop.Battery.Length) + "}", laptop.Battery, "│");
                Console.WriteLine(" │ Weight:      {0}{1," + (lengthLine - 15 - laptop.Weight.Length) + "}", laptop.Weight, "│");
                Console.WriteLine(" │ Materials:   {0}{1," + (lengthLine - 15 - laptop.Materials.Length) + "}", laptop.Materials, "│");
                data = laptop.Ports;
                if (data.Length > 99)
                {
                    var lines = Utility.LineFormat(data, 99);
                    Console.WriteLine(" │ Ports:       {0}{1," + (lengthLine - 15 - lines[0].Length) + "}", lines[0], "│");
                    for (int i = 1; i < lines.Count; i++)
                        Console.WriteLine(" │              {0}{1," + (lengthLine - 15 - lines[i].Length) + "}", lines[i], "│");
                }
                else
                {
                    Console.WriteLine(" │ Ports:       {0}{1," + (lengthLine - 15 - data.Length) + "}", data, "│");
                }
                Console.WriteLine(" │ Network and connection: {0}{1," + (lengthLine - 26 - laptop.NetworkAndConnection.Length) + "}", laptop.NetworkAndConnection, "│");
                Console.WriteLine(" │ Security:    {0}{1," + (lengthLine - 15 - laptop.Security.Length) + "}", laptop.Security, "│");
                Console.WriteLine(" │ Keyboard:    {0}{1," + (lengthLine - 15 - laptop.Keyboard.Length) + "}", laptop.Keyboard, "│");
                Console.WriteLine(" │ Audio:       {0}{1," + (lengthLine - 15 - laptop.Audio.Length) + "}", laptop.Audio, "│");
                Console.WriteLine(" │ Size:        {0}{1," + (lengthLine - 15 - laptop.Size.Length) + "}", laptop.Size, "│");
                Console.WriteLine(" │ Operating system: {0}{1," + (lengthLine - 20 - laptop.OS.Length) + "}", laptop.OS, "│");
                Console.WriteLine(" │ Quantity:    {0}{1," + (lengthLine - 15 - laptop.Quantity.ToString().Length) + "}", laptop.Quantity, "│");
                string price = laptop.Price.ToString("N0") + " VNĐ";
                Console.WriteLine(" │ Price:       {0}{1," + (lengthLine - 15 - price.Length) + "}", price, "│");
                Console.WriteLine(" │ Warranty period: {0}{1," + (lengthLine - 19 - laptop.WarrantyPeriod.Length) + "}", laptop.WarrantyPeriod, "│");
                Console.WriteLine(" └{0}┘", line);

            }
            Console.CursorVisible = false;
            Console.WriteLine("  Press any key to back..."); Console.ReadKey(true);
        }
        static void Display(List<Laptop> laptops, int page, int pageCount)
        {
            Console.Clear();
            Console.CursorVisible = false;
            if (laptops.Count == 0 || laptops == null)
            {
                Console.WriteLine(" Laptop not found!");
                Console.WriteLine("\n → Press 'F' to search laptops");
                Console.WriteLine(" → Press 'ESC' to exit");
            }
            else
            {
                List<string[]> lines = new List<string[]>();
                lines.Add(new[] { "ID", "Manufactory", "Laptop Name", "CPU", "RAM", "Quantity", "Price(VNĐ)" });
                foreach (var laptop in laptops)
                {
                    int lengthName = 30;
                    string id = laptop.LaptopId.ToString();
                    string name = (laptop.LaptopName.Length > lengthName) ?
                    laptop.LaptopName.Remove(lengthName, laptop.LaptopName.Length - lengthName) + "..." : laptop.LaptopName;
                    int index = laptop.Ram.IndexOf('B') + 1;
                    string ram = laptop.Ram.Substring(0, index);
                    string quantity = laptop.Quantity.ToString();
                    string price = laptop.Price.ToString("N0");
                    lines.Add(new[] { id, laptop.ManufactoryInfo.ManufactoryName, name, laptop.CPU, ram, quantity, price });
                }
                string[] table = Utility.GetTable(lines);
                foreach (string line in table) Console.WriteLine(" " + line);
                string nextPage = (page > 0 && page < pageCount) ? "►" : " ";
                string prePage = (page > 1) ? "◄" : " ";
                string pages = prePage + $"      [{page}]      " + nextPage;
                if (page > 0 && pageCount > 1)
                {
                    int position = table[0].Length / 2 + pages.Length / 2 + 1;
                    Console.WriteLine(String.Format("{0," + position + "}", pages));
                }
                Console.WriteLine("\n → Press 'F' to search laptops");
                Console.WriteLine(" → Press 'D' to view laptop details");
                Console.WriteLine(" → Press 'C' to Create Order");
                Console.WriteLine(" → Press 'ESC' to exit");
            }
        }
        static ConsoleKey PressYN()
        {
            ConsoleKey key = new ConsoleKey();
            while (true)
            {
                Console.CursorVisible = false;
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                if (key == ConsoleKey.N || key == ConsoleKey.Y)
                    return key;
            }
        }
        static short Menu(string title, string[] menuItems)
        {
            short choose = 0;
            string input;
            Console.WriteLine(Utility.GetMenu(title, menuItems));
            Console.Write("\n → Your choice: ");
            while (true)
            {
                input = Console.ReadLine();
                if (short.TryParse(input, out choose) && choose >= 1 && choose <= menuItems.Length) return choose;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" → Entered incorrectly!");
                Console.ResetColor();
                Console.Write(" → re-enter: ");
            }
        }
        static Staff Login()
        {
            string username, password;
            Staff staff;
            do
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════╗");
                Console.WriteLine("║                                               ║");
                Console.WriteLine("║                     LOGIN                     ║");
                Console.WriteLine("║                                               ║");
                Console.WriteLine("╚═══════════════════════════════════════════════╝");
                Console.Write("\n → Username: ");
                while (true)
                {
                    username = Console.ReadLine();
                    try
                    {
                        Staff.CheckUsername(username);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" " + e.Message); Console.ResetColor();
                        Console.Write(" → Re-enter Username: ");
                    }
                }
                Console.Write(" → Password: ");
                while (true)
                {
                    password = GetPassword();
                    Console.WriteLine();
                    try
                    {
                        Staff.CheckPassword(password);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" " + e.Message); Console.ResetColor();
                        Console.Write(" → Re-enter Password: ");
                    }
                }
                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                if (staff == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect UserName or Password!"); Console.ResetColor();
                    Console.Write("Press any key to login again..."); Console.ReadKey(true);
                }
            } while (staff == null);
            return staff;
        }
        static int GetNumber(string message)
        {
            int number;
            Console.Write(" → Enter {0}: ", message);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number) && number > 0) return number;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" → Invalid {0}!", message);
                Console.ResetColor();
                Console.Write(" → Re-enter {0}: ", message);
            }
        }
        static string GetName()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine();
                try
                {
                    Customer.CheckName(name);
                    return name;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" " + e.Message);
                    Console.ResetColor();
                    Console.Write(" → Re-enter customer name: ");
                }
            }
        }
        static string GetPhone()
        {
            string phone;
            while (true)
            {
                phone = Console.ReadLine();
                try
                {
                    Customer.CheckPhone(phone);
                    return phone;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" " + e.Message);
                    Console.ResetColor();
                    Console.Write(" → Re-enter phone: ");
                }
            }
        }
        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}
