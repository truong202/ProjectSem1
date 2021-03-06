using BL;
using Persistance;
using System;
using Utilities;
namespace ConsolePL {
    class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "LAPTOP STORE";
            Staff staff = Login();
            int choose;
            string[] menuItems;
            switch (staff.Role) {
                case Staff.SELLER:
                    menuItems = new[] { "Search Laptops", "Exit" };
                    LaptopHandle laptopH = new LaptopHandle();
                    do {
                        choose = Menu("[MENU SELLER]", menuItems);
                        switch (choose) {
                            case 1:
                                laptopH.SearchLaptops(staff);
                                break;
                        }
                    } while (choose != menuItems.Length);
                    break;
                case Staff.ACCOUNTANT:
                    menuItems = new[] { "Payment", "Exit" };
                    OrderHandle orderH = new OrderHandle();
                    do {
                        choose = Menu("[MENU ACCOUNTANT]", menuItems);
                        switch (choose) {
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

        public static int Menu(string title, string[] menuItems) {
            int choose = 0;
            string input;
            Console.Clear();
            Console.WriteLine(" ╔════════════════════════════════════════════════════════╗");
            Console.Write(" ║                                                        ║\n ║");
            ConsoleUtility.Write("     ╦   ╔═╗ ╔═╗ ╔╦╗ ╔═╗ ╔═╗    ╔═╗ ╔╦╗ ╔═╗ ╦═╗ ╔═╗     ", ConsoleColor.Cyan);
            Console.Write("║\n ║");
            ConsoleUtility.Write("     ║   ╠═╣ ╠═╝  ║  ║ ║ ╠═╝    ╚═╗  ║  ║ ║ ╠╦╝ ╠╣      ", ConsoleColor.Cyan);
            Console.Write("║\n ║");
            ConsoleUtility.Write("     ╩═╝ ╩ ╩ ╩    ╩  ╚═╝ ╩      ╚═╝  ╩  ╚═╝ ╝╚═ ╚═╝     ", ConsoleColor.Cyan);
            Console.WriteLine("║");
            Console.Write(" ║                                                        ║\n ║");
            int x = ConsoleUtility.GetPosition(title, 58);
            ConsoleUtility.Write(String.Format("{0," + x + "}{1}", "", title), ConsoleColor.Yellow);
            Console.Write(String.Format("{0," + (56 - x - title.Length) + "}", ""));
            Console.WriteLine("║");
            for (int i = 0; i < menuItems.Length; i++) {
                Console.WriteLine(" ╟────────────────────────────────────────────────────────╢");
                Console.Write(" ║");
                x = ConsoleUtility.GetPosition(menuItems[i] + (i + 1) + ". ", 58);
                Console.Write(String.Format("{0," + x + "}{1}. {2}", "", i + 1, menuItems[i]));
                Console.Write(String.Format("{0," + (53 - x - menuItems[i].Length) + "}", ""));
                Console.WriteLine("║");
            }
            Console.WriteLine(" ╚════════════════════════════════════════════════════════╝");
            Console.Write("\n  → Your choice: ");
            while (true) {
                input = Console.ReadLine();
                if (int.TryParse(input, out choose) && choose >= 1 && choose <= menuItems.Length) return choose;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Entered incorrectly!");
                Console.ResetColor();
                Console.Write("  → re-enter: ");
            }
        }
        public static Staff Login() {
            string username, password;
            StaffBL staffBL = new StaffBL();
            Staff staff;
            do {
                Console.Clear();
                Console.WriteLine(" ╔════════════════════════════════════════════════════════╗");
                Console.Write(" ║                                                        ║\n ║");
                ConsoleUtility.Write("     ╦   ╔═╗ ╔═╗ ╔╦╗ ╔═╗ ╔═╗    ╔═╗ ╔╦╗ ╔═╗ ╦═╗ ╔═╗     ", ConsoleColor.Cyan);
                Console.Write("║\n ║");
                ConsoleUtility.Write("     ║   ╠═╣ ╠═╝  ║  ║ ║ ╠═╝    ╚═╗  ║  ║ ║ ╠╦╝ ╠╣      ", ConsoleColor.Cyan);
                Console.Write("║\n ║");
                ConsoleUtility.Write("     ╩═╝ ╩ ╩ ╩    ╩  ╚═╝ ╩      ╚═╝  ╩  ╚═╝ ╝╚═ ╚═╝     ", ConsoleColor.Cyan);
                Console.WriteLine("║");
                Console.Write(" ║                                                        ║\n ║");
                ConsoleUtility.Write("                         [LOGIN]                        ", ConsoleColor.Yellow);
                Console.WriteLine("║");
                Console.WriteLine(" ╚════════════════════════════════════════════════════════╝");
                Console.Write("\n  → Username: ");
                username = ConsoleUtility.GetUsername();
                Console.Write("  → Password: ");
                password = ConsoleUtility.GetPassword();
                staff = staffBL.Login(new Staff { Username = username, Password = password });
                if (staff == null) {
                    ConsoleUtility.Write("\n  Incorrect Username or Password!\n", ConsoleColor.Red);
                    ConsoleUtility.PressAnyKey("login again");
                }
            } while (staff == null);
            return staff;
        }
    }
}
