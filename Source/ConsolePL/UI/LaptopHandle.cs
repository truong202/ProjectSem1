using System;
using System.Collections.Generic;
using Persistance;
using BL;

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
                Utility.PrintTitle("▬▬▬▬ SEARCH LAPTOP ▬▬▬▬", true);
                Utility.Write("\n  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                Utility.PressAnyKey("back");
                return;
            }
            laptops = Laptop.SplitList(listLaptop, index, 10);
            laptopCount = listLaptop == null ? 0 : listLaptop.Count;
            pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
            ShowListLaptop(laptops, "SEARCH LAPTOP");
            Utility.ShowPageNumber(pageCount, page);
            ShowFeature(staff);
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
                            searchValue = Utility.GetString(out keyInfo, new[] { ConsoleKey.H });
                            key = keyInfo.Key;
                            Console.WriteLine();
                            if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0 && key == ConsoleKey.H)
                            {
                                Console.WriteLine("\n  * Name|Manufactory|Category: Search by Name or Manufactory or Category");
                                Console.WriteLine("  * Name|Manufactory|Category # desc|asc: Search by Name or Manufactory or Category, Price: High → Low or Low → High");
                                Console.WriteLine("  * Manufactory # Category: Search by Manufactory and Category");
                                Console.WriteLine("  * Manufactory # Category # desc|asc: Search by Manufactory and Category, Price: High → Low or Low → High\n");
                                continue;
                            }
                            break;
                        }
                        listLaptop = laptopBL.Search(searchValue);
                        if (listLaptop == null || listLaptop.Count == 0)
                        {
                            Console.Clear();
                            Utility.PrintTitle("▬▬▬▬ SEARCH LAPTOP ▬▬▬▬", true);
                            Utility.Write("\n  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                        }
                        else
                        {
                            index = 0; page = 1; laptopCount = listLaptop.Count;
                            pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                            laptops = Laptop.SplitList(listLaptop, index, 10);
                            ShowListLaptop(laptops, "SEARCH LAPTOP");
                            Utility.ShowPageNumber(pageCount, page);
                        }
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.D:
                        Console.Write("\n  → Input Laptop ID to view details: ");
                        int id = Utility.GetNumber("ID", 1);
                        Laptop laptop = laptopBL.GetById(id);
                        ViewLaptopDetails(laptop);
                        Utility.PressAnyKey("back");
                        ShowListLaptop(laptops, "SEARCH LAPTOP");
                        Utility.ShowPageNumber(pageCount, page);
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.C:
                        orderH.CreateOrder(staff);
                        ShowListLaptop(laptops, "SEARCH LAPTOP");
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            page--;
                            index -= 10;
                            laptops = Laptop.SplitList(listLaptop, index, 10);
                            ShowListLaptop(laptops, "SEARCH LAPTOP");
                            Utility.ShowPageNumber(pageCount, page);
                            ShowFeature(staff);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (page < pageCount)
                        {
                            page++;
                            index += 10;
                            laptops = Laptop.SplitList(listLaptop, index, 10);
                            ShowListLaptop(laptops, "SEARCH LAPTOP");
                            Utility.ShowPageNumber(pageCount, page);
                            ShowFeature(staff);
                        }
                        break;
                }
            } while (key != ConsoleKey.Escape);
            Console.CursorVisible = true;
        }
        public void ShowListLaptop(List<Laptop> laptops, string title)
        {
            Console.Clear();
            Utility.PrintTitle("▬▬▬▬ " + title + " ▬▬▬▬", false);
            Console.CursorVisible = false;
            int[] lengthDatas = { 3, 30, 11, 11, 21, 6, 12 };
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╟", "─", "┬", "╢"));
            Console.WriteLine("  ║ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12} ║", "ID", "Laptop Name",
                               "Manufactory", "Category", "CPU", "RAM", "Price(VNĐ)");
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╟", "─", "┼", "╢"));
            for (int i = 0; i < laptops.Count; i++)
            {
                int lengthName = 27;
                string name = (laptops[i].LaptopName.Length > lengthName) ?
                laptops[i].LaptopName.Remove(lengthName, laptops[i].LaptopName.Length - lengthName) + "..." : laptops[i].LaptopName;
                int index = laptops[i].Ram.IndexOf('B') + 1;
                string ram = laptops[i].Ram.Substring(0, index);
                Console.WriteLine("  ║ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12:N0} ║", laptops[i].LaptopId,
                                name, laptops[i].ManufactoryInfo.ManufactoryName, laptops[i].CategoryInfo.CategoryName,
                                laptops[i].CPU, ram, laptops[i].Price);
            }
            Console.WriteLine(Utility.GetLine(lengthDatas, "  ╚", "═", "╧", "╝"));
        }
        private void ViewLaptopDetails(Laptop laptop)
        {
            Console.CursorVisible = false;
            if (laptop == null)
            {
                Utility.Write("  LAPTOP NOT FOUND!\n", ConsoleColor.Red);
                return;
            }
            Console.Clear();
            string data;
            string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "LAPTOP INFORMATION";
            int lengthLine = line.Length + 2;
            int posLeft = Utility.GetPosition(title, lengthLine);
            Console.WriteLine("  ┌{0}┐", line);
            Console.Write("  │{0," + (posLeft - 1) + "}", ""); Utility.Write(title, ConsoleColor.Green);
            Console.WriteLine("{0," + (lengthLine - title.Length - posLeft) + "}", "│");
            Console.WriteLine("  ├{0}┤", line);
            Console.WriteLine("  │ Laptop Id:   {0,-99} │", laptop.LaptopId);
            Console.WriteLine("  │ Laptop name: {0,-99} │", laptop.LaptopName);
            Console.WriteLine("  │ Manufactory: {0,-99} │", laptop.ManufactoryInfo.ManufactoryName);
            Console.WriteLine("  │ Category:    {0,-99} │", laptop.CategoryInfo.CategoryName);
            Console.WriteLine("  │ CPU:         {0,-99} │", laptop.CPU);
            Console.WriteLine("  │ RAM:         {0,-99} │", laptop.Ram);
            Console.WriteLine("  │ Hard drive:  {0,-99} │", laptop.HardDrive);
            Console.WriteLine("  │ VGA:         {0,-99} │", laptop.VGA);
            data = laptop.Display;
            if (data.Length > 99)
            {
                var lines = Utility.SplitLine(data, 99);
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
                var lines = Utility.SplitLine(data, 99);
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
            Console.WriteLine("  │ Price:       {0,-99} │", laptop.Price.ToString("N0"));
            Console.WriteLine("  │ Warranty period: {0,-95} │", laptop.WarrantyPeriod);
            Console.WriteLine("  └{0}┘", line);
        }
        public void ShowFeature(Staff staff)
        {
            if (staff.Role == Staff.SELLER)
            {
                Console.Write("\n  ● Press '");
                Utility.Write("F", ConsoleColor.Yellow);
                Console.Write("' to search laptops, '");
                Utility.Write("D", ConsoleColor.Yellow);
                Console.Write("' to view laptop details, '");
                Utility.Write("C", ConsoleColor.Yellow);
                Console.Write("' to Create Order, '");
                Utility.Write("ESC", ConsoleColor.Red);
                Console.WriteLine("' to exit");
            }
        }
    }
}