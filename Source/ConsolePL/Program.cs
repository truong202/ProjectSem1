using System;
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
            CreateOrder();
            // Staff staff = Login();
            // int role = staff.Role;
            // switch (role)
            // {
            //     case StaffRole.SELLER:
            //         short choice;
            //         string title = "MENU SELLER";
            //         string[] menu = { "CREATE ORDER", "EXIT" };
            //         do
            //         {
            //             choice = Menu(title, menu);
            //             switch (choice)
            //             {
            //                 case 1:
            //                     break;
            //                 case 2:
            //                     break;
            //             }
            //         } while (choice != menu.Length);
            //         break;
            //     case StaffRole.ACCOUNTANCE:
            // Console.WriteLine("accountance");

            //         break;
            // }
        }

        static void CreateOrder()
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
                key = PressKey();
                switch (key)
                {
                    case ConsoleKey.F:
                        Console.CursorVisible = true;
                        Console.Write(" → Input search value: "); searchValue = Console.ReadLine();
                        laptopCount = laptopBL.GetCount(searchValue);
                        pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                        page = (laptopCount > 0) ? 1 : 0;
                        laptops = laptopBL.Search(searchValue, offset);
                        Display(laptops, page, pageCount);
                        break;
                    case ConsoleKey.C:
                        Console.CursorVisible = true;
                        Console.Write(" → Input search value: ");
                        break;
                    case ConsoleKey.D:
                        Console.CursorVisible = true;
                        Console.Write(" → Input search value: ");
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
            } while (key != ConsoleKey.Escape);
            Console.CursorVisible = true;
        }
        static void Display(List<Laptop> laptops, int page, int pageCount)
        {
            Console.Clear();
            List<string[]> lines = new List<string[]>();
            lines.Add(new[] { "Manufactory", "Laptop Name", "CPU", "RAM", "Quantity", "Price(VNĐ)" });
            foreach (var laptop in laptops)
            {
                int lengthName = 35;
                string name = (laptop.LaptopName.Length > lengthName) ?
                laptop.LaptopName.Remove(lengthName, laptop.LaptopName.Length - lengthName) + "..." : laptop.LaptopName;
                int index = laptop.Ram.IndexOf('B') + 1;
                string ram = laptop.Ram.Substring(0, index);
                string quantity = laptop.Quantity.ToString();
                string price = laptop.Price.ToString("N0");
                lines.Add(new[] { laptop.ManufactoryInfo.ManufactoryName, name, laptop.CPU, ram, quantity, price });
            }
            string[] table = ConsoleUtility.GetTable(lines);
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
        static ConsoleKey PressKey()
        {
            ConsoleKey key = new ConsoleKey();
            while (true)
            {
                Console.CursorVisible = false;
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Escape || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow ||
                    key == ConsoleKey.F || key == ConsoleKey.C || key == ConsoleKey.D)
                    return key;
            }
        }
        static short Menu(string title, string[] menuItems)
        {
            short choose = 0;
            string input;
            Console.WriteLine(ConsoleUtility.GetMenu(title, menuItems));
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
                Console.Write("Username: ");
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
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        Console.Write("Re-enter Username: ");
                    }
                }
                Console.Write("Password: ");
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
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        Console.Write("Re-enter Password: ");
                    }
                }
                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                if (staff == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect UserName or Password!");
                    Console.ResetColor();
                }
            } while (staff == null);
            return staff;
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
