using System;
using System.Text;

namespace ConsolePL
{
    public class Menu
    {
        private string title;
        private string[] menuItems;

        public Menu(string title, string[] menuItems)
        {
            this.title = title;
            this.menuItems = menuItems;

        }
        public int Run()
        {
            int choose = 0;
            string input;
            Console.Clear();
            Utility.Write("╔════════════════════════════════════════════════════════╗\n", ConsoleColor.Green);
            Utility.Write("║                                                        ║\n║", ConsoleColor.Green);
            Utility.Write("     ╦   ╔═╗ ╔═╗ ╔╦╗ ╔═╗ ╔═╗    ╔═╗ ╔╦╗ ╔═╗ ╦═╗ ╔═╗     ", ConsoleColor.Cyan);
            Utility.Write("║\n║", ConsoleColor.Green);
            Utility.Write("     ║   ╠═╣ ╠═╝  ║  ║ ║ ╠═╝    ╚═╗  ║  ║ ║ ╠╦╝ ╠╣      ", ConsoleColor.Cyan);
            Utility.Write("║\n║", ConsoleColor.Green);
            Utility.Write("     ╩═╝ ╩ ╩ ╩    ╩  ╚═╝ ╩      ╚═╝  ╩  ╚═╝ ╝╚═ ╚═╝     ", ConsoleColor.Cyan);
            Utility.Write("║\n", ConsoleColor.Green);
            Utility.Write("║                                                        ║\n║", ConsoleColor.Green);
            int x = Utility.GetPosition(title, 58);
            Utility.Write(String.Format("{0," + x + "}{1}", "", title), ConsoleColor.Yellow);
            Console.Write(String.Format("{0," + (56 - x - title.Length) + "}", ""));
            Utility.Write("║\n", ConsoleColor.Green);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Utility.Write("╟────────────────────────────────────────────────────────╢\n", ConsoleColor.Green);
                Utility.Write("║", ConsoleColor.Green);
                x = Utility.GetPosition(menuItems[i] + (i + 1) + ". ", 58);
                Console.Write(String.Format("{0," + x + "}{1}. {2}", "", i + 1, menuItems[i]));
                Console.Write(String.Format("{0," + (53 - x - menuItems[i].Length) + "}", ""));
                Utility.Write("║\n", ConsoleColor.Green);
            }
            Utility.Write("╚════════════════════════════════════════════════════════╝\n", ConsoleColor.Green);
            Console.Write("\n  → Your choice: ");
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out choose) && choose >= 1 && choose <= menuItems.Length) return choose;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Entered incorrectly!");
                Console.ResetColor();
                Console.Write("  → re-enter: ");
            }
        }
    }
}