using System;
using Persistance;
using BL;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.OutputEncoding = System.Text.Encoding.Unicode;
            // Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.TreatControlCAsInput = true;
            Console.Title = "LAPTOP STORE";
            Staff staff = Login.Run();
            // Staff staff = new Staff() { Role = Staff.SELLER, StaffId =  6 };
            int choose;
            string title;
            string[] menuItems;
            switch (staff.Role)
            {
                case Staff.SELLER:
                    title = "MENU SELLER";
                    menuItems = new[] { "CREATE ORDER", "EXIT" };
                    Menu sellerMenu = new Menu(title, menuItems);
                    LaptopHandle laptopH = new LaptopHandle();
                    do
                    {
                        choose = sellerMenu.Run();
                        switch (choose)
                        {
                            case 1:
                                laptopH.SearchLaptop(staff);
                                break;
                        }
                    } while (choose != menuItems.Length);
                    break;
                case Staff.ACCOUNTANCE:
                    title = "MENU ACCOUNTANCE";
                    menuItems = new[] { "PAYMENT", "EXIT" };
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
