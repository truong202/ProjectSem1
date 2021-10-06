using System;
using System.Collections.Generic;
using Persistance;
using BL;

namespace ConsolePL
{
    public class LaptopHandle
    {
        LaptopBL laptopBL = new LaptopBL();
        OrderHandle orderH = new OrderHandle();
        public void SearchLaptop(Staff staff)
        {
            Console.Clear();
            List<Laptop> laptops = new List<Laptop>();
            ConsoleKey[] validKeys;
            validKeys = staff.Role == Staff.SELLER ? new[] { ConsoleKey.Escape, ConsoleKey.DownArrow, ConsoleKey.Tab } : new[] { ConsoleKey.Escape };
            int feature = 1, lStatus = -1, pStatus = 1, page = 1, pageCount, laptopCount, offset = 0, laptopChoose = 0;
            ConsoleKey key;
            ConsoleKeyInfo keyInfo;
            bool result;
            string searchValue = "";
            string subSearchValue = searchValue.Length <= 60 ? searchValue : "..." + searchValue.Substring(searchValue.Length - 57, 57);
            laptopCount = laptopBL.GetCount(searchValue);
            laptops = laptopBL.Search(searchValue, offset);
            pageCount = (laptopCount % 7 == 0) ? laptopCount / 7 : laptopCount / 7 + 1;
            Utility.PrintBorder(119, 29);
            Utility.PrintNameStore(72, 1);
            ShowFormSearch(staff);
            ShowListLaptop(laptops);
            Utility.ShowPageNumber(page, pageCount, 0, 120);
            do
            {
                if (feature == 1)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(22 + subSearchValue.Length, 6);
                    keyInfo = Utility.GetString(ref searchValue, 60, validKeys);
                    subSearchValue = searchValue.Length <= 60 ? searchValue : "..." + searchValue.Substring(searchValue.Length - 57, 57);
                    Console.CursorVisible = false;
                }
                else
                {
                    keyInfo = Console.ReadKey(true);
                }
                key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        switch (feature)
                        {
                            case 1:
                                offset = 0; page = 1;
                                laptopCount = laptopBL.GetCount(searchValue.Trim());
                                pageCount = (laptopCount % 7 == 0) ? laptopCount / 7 : laptopCount / 7 + 1;
                                laptops = laptopBL.Search(searchValue.Trim(), offset);
                                Utility.Clear(1, 8, 117, 20);
                                ShowListLaptop(laptops);
                                Utility.ShowPageNumber(page, pageCount, 0, 120);
                                break;
                            case 2:
                                if (lStatus == 1)
                                {
                                    Utility.Clear(1, 8, 117, 20);
                                    ViewLaptopDetails(laptopBL.GetById(laptops[laptopChoose].LaptopId));
                                    Utility.Clear(1, 4, 117, 24);
                                    ShowFormSearch(staff);
                                    Console.SetCursorPosition(22, 6);
                                    Console.Write(subSearchValue);
                                    ShowListLaptop(laptops);
                                    Utility.ShowPageNumber(page, pageCount, 0, 120);
                                    Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                    PrintColor(lStatus);
                                }
                                else
                                {
                                    var laptop = laptopBL.GetById(laptops[laptopChoose].LaptopId);
                                    string[] message;
                                    if (laptop.Quantity == 0)
                                    {
                                        result = false;
                                        message = new[] { "LAPTOP IS OUT OF STOCK!", "PLEASE CHOOSE ANOTHER LAPTOP!" };
                                    }
                                    else
                                    {
                                        laptop.Quantity = 1;
                                        result = orderH.AddLaptoptoOrder(laptop);
                                        if (result)
                                        {
                                            message = new[] { "ADD LAPTOP TO ORDER COMPLETED!" };
                                        }
                                        else
                                        {
                                            message = new[] { "THE STORE DOSEN'T HAVE ENOUGH LAPTOPS IN STOCK!" };
                                        }
                                    }
                                    Utility.Clear(1, 8, 117, 20);
                                    Utility.ShowMessage(message, result ? ConsoleColor.Green : ConsoleColor.Red);
                                    Utility.Clear(1, 8, 117, 20);
                                    ShowListLaptop(laptops);
                                    Utility.ShowPageNumber(page, pageCount, 0, 120);
                                    Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                    PrintColor(lStatus);
                                }
                                break;
                            case 3:
                                if (pStatus == 1)
                                {
                                    offset += 7; page++;
                                }
                                else
                                {
                                    offset -= 7; page--;
                                }
                                laptops = laptopBL.Search(searchValue, offset);
                                laptopChoose = laptops.Count - 1;
                                Utility.Clear(1, 8, 117, 20);
                                ShowListLaptop(laptops);
                                pStatus = page == 1 ? 1 : page == pageCount ? -1 : pStatus == 1 ? 1 : -1;
                                Utility.ShowPageNumber(page, pageCount, pStatus, 120);
                                break;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        switch (feature)
                        {
                            case 1:
                                if (laptops != null)
                                {
                                    feature = 2;
                                    laptopChoose = 0;
                                    Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                    PrintColor(lStatus);
                                }
                                break;
                            case 2:
                                if (laptops.Count - 1 != laptopChoose)
                                {
                                    Console.SetCursorPosition(100, Console.CursorTop);
                                    PrintColor(0);
                                    laptopChoose++;
                                    Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                    PrintColor(lStatus);
                                }
                                else
                                {
                                    if (pageCount > 1)
                                    {
                                        Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                        PrintColor(0);
                                        pStatus = page == 1 ? 1 : -1;
                                        Console.SetCursorPosition(100, laptopChoose * 2 + 13);
                                        Utility.ShowPageNumber(page, pageCount, pStatus, 120);
                                        feature = 3;
                                        lStatus = -1;
                                    }
                                }
                                break;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        switch (feature)
                        {
                            case 2:
                                Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                PrintColor(0);
                                if (laptopChoose == 0)
                                {
                                    feature = 1;
                                    lStatus = -1;
                                }
                                else if (laptopChoose <= laptops.Count - 1)
                                {
                                    laptopChoose--;
                                    Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                    PrintColor(lStatus);
                                }
                                break;
                            case 3:
                                Utility.ShowPageNumber(page, pageCount, 0, 120);
                                Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                PrintColor(lStatus);
                                feature = 2;
                                break;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        switch (feature)
                        {
                            case 2:
                                lStatus = -lStatus;
                                Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                                PrintColor(lStatus);
                                break;
                            case 3:
                                pStatus = page == 1 ? 1 : page == pageCount ? -1 : pStatus == 1 ? -1 : 1;
                                Utility.ShowPageNumber(page, pageCount, pStatus, 120);
                                break;
                        }
                        break;
                    case ConsoleKey.Escape:
                        orderH.NewOrder = new Order();
                        break;
                    case ConsoleKey.F:
                        if (feature == 2)
                        {
                            Console.SetCursorPosition(100, laptopChoose * 2 + 11);
                            PrintColor(0);
                        }
                        else if (feature == 3)
                        {
                            Utility.ShowPageNumber(page, pageCount, 0, 120);
                        }
                        feature = 1;
                        break;
                    case ConsoleKey.Tab:
                        orderH.CreateOrder(staff);
                        Utility.Clear(1, 5, 117, 24);
                        feature = 1;
                        ShowFormSearch(staff);
                        Console.SetCursorPosition(22, 6);
                        Console.Write(subSearchValue);
                        ShowListLaptop(laptops);
                        Utility.ShowPageNumber(page, pageCount, 0, 120);
                        break;
                    default:
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }
        public void ShowFormSearch(Staff staff)
        {
            int x = 20, y = 5, width = 64;
            Utility.PrintBox(width, x, y);
            Utility.Write("▄▄▄▄▄▄▄▄▄▄", ConsoleColor.Red, x + width + 1, y);
            Utility.Write("  Search  ", ConsoleColor.White, ConsoleColor.Red, x + width + 1, y + 1);
            Utility.Write("▀▀▀▀▀▀▀▀▀▀", ConsoleColor.Red, x + width + 1, y + 2);
            if (staff.Role == Staff.SELLER)
            {
                Utility.Write("Choose: ", ConsoleColor.Cyan, 2, 28); Console.Write("add laptop to order");
                Utility.Write("TAB", ConsoleColor.Yellow, 91, 28); Console.Write(": view order, ");
                Utility.Write("ESC", ConsoleColor.Red); Console.Write(": back");
            }
        }
        public void ShowListLaptop(List<Laptop> laptops)
        {
            if (laptops == null || laptops.Count == 0)
            {
                int posLeft = Utility.GetPosition("LAPTOP NOT FOUND!!!", 120);
                Utility.Write("LAPTOP NOT FOUND!!!", ConsoleColor.Red, posLeft, 15);
                // ShowMessage(new[] { "LAPTOP NOT FOUND!!!" }, ConsoleColor.Red);
                return;
            }
            int[] lengthDatas = { 3, 27, 11, 21, 5, 12, 6, 7 };
            Utility.Write(Utility.GetLine(lengthDatas, "┌", "─", "┬", "┐\n"), 1, 8);
            Utility.Write(String.Format("│ {0,3} │ {1,-27} │ {2,-11} │ {3,-21} │ {4,5} │ {5,12} │        │         │\n",
                                "ID", "Laptop Name", "Manufactory", "CPU", "RAM", "Price(VNĐ)"), 1, Console.CursorTop);
            for (int i = 0; i < laptops.Count; i++)
            {
                int lengthName = 27;
                string name = (laptops[i].LaptopName.Length > lengthName) ?
                laptops[i].LaptopName.Remove(lengthName - 3, laptops[i].LaptopName.Length - lengthName + 3) + "..." : laptops[i].LaptopName;
                int index = laptops[i].Ram.IndexOf('B') + 1;
                string ram = laptops[i].Ram.Substring(0, index);
                Utility.Write(Utility.GetLine(lengthDatas, "├", "─", "┼", "┤\n"), 1, Console.CursorTop);
                Utility.Write(String.Format("│ {0,3} │ {1,-27} │ {2,-11} │ {3,-21} │ {4,5} │ {5,12:N0} │ ", laptops[i].LaptopId, name,
                laptops[i].ManufactoryInfo.Name, laptops[i].CPU, ram, laptops[i].Price), 1, Console.CursorTop);
                Utility.Write("Choose", ConsoleColor.Cyan, Console.CursorLeft, Console.CursorTop);
                Console.Write(" │ ");
                Utility.Write("Details", ConsoleColor.Cyan, Console.CursorLeft, Console.CursorTop);
                Console.WriteLine(" │");
            }
            Utility.Write(Utility.GetLine(lengthDatas, "└", "─", "┴", "┘\n"), 1, Console.CursorTop);
        }
        public void ViewLaptopDetails(Laptop laptop)
        {
            int posLeft;
            if (laptop == null)
            {
                Utility.ShowMessage(new[] { "LAPTOP NOT FOUND!!!" }, ConsoleColor.Red);
                // posLeft = Utility.GetPosition("LAPTOP NOT FOUND!!!", 120);
                // Utility.Write("LAPTOP NOT FOUND!!!", ConsoleColor.Red, ConsoleColor.Black, posLeft, 15);
                // Utility.Write("Press any key to continue", posLeft - 3, 17); Console.ReadKey(true);
                return;
            }
            string data;
            string line = "───────────────────────────────────────────────────────────────────────────────────────────────────────────────────";
            string title = "LAPTOP INFORMATION";
            int lengthLine = line.Length + 2;
            posLeft = Utility.GetPosition(title, lengthLine);
            Utility.Write($"┌{line}┐\n", 1, 5);
            Utility.Write(String.Format("│{0,116}", "│"), 1, Console.CursorTop);
            Utility.Write("LAPTOP INFORMATION\n", ConsoleColor.Green, posLeft, Console.CursorTop);
            Utility.Write(String.Format("├{0}┤\n", line), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Laptop name: {0,-101}│\n", laptop.LaptopName), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Price      : {0,-101}│\n", laptop.Price.ToString("N0") + " VNĐ"), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Manufactory: {0,-45} Category : {1,-44}│\n", laptop.ManufactoryInfo.Name, laptop.CategoryInfo.Name), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Hard drive : {0,-45} VGA      : {1,-44}│\n", laptop.HardDrive, laptop.VGA), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Battery    : {0,-45} Materials: {1,-44}│\n", laptop.Battery, laptop.Materials), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Security   : {0,-45} Weight   : {1,-44}│\n", laptop.Security, laptop.Weight), 1, Console.CursorTop);
            Utility.Write(String.Format("│ CPU        : {0,-101}│\n", laptop.CPU), 1, Console.CursorTop);
            Utility.Write(String.Format("│ RAM        : {0,-101}│\n", laptop.Ram), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Keyboard   : {0,-101}│\n", laptop.Keyboard), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Audio      : {0,-101}│\n", laptop.Audio), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Size       : {0,-101}│\n", laptop.Size), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Quantity   : {0,-101}│\n", laptop.Quantity), 1, Console.CursorTop);
            data = laptop.Display;

            if (data.Length > 99)
            {
                var lines = Utility.LineFormat(data, 99);
                Utility.Write(String.Format("│ Display    : {0,-101}│\n", lines[0]), 1, Console.CursorTop);
                for (int i = 1; i < lines.Count; i++)
                {

                    Utility.Write(String.Format("│              {0,-101}│\n", lines[i]), 1, Console.CursorTop);
                }
            }
            else
                Utility.Write(String.Format("│ Display    : {0,-101}│\n", data), 1, Console.CursorTop);
            data = laptop.Ports;

            if (data.Length > 99)
            {
                var lines = Utility.LineFormat(data, 99);
                Utility.Write(String.Format("│ Ports      : {0,-101}│\n", lines[0]), 1, Console.CursorTop);
                for (int i = 1; i < lines.Count; i++)
                {
                    Utility.Write(String.Format("│              {0,-101}│\n", lines[i]), 1, Console.CursorTop);
                }
            }
            else
                Utility.Write(String.Format("│ Ports      : {0,-101}│\n", data), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Operating system: {0,-96}│\n", laptop.OS), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Network and connection: {0,-90}│\n", laptop.NetworkAndConnection), 1, Console.CursorTop);
            Utility.Write(String.Format("│ Warranty period: {0,-97}│", laptop.WarrantyPeriod), 1, Console.CursorTop);
            Utility.Write($"└{line}┘\n", 1, Console.CursorTop);
            string msg = "Press any key to back";
            posLeft = Utility.GetPosition(msg, 120);
            Utility.Write($"{msg}", ConsoleColor.Magenta, posLeft, Console.CursorTop);
            Console.ReadKey(true);
        }

        public void PrintColor(int status)
        {
            ConsoleColor textColor = ConsoleColor.Cyan;
            ConsoleColor backgroundDefault = ConsoleColor.Black;
            ConsoleColor background = ConsoleColor.Red;
            switch (status)
            {
                case -1:
                    Utility.Write("Choose", textColor, background);
                    Console.Write(" │ ");
                    Utility.Write("Details", textColor, backgroundDefault);
                    break;
                case 0:
                    Utility.Write("Choose", textColor, backgroundDefault);
                    Console.Write(" │ ");
                    Utility.Write("Details", textColor, backgroundDefault);
                    break;
                case 1:
                    Utility.Write("Choose", textColor, backgroundDefault);
                    Console.Write(" │ ");
                    Utility.Write("Details", textColor, background);
                    break;
            }
        }
    }
}