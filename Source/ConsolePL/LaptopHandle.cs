using System;
using System.Collections.Generic;
using Persistance;
using BL;
using Utilities;

namespace ConsolePL
{
    public class LaptopHandle
    {
        private LaptopBL laptopBL = new LaptopBL();
        private OrderHandle orderH = new OrderHandle();
        public void SearchLaptops(Staff staff)
        {
            int index = 0, laptopCount, pageCount, page = 1;
            string searchValue = "";
            List<Laptop> laptops;
            var listLaptop = laptopBL.Search(searchValue);
            Console.Clear();
            if (listLaptop == null || listLaptop.Count == 0)
            {
                ConsoleUtility.PrintTitle("▬▬▬▬ SEARCH LAPTOP ▬▬▬▬", true);
                ConsoleUtility.Write("\n  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                ConsoleUtility.PressAnyKey("back");
                return;
            }
            laptops = Laptop.SplitList(listLaptop, index, 10);
            laptopCount = listLaptop == null ? 0 : listLaptop.Count;
            pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
            ShowListLaptop(laptops, "SEARCH LAPTOP");
            ConsoleUtility.ShowPageNumber(pageCount, page);
            ShowFeatures(staff);
            ConsoleKey key = new ConsoleKey();
            do
            {
                Console.CursorVisible = false;
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.F:
                        Console.CursorVisible = true;
                        Console.WriteLine("\n  Press combination CTRL + H to view instructions");
                        while (true)
                        {
                            Console.Write("  → Input search value: ");
                            searchValue = ConsoleUtility.GetString(out keyInfo, new[] { ConsoleKey.H });
                            key = keyInfo.Key;
                            Console.WriteLine();
                            if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && key == ConsoleKey.H)
                            {
                                Console.WriteLine("\n  * NAME|MANUFACTORY|CATEGORY: Search by NAME or MANUFACTORY or CATEGORY");
                                Console.WriteLine("  * NAME|MANUFACTORY|CATEGORY # desc|asc: Search by NAME or MANUFACTORY or CATEGORY, PRICE: High → Low or Low → High");
                                Console.WriteLine("  * MANUFACTORY # CATEGORY: Search by MANUFACTORY and CATEGORY");
                                Console.WriteLine("  * MANUFACTORY # CATEGORY # desc|asc: Search by MANUFACTORY and CATEGORY, PRICE: High → Low or Low → High\n");
                                continue;
                            }
                            break;
                        }
                        listLaptop = laptopBL.Search(searchValue);
                        if (listLaptop == null || listLaptop.Count == 0)
                        {
                            Console.Clear();
                            ConsoleUtility.PrintTitle("▬▬▬▬ SEARCH LAPTOP ▬▬▬▬", true);
                            ConsoleUtility.Write("\n  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                        }
                        else
                        {
                            index = 0; page = 1; laptopCount = listLaptop.Count;
                            pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                            laptops = Laptop.SplitList(listLaptop, index, 10);
                            ShowListLaptop(laptops, "SEARCH LAPTOP");
                            ConsoleUtility.ShowPageNumber(pageCount, page);
                        }
                        ShowFeatures(staff);
                        break;
                    case ConsoleKey.D:
                        Console.Write("\n  → Input Laptop ID to view details(input 0 to cancel): ");
                        int id = ConsoleUtility.GetNumber("ID", 0);
                        if (id != 0)
                        {
                            Laptop laptop = laptopBL.GetById(id);
                            ViewLaptopDetails(laptop);
                            ConsoleUtility.PressAnyKey("back");
                        }
                        ShowListLaptop(laptops, "SEARCH LAPTOP");
                        ConsoleUtility.ShowPageNumber(pageCount, page);
                        ShowFeatures(staff);
                        break;
                    case ConsoleKey.C:
                        orderH.CreateOrder(staff);
                        ShowListLaptop(laptops, "SEARCH LAPTOP");
                        ShowFeatures(staff);
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
                        laptops = Laptop.SplitList(listLaptop, index, 10);
                        ShowListLaptop(laptops, "SEARCH LAPTOP");
                        ConsoleUtility.ShowPageNumber(pageCount, page);
                        ShowFeatures(staff);
                        break;
                }
            } while (key != ConsoleKey.Escape);
            Console.CursorVisible = true;
        }
        public void ShowListLaptop(List<Laptop> laptops, string title)
        {
            Console.Clear();
            ConsoleUtility.PrintTitle("▬▬▬▬ " + title + " ▬▬▬▬", false);
            Console.CursorVisible = false;
            int[] lengthDatas = { 3, 30, 11, 11, 21, 6, 12 };
            ConsoleUtility.PrintLine(lengthDatas, "  ╟", "─", "┬", "╢\n");
            Console.WriteLine("  ║ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12} ║", "ID", "Laptop Name",
                               "Manufactory", "Category", "CPU", "RAM", "Price(VND)");
            ConsoleUtility.PrintLine(lengthDatas, "  ╟", "─", "┼", "╢\n");
            for (int i = 0; i < laptops.Count; i++)
            {
                int lengthName = 27;
                string name = (laptops[i].Name.Length > lengthName) ?
                laptops[i].Name.Remove(lengthName, laptops[i].Name.Length - lengthName) + "..." : laptops[i].Name;
                int index = laptops[i].Ram.IndexOf('B') + 1;
                string ram = laptops[i].Ram.Substring(0, index);
                Console.WriteLine("  ║ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12:N0} ║", laptops[i].ID,
                                name, laptops[i].ManufactoryInfo.Name, laptops[i].CategoryInfo.Name,
                                laptops[i].CPU, ram, laptops[i].Price);
            }
            ConsoleUtility.PrintLine(lengthDatas, "  ╚", "═", "╧", "╝\n");
        }
        private void ViewLaptopDetails(Laptop laptop)
        {
            if (laptop == null)
            {
                ConsoleUtility.Write("  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                return;
            }
            Console.Clear();
            string data;
            string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "LAPTOP INFORMATION";
            int lengthLine = line.Length + 2;
            int posLeft = ConsoleUtility.GetPosition(title, lengthLine);
            Console.WriteLine("  ┌{0}┐", line);
            Console.Write("  │{0," + (posLeft - 1) + "}", ""); ConsoleUtility.Write(title, ConsoleColor.Green);
            Console.WriteLine("{0," + (lengthLine - title.Length - posLeft) + "}", "│");
            Console.WriteLine("  ├{0}┤", line);
            Console.WriteLine("  │ Laptop Id:   {0,-99} │", laptop.ID);
            Console.WriteLine("  │ Laptop name: {0,-99} │", laptop.Name);
            Console.WriteLine("  │ Manufactory: {0,-99} │", laptop.ManufactoryInfo.Name);
            Console.WriteLine("  │ Category:    {0,-99} │", laptop.CategoryInfo.Name);
            Console.WriteLine("  │ CPU:         {0,-99} │", laptop.CPU);
            Console.WriteLine("  │ RAM:         {0,-99} │", laptop.Ram);
            Console.WriteLine("  │ Hard drive:  {0,-99} │", laptop.HardDrive);
            Console.WriteLine("  │ VGA:         {0,-99} │", laptop.VGA);
            data = laptop.Display;
            if (data.Length > 99)
            {
                var lines = ConsoleUtility.SplitLine(data, 99);
                Console.WriteLine("  │ Display:     {0,-99} │", lines[0]);
                for (int i = 1; i < lines.Count; i++)
                    Console.WriteLine("  │              {0,-99} │", lines[i]);
            }
            else
                Console.WriteLine("  │ Display:     {0,-99} │", data);
            Console.WriteLine("  │ Battery:     {0,-99} │", laptop.Battery);
            Console.WriteLine("  │ Weight:      {0,-99} │", laptop.Weight.ToString() + " Kg");
            Console.WriteLine("  │ Materials:   {0,-99} │", laptop.Materials);
            data = laptop.Ports;
            if (data.Length > 99)
            {
                var lines = ConsoleUtility.SplitLine(data, 99);
                Console.WriteLine("  │ Ports:       {0,-99} │", lines[0]);
                for (int i = 1; i < lines.Count; i++)
                    Console.WriteLine("  │              {0,-99} │", lines[i]);
            }
            else
                Console.WriteLine("  │ Ports:       {0,-99} │", data);
            Console.WriteLine("  │ Network and connection: {0,-88} │", laptop.NetworkAndConnection);
            Console.WriteLine("  │ Security:    {0,-99} │", laptop.Security);
            Console.WriteLine("  │ Keyboard:    {0,-99} │", laptop.Keyboard);
            Console.WriteLine("  │ Audio:       {0,-99} │", laptop.Audio);
            Console.WriteLine("  │ Size:        {0,-99} │", laptop.Size);
            Console.WriteLine("  │ Operating system: {0,-94} │", laptop.OS);
            Console.WriteLine("  │ Quantity:    {0,-99} │", laptop.Quantity);
            Console.WriteLine("  │ Price:       {0,-99} │", laptop.Price.ToString("N0") + " VND");
            Console.WriteLine("  │ Warranty period: {0,-95} │", laptop.WarrantyPeriod);
            Console.WriteLine("  └{0}┘", line);
        }
        public void ShowFeatures(Staff staff)
        {
            if (staff.Role == Staff.SELLER)
            {
                Console.Write("  ● Press '");
                ConsoleUtility.Write("F", ConsoleColor.Yellow);
                Console.Write("' to search laptops, '");
                ConsoleUtility.Write("D", ConsoleColor.Yellow);
                Console.Write("' to view laptop details, '");
                ConsoleUtility.Write("C", ConsoleColor.Yellow);
                Console.Write("' to Create Order, '");
                ConsoleUtility.Write("ESC", ConsoleColor.Red);
                Console.WriteLine("' to exit");
            }
        }
    }
}