using System;
using Persistance;
using BL;
using System.Collections.Generic;


// dotnet publish --configuration Release -o /Users/sinhnx/Desktop/LoginDemo
namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Staff staff = Login.Run();
            short choose;
            string title;
            string[] menu;
            switch (staff.Role)
            {
                case Staff.SELLER:
                    title = "MENU SELLER";
                    menu = new[] { "SEARCH LAPTOPS", "EXIT" };
                    LaptopHandle laptopH = new LaptopHandle();
                    do
                    {
                        choose = Menu.Display(title, menu);
                        switch (choose)
                        {
                            case 1:
                                laptopH.SearchLaptops(staff);
                                break;
                        }
                    } while (choose != menu.Length);
                    break;
                case Staff.ACCOUNTANCE:
                    Console.WriteLine("accountance");

                    break;
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
    }
}
