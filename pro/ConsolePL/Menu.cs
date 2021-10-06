using System;
namespace ConsolePL
{
    public class Menu
    {
        private string[] storeName =
            {"██╗      █████╗ ██████╗ ████████╗ ██████╗ ██████╗     ███████╗████████╗ ██████╗ ██████╗ ███████╗",
             "██║     ██╔══██╗██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗    ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝",
             "██║     ███████║██████╔╝   ██║   ██║   ██║██████╔╝    ███████╗   ██║   ██║   ██║██████╔╝█████╗  ",
             "██║     ██╔══██║██╔═══╝    ██║   ██║   ██║██╔═══╝     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝  ",
             "███████╗██║  ██║██║        ██║   ╚██████╔╝██║         ███████║   ██║   ╚██████╔╝██║  ██║███████╗",
             "╚══════╝╚═╝  ╚═╝╚═╝        ╚═╝    ╚═════╝ ╚═╝         ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝"};
        private int posTop;
        private string title;
        private string[] menuItems;
        private int choose;
        public Menu(string title, string[] menuItems)
        {
            this.title = title;
            this.menuItems = menuItems;
            int padding = (29 - storeName.Length - 5 - menuItems.Length * 2) / 2;
            this.posTop = padding + storeName.Length + 4;
        }
        public int Run()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Utility.PrintBorder(119, 29);
            int choose;
            bool exit;
            do
            {
                exit = true;
                int posLeft = Utility.GetPosition(storeName[0], 120);
                for (int i = 0; i < storeName.Length; i++)
                {
                    Utility.Write(storeName[i], ConsoleColor.DarkCyan, ConsoleColor.Black, posLeft, 2 + i);
                }
                int length = title.Length + 4;
                posLeft = Utility.GetPosition(title + "    ", 120);
                Utility.PrintBox(length, posLeft, posTop - 4);
                Utility.Write(title, ConsoleColor.Green, ConsoleColor.Black, posLeft + 2, posTop - 3);
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i < menuItems.Length - 1)
                        Utility.Write(String.Format("{0}.    {1}", i + 1, menuItems[i]), 52, i * 2 + posTop);
                    else
                        Utility.Write(String.Format("ESC.  {1}", i + 1, menuItems[i]), 52, i * 2 + posTop);
                }
                choose = Handle();
                if (choose == menuItems.Length)
                {
                    exit = ConfirmExit();
                }
            } while (!exit);
            return choose;
        }
        private int Handle()
        {
            choose = 0;
            int itemCount = menuItems.Length;
            ConsoleKey keyPressed;
            Utility.Write(menuItems[choose], ConsoleColor.Black, ConsoleColor.White, 58, choose * 2 + posTop);
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                switch (keyPressed)
                {
                    case ConsoleKey.DownArrow:
                        Utility.Write(menuItems[choose], ConsoleColor.White, ConsoleColor.Black, 58, choose * 2 + posTop);
                        choose++;
                        if (choose > itemCount - 1) choose = 0;
                        Utility.Write(menuItems[choose], ConsoleColor.Black, ConsoleColor.White, 58, choose * 2 + posTop);
                        break;
                    case ConsoleKey.UpArrow:
                        Utility.Write(menuItems[choose], ConsoleColor.White, ConsoleColor.Black, 58, choose * 2 + posTop);
                        choose--;
                        if (choose == -1) choose = itemCount - 1;
                        Utility.Write(menuItems[choose], ConsoleColor.Black, ConsoleColor.White, 58, choose * 2 + posTop);
                        break;
                    case ConsoleKey.D1:
                        if (itemCount > 1) return 1;
                        break;
                    case ConsoleKey.D2:
                        if (itemCount > 2) return 2;
                        break;
                    case ConsoleKey.D3:
                        if (itemCount > 3) return 3;
                        break;
                    case ConsoleKey.D4:
                        if (itemCount > 4) return 4;
                        break;
                    case ConsoleKey.D5:
                        if (itemCount > 5) return 5;
                        break;
                    case ConsoleKey.D6:
                        if (itemCount > 6) return 6;
                        break;
                    case ConsoleKey.D7:
                        if (itemCount > 7) return 7;
                        break;
                    case ConsoleKey.D8:
                        if (itemCount > 8) return 8;
                        break;
                    case ConsoleKey.D9:
                        if (itemCount > 9) return 9;
                        break;
                    case ConsoleKey.Escape:
                        return itemCount;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return choose + 1;
        }
        private static bool ConfirmExit()
        {
            bool result = true;
            ConsoleColor color = ConsoleColor.Yellow;
            Utility.Clear(1, 8, 117, 21);
            string line = "┌─────────────────────────────────────────────────────────────┐";
            int posLeft = Utility.GetPosition(line, 120);
            Utility.Write("┌─────────────────────────────────────────────────────────────┐\n", color, posLeft, 12);
            for (int i = 0; i < 10; i++)
                Utility.Write("│                                                             │\n", color, posLeft, Console.CursorTop);
            Utility.Write("└─────────────────────────────────────────────────────────────┘\n", color, posLeft, Console.CursorTop);
            string msg = "DO YOU REALLY WANT TO EXIT?";
            posLeft = Utility.GetPosition(msg, 120);
            Utility.Write(msg, ConsoleColor.Yellow, posLeft, 15);
            ShowBottom("   YES   ", ConsoleColor.Red, 40);
            ShowBottom("   NO    ", ConsoleColor.DarkCyan, 70);
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        if (result)
                        {
                            ShowBottom("   YES   ", ConsoleColor.DarkCyan, 40);
                            ShowBottom("   NO    ", ConsoleColor.Red, 70);
                            result = false;
                        }
                        else
                        {
                            ShowBottom("   YES   ", ConsoleColor.Red, 40);
                            ShowBottom("   NO    ", ConsoleColor.DarkCyan, 70);
                            result = true;
                        }
                        break;
                }
            } while (key != ConsoleKey.Enter);
            Utility.Clear(1, 8, 117, 21);
            return result;
        }
        private static void ShowBottom(string msg, ConsoleColor bColor, int x)
        {
            Utility.Write("▄▄▄▄▄▄▄▄▄\n", bColor, x, 18);
            Utility.Write(msg + "\n", ConsoleColor.White, bColor, x, Console.CursorTop);
            Utility.Write("▀▀▀▀▀▀▀▀▀", bColor, x, Console.CursorTop);
        }
    }
}