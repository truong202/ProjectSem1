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
            int offset = 0;
            ConsoleKey key = new ConsoleKey();
            string searchValue = "";
            int laptopCount = laptopBL.GetCount(searchValue);
            int pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
            int page = (laptopCount > 0) ? 1 : 0;
            var laptops = laptopBL.Search(searchValue, offset);
            ShowListLaptop(laptops, page, pageCount);
            ShowFeature(staff);
            do
            {
                Console.CursorVisible = false;
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.F:
                        offset = 0; page = 1;
                        Console.Write("\n  → Input search value(Press enter to show all laptop): ");
                        Console.CursorVisible = true;
                        searchValue = Console.ReadLine().Trim();
                        laptopCount = laptopBL.GetCount(searchValue);
                        pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                        laptops = laptopBL.Search(searchValue, offset);
                        ShowListLaptop(laptops, page, pageCount);
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.D:
                        Console.Write("\n  → Input Laptop ID to view details: ");
                        int id = Utility.GetNumber("ID", 1);
                        Laptop laptop = laptopBL.GetById(id);
                        ViewLaptopDetails(laptop);
                        Utility.PressAnyKey("back");
                        ShowListLaptop(laptops, page, pageCount);
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.C:
                        orderH.CreateOrder(staff);
                        ShowListLaptop(laptops, page, pageCount);
                        ShowFeature(staff);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (page > 1)
                        {
                            page--;
                            offset -= 10;
                            laptops = laptopBL.Search(searchValue, offset);
                            ShowListLaptop(laptops, page, pageCount);
                            ShowFeature(staff);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (page < pageCount)
                        {
                            page++;
                            offset += 10;
                            laptops = laptopBL.Search(searchValue, offset);
                            ShowListLaptop(laptops, page, pageCount);
                            ShowFeature(staff);
                        }
                        break;
                }
            } while (key != ConsoleKey.Escape);
            Console.CursorVisible = true;
        }
        public void ShowListLaptop(List<Laptop> laptops, int page, int pageCount)
        {
            Console.Clear();
            Utility.PrintTitle("▬▬▬▬ SEARCH LAPTOP ▬▬▬▬", true);
            Console.CursorVisible = false;
            if (laptops == null || laptops.Count == 0)
            {
                Console.WriteLine("\n  Laptop not found!");
            }
            else
            {
                int lengthLine = 116;
                int[] lengthDatas = { 3, 30, 11, 11, 21, 6, 12 };
                Console.WriteLine(Utility.GetLine(lengthDatas, "  ┌", "─", "┬", "┐"));
                Console.WriteLine("  │ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12} │", "ID", "Laptop Name",
                                   "Manufactory", "Category", "CPU", "RAM", "Price(VNĐ)");
                Console.WriteLine(Utility.GetLine(lengthDatas, "  ├", "─", "┼", "┤"));
                for (int i = 0; i < laptops.Count; i++)
                {
                    int lengthName = 27;
                    string name = (laptops[i].LaptopName.Length > lengthName) ?
                    laptops[i].LaptopName.Remove(lengthName, laptops[i].LaptopName.Length - lengthName) + "..." : laptops[i].LaptopName;
                    int index = laptops[i].Ram.IndexOf('B') + 1;
                    string ram = laptops[i].Ram.Substring(0, index);
                    Console.WriteLine("  │ {0,3} │ {1,-30} │ {2,-11} │ {3,-11} │ {4,-21} │ {5,6} │ {6,12:N0} │", laptops[i].LaptopId,
                                    name, laptops[i].ManufactoryInfo.ManufactoryName, laptops[i].CategoryInfo.CategoryName,
                                    laptops[i].CPU, ram, laptops[i].Price);
                }
                Console.WriteLine(Utility.GetLine(lengthDatas, "  └", "─", "┴", "┘"));
                if (pageCount > 1)
                {
                    string nextPage = (page > 0 && page < pageCount) ? "►" : " ";
                    string prePage = (page > 1) ? "◄" : " ";
                    string pages = prePage + $"      [{page}/{pageCount}]      " + nextPage;
                    int position = lengthLine / 2 + pages.Length / 2 + 1;
                    Console.WriteLine(String.Format("{0," + position + "}", pages));
                }
            }
        }
        private void ViewLaptopDetails(Laptop laptop)
        {
            Console.CursorVisible = false;
            if (laptop == null)
            {
                Console.WriteLine("  Laptop not found!");
                Utility.PressAnyKey("back");
                return;
            }
            Console.Clear();
            string data;
            string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "LAPTOP INFORMATION";
            int lengthLine = line.Length + 2;
            int posLeft = Utility.GetPosition(title, lengthLine);
            Console.WriteLine("  ┌{0}┐", line);
            Console.Write("  │{0," + (posLeft - 1) + "}", ""); Utility.PrintColor(title, ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("{0," + (lengthLine - title.Length - posLeft) + "}", "│");
            Console.WriteLine("  ├{0}┤", line);
            Console.WriteLine("  │ Laptop Id:   {0," + -(lengthLine - 16) + "}│", laptop.LaptopId);
            Console.WriteLine("  │ Laptop name: {0," + -(lengthLine - 16) + "}│", laptop.LaptopName);
            Console.WriteLine("  │ Manufactory: {0," + -(lengthLine - 16) + "}│", laptop.ManufactoryInfo.ManufactoryName);
            Console.WriteLine("  │ Category:    {0," + -(lengthLine - 16) + "}│", laptop.CategoryInfo.CategoryName);
            Console.WriteLine("  │ CPU:         {0," + -(lengthLine - 16) + "}│", laptop.CPU);
            Console.WriteLine("  │ RAM:         {0," + -(lengthLine - 16) + "}│", laptop.Ram);
            Console.WriteLine("  │ Hard drive:  {0," + -(lengthLine - 16) + "}│", laptop.HardDrive);
            Console.WriteLine("  │ VGA:         {0," + -(lengthLine - 16) + "}│", laptop.VGA);
            data = laptop.Display;
            if (data.Length > 99)
            {
                var lines = Utility.LineFormat(data, 99);
                Console.WriteLine("  │ Display:     {0," + -(lengthLine - 16) + "}│", lines[0]);
                for (int i = 1; i < lines.Count; i++)
                    Console.WriteLine("  │              {0," + -(lengthLine - 16) + "}│", lines[i]);
            }
            else
                Console.WriteLine("  │ Display:     {0," + -(lengthLine - 16) + "}│", data);
            Console.WriteLine("  │ Battery:     {0," + -(lengthLine - 16) + "}│", laptop.Battery);
            Console.WriteLine("  │ Weight:      {0," + -(lengthLine - 16) + "}│", laptop.Weight);
            Console.WriteLine("  │ Materials:   {0," + -(lengthLine - 16) + "}│", laptop.Materials);
            data = laptop.Ports;
            if (data.Length > 99)
            {
                var lines = Utility.LineFormat(data, 99);
                Console.WriteLine("  │ Ports:       {0," + -(lengthLine - 16) + "}│", lines[0]);
                for (int i = 1; i < lines.Count; i++)
                    Console.WriteLine("  │              {0," + -(lengthLine - 16) + "}│", lines[i]);
            }
            else
                Console.WriteLine("  │ Ports:       {0," + -(lengthLine - 16) + "}│", data);
            Console.WriteLine("  │ Network and connection: {0," + -(lengthLine - 27) + "}│", laptop.NetworkAndConnection);
            Console.WriteLine("  │ Security:    {0," + -(lengthLine - 16) + "}│", laptop.Security);
            Console.WriteLine("  │ Keyboard:    {0," + -(lengthLine - 16) + "}│", laptop.Keyboard);
            Console.WriteLine("  │ Audio:       {0," + -(lengthLine - 16) + "}│", laptop.Audio);
            Console.WriteLine("  │ Size:        {0," + -(lengthLine - 16) + "}│", laptop.Size);
            Console.WriteLine("  │ Operating system: {0," + -(lengthLine - 21) + "}│", laptop.OS);
            Console.WriteLine("  │ Quantity:    {0," + -(lengthLine - 16) + "}│", laptop.Quantity);
            Console.WriteLine("  │ Price:       {0," + -(lengthLine - 16) + "}│", laptop.Price.ToString("N0"));
            Console.WriteLine("  │ Warranty period: {0," + -(lengthLine - 20) + "}│", laptop.WarrantyPeriod);
            Console.WriteLine("  └{0}┘", line);
        }
        public void ShowFeature(Staff staff)
        {
            if (staff.Role == Staff.SELLER)
            {
                Console.Write("\n  ● Press '");
                Utility.PrintColor("F", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.Write("' to search laptops, '");
                Utility.PrintColor("D", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.Write("' to view laptop details, '");
                Utility.PrintColor("C", ConsoleColor.Yellow, ConsoleColor.Black);
                Console.Write("' to Create Order, '");
                Utility.PrintColor("ESC", ConsoleColor.Red, ConsoleColor.Black);
                Console.WriteLine("' to exit");
            }
        }
    }
}