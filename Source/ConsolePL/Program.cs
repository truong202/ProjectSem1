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
            Console.Title = "LAPTOP STORE";
            Staff staff = Login.Run();
            int choose;
            string title;
            string[] menu;
            switch (staff.Role)
            {
                case Staff.SELLER:
                    title = "[MENU SELLER]";
                    // menu = new[] { "SEARCH LAPTOPS", "EXIT" };
                    menu = new[] { "Search Laptops", "Exit" };
                    LaptopHandle laptopH = new LaptopHandle();
                    Menu sellerMenu = new Menu(title, menu);
                    do
                    {
                        // choose = Menu.Display(title, menu);
                        choose = sellerMenu.Run();
                        switch (choose)
                        {
                            case 1:
                                laptopH.SearchLaptops(staff);
                                break;
                        }
                    } while (choose != menu.Length);
                    break;
                case Staff.ACCOUNTANCE:
                    title = "ACCOUNTANCE";
                    menu = new[] { "PAYMENT", "EXIT" };
                    OrderHandle orderH = new OrderHandle();
                    Menu accountMenu = new Menu(title, menu);
                    do{
                        choose = accountMenu.Run();
                        switch (choose)
                        {
                        case 1:
                            orderH.Payment(staff);
                            break;
                        }

                } while (choose != menu.Length);
                    break;
            }
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
