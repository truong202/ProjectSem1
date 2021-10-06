using System;
using Persistance;
using BL;
using System.Collections.Generic;

// dotnet publish --configuration Release -o D:\APP
namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            // Console.TreatControlCAsInput = true;
            Console.Title = "LAPTOP_STORE";
            Staff staff = Login.Run();
            // Staff staff = new Staff() { Role = Staff.SELLER, Id = 1 };
            int choose;
            string title;
            string[] menuItems;
            switch (staff.Role)
            {
                case Staff.SELLER:
                    title = "[MENU SELLER]";
                    menuItems = new[] { "Search Laptops", "Exit" };
                    Menu sellerMenu = new Menu(title, menuItems);
                    LaptopHandle laptopH = new LaptopHandle();
                    do
                    {
                        choose = sellerMenu.Run();
                        switch (choose)
                        {
                            case 1:
                                laptopH.SearchLaptops(staff);
                                break;
                        }
                    } while (choose != menuItems.Length);
                    break;
                case Staff.ACCOUNTANCE:
                    title = "[MENU ACCOUNTANCE]";
                    menuItems = new[] { "Payment", "Exit" };
                    Menu accountanceMenu = new Menu(title, menuItems);
                    OrderHandle orderH = new OrderHandle();
                    do
                    {
                        choose = accountanceMenu.Run();
                        switch (choose)
                        {
                            case 1:
                                orderH.Payment(staff);
                                break;
                        }
                    } while (choose != menuItems.Length);
                    break;
            }
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
