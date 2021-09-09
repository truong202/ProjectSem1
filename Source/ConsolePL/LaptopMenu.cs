using System;
using System.Collections.Generic;
using Persistance;
using BL;

namespace ConsolePL
{
    public class LaptopMenu
    {
        private LaptopBL laptopBL = new LaptopBL();
        private OrderMenu orderM = new OrderMenu();
        public void SearchLaptops(Staff staff)
        {
            int offset = 0;
            ConsoleKey key = new ConsoleKey();
            string searchValue = "";
            int laptopCount = laptopBL.GetCount(searchValue);
            if (laptopCount == 0)
            {
                Console.WriteLine(" Laptop not found!");
                Console.Write(" Press any key to back..."); Console.ReadKey();
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
                        offset = 0; page = 1;
                        Console.WriteLine();
                        Console.Write(" → Input search value: ");
                        Console.CursorVisible = true;
                        searchValue = Console.ReadLine().Trim();
                        laptopCount = laptopBL.GetCount(searchValue);
                        pageCount = (laptopCount % 10 == 0) ? laptopCount / 10 : laptopCount / 10 + 1;
                        laptops = laptopBL.Search(searchValue, offset);
                        Display(laptops, page, pageCount);
                        break;
                    case ConsoleKey.C:
                        // if (laptops != null && laptops.Count > 0)
                        // {
                        orderM.CreateOrder(staff);
                        Display(laptops, page, pageCount);
                        // }
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine();
                        Console.Write(" → Input ID(input 0 to cannel): ");
                        int id = Utility.GetNumber();
                        if (id != 0)
                        {
                            Laptop laptop = new LaptopBL().GetById(id);
                            ViewLaptopDetails(laptop);
                        }
                        Display(laptops, page, pageCount);
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
        public void Display(List<Laptop> laptops, int page, int pageCount)
        {
            Console.Clear();
            Console.CursorVisible = false;
            if (laptops == null || laptops.Count == 0)
            {
                Console.WriteLine(" Laptop not found!");
            }
            else
            {
                List<string[]> lines = new List<string[]>();
                lines.Add(new[] { "ID", "Laptop Name", "Manufactory", "Category", "CPU", "RAM", "Price(VNĐ)" });
                foreach (var laptop in laptops)
                {
                    int lengthName = 30;
                    string id = laptop.LaptopId.ToString();
                    string name = (laptop.LaptopName.Length > lengthName) ?
                    laptop.LaptopName.Remove(lengthName, laptop.LaptopName.Length - lengthName) + "..." : laptop.LaptopName;
                    int index = laptop.Ram.IndexOf('B') + 1;
                    string ram = laptop.Ram.Substring(0, index);
                    string price = laptop.Price.ToString("N0");
                    lines.Add(new[] { id, name, laptop.ManufactoryInfo.ManufactoryName, laptop.CategoryInfo.CategoryName, laptop.CPU, ram, price });
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
            Console.WriteLine("\n ● Press 'F' to search laptops");
            Console.WriteLine(" ● Press 'D' to view laptop details");
            Console.WriteLine(" ● Press 'C' to Create Order");
            Console.WriteLine(" ● Press 'ESC' to exit");
        }
        private void ViewLaptopDetails(Laptop laptop)
        {
            Console.CursorVisible = false;
            if (laptop == null)
            {
                Console.WriteLine(" Laptop not found!");
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
                return;
            }
            Console.Clear();
            string data;
            string line = "──────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "Laptop infomation";
            int lengthLine = line.Length + 2;
            int position = lengthLine / 2 + title.Length / 2 - 1;
            Console.WriteLine(" ┌{0}┐", line);
            Console.WriteLine(" │{0," + position + "}{1," + (lengthLine - position - 1) + "}", title, "│");
            Console.WriteLine(" ├{0}┤", line);
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
                Console.WriteLine(" │ Display:     {0}{1," + (lengthLine - 15 - data.Length) + "}", data, "│");
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
                Console.WriteLine(" │ Ports:       {0}{1," + (lengthLine - 15 - data.Length) + "}", data, "│");
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
            Console.Write(" → Input quantity to Add laptop to order or input 0 to back: ");
            laptop.Quantity = Utility.GetNumber();
            Console.CursorVisible = false;
            if (laptop.Quantity == 0) return;
            bool result = orderM.AddLaptopToOrder(laptop);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine((result ? " Add laptop to order completed!" : " The number of laptop in the store is not enough!"));
            Console.ResetColor();
            Console.Write(" Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}