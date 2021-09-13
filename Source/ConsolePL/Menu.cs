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
        private int positionY;
        private string title;
        private string[] menu;
        private int width = 119;
        private int height = 29;
        public Menu(string title, string[] menu)
        {
            this.title = title;
            this.menu = menu;
            int padding = (height - storeName.Length - 5 - menu.Length * 2) / 2;
            this.positionY = padding + storeName.Length + 4;
        }
        public int Run()
        {
            ShowForm();
            return Handle();
        }
        public void ShowForm()
        {
            Console.Clear();
            Utility.PrintBorder(width, height);
            int positionX = Utility.GetPosition(storeName[0], width) - 1;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < storeName.Length; i++)
            {
                Console.SetCursorPosition(1, i + 2);
                Console.Write("{0," + positionX + "}", storeName[i]);
            }
            Console.ResetColor();
            Console.SetCursorPosition(1, positionY - 3);
            positionX = Utility.GetPosition(title, width);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0," + positionX + "}", title);
            Console.ResetColor();
            for (int i = 0; i < menu.Length; i++)
            {
                positionX = Utility.GetPosition(menu[i], width);
                Console.SetCursorPosition(1, positionY + i * 2);
                Console.Write("{0," + positionX + "}", menu[i]);
            }
        }
        public int Handle()
        {
            Console.CursorVisible = false;
            int choose = 0;
            ConsoleKey keyPressed;
            int posLeft;
            posLeft = Utility.GetPosition(menu[choose], width) - menu[choose].Length;
            Console.SetCursorPosition(1, choose * 2 + positionY);
            Utility.PrintColor(menu[choose], posLeft, ConsoleColor.Black, ConsoleColor.White);
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(1, Console.CursorTop);
                    posLeft = Utility.GetPosition(menu[choose], width) - menu[choose].Length;
                    Utility.PrintColor(menu[choose], posLeft, ConsoleColor.White, ConsoleColor.Black);
                    if (keyPressed == ConsoleKey.UpArrow)
                    {
                        choose--;
                        if (choose == -1)
                        {
                            choose = menu.Length - 1;
                        }
                    }
                    else
                    {
                        choose++;
                        if (choose > menu.Length- 1)
                        {
                            choose = 0;
                        }
                    }
                    posLeft = Utility.GetPosition(menu[choose], width) - menu[choose].Length;
                    Console.SetCursorPosition(1, choose * 2 + positionY);
                    Utility.PrintColor(menu[choose], posLeft, ConsoleColor.Black, ConsoleColor.White);
                }
            } while (keyPressed != ConsoleKey.Enter);
            return choose + 1;
        }
    }
}

