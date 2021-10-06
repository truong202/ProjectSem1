using System;
using Persistance;
using BL;

namespace ConsolePL
{
    public class Login
    {
        private static int width = 119;
        private static int height = 29;
        private const int POSITION_TOP = 12;
        public static Staff Run()
        {
            Console.Clear();
            string[] title =
            {"██╗      █████╗ ██████╗ ████████╗ ██████╗ ██████╗     ███████╗████████╗ ██████╗ ██████╗ ███████╗",
             "██║     ██╔══██╗██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗    ██╔════╝╚══██╔══╝██╔═══██╗██╔══██╗██╔════╝",
             "██║     ███████║██████╔╝   ██║   ██║   ██║██████╔╝    ███████╗   ██║   ██║   ██║██████╔╝█████╗  ",
             "██║     ██╔══██║██╔═══╝    ██║   ██║   ██║██╔═══╝     ╚════██║   ██║   ██║   ██║██╔══██╗██╔══╝  ",
             "███████╗██║  ██║██║        ██║   ╚██████╔╝██║         ███████║   ██║   ╚██████╔╝██║  ██║███████╗",
             "╚══════╝╚═╝  ╚═╝╚═╝        ╚═╝    ╚═════╝ ╚═╝         ╚══════╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝"};
            Utility.PrintBorder(width, height);
            int posLeft = Utility.GetPosition(title[0], width);
            for (int i = 0; i < title.Length; i++)
            {
                Utility.Write(title[i], ConsoleColor.DarkCyan, ConsoleColor.Black, posLeft, i + 3);
            }
            Utility.PrintBox(40, 40, POSITION_TOP);
            Utility.Write("Username:\n\n\n\n", ConsoleColor.Yellow, 26, Console.CursorTop - 1);
            Utility.PrintBox(40, 40, Console.CursorTop);
            Utility.Write("Password:\n\n\n\n", ConsoleColor.Yellow, 26, Console.CursorTop - 1);
            Utility.Write("███████████████\n", ConsoleColor.Red, 53, Console.CursorTop);
            Utility.Write("     LOGIN     \n", ConsoleColor.White, ConsoleColor.Red, 53, Console.CursorTop);
            Utility.Write("███████████████", ConsoleColor.Red, 53, Console.CursorTop);
            return Handle();
        }

        private static Staff Handle()
        {
            string password = string.Empty, username = string.Empty, pass = string.Empty;
            ConsoleKey key = new ConsoleKey();
            Staff staff;
            int choose = 1;
            Console.SetCursorPosition(42, POSITION_TOP + 1);
            while (true)
            {
                Console.CursorVisible = true;
                var keyInfo = Console.ReadKey(true);
                Console.CursorVisible = false;
                key = keyInfo.Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (choose == 1)
                        {
                            if (IsValidUsername(username))
                            {
                                Utility.Write(pass, 42, POSITION_TOP + 6);
                                choose = 2;
                            }
                        }
                        else if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                                if (staff == null)
                                {
                                    Utility.Write("Incorrect Username or Password!", ConsoleColor.Red, 40, POSITION_TOP + 8);
                                    Utility.Write(pass, 42, POSITION_TOP + 6);
                                }
                                else
                                {
                                    return staff;
                                }
                            }
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (choose == 1)
                        {
                            if (IsValidUsername(username))
                            {
                                Utility.Write(pass, 42, POSITION_TOP + 6);
                                choose = 2;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Utility.Write(username, 42, POSITION_TOP + 1);
                                choose = 1;
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Backspace:
                        if (choose == 1 && username != "")
                        {
                            Console.Write("\b \b");
                            username = username[..^1];
                        }
                        else if (choose == 2 && password != "")
                        {
                            Console.Write("\b \b");
                            password = password[..^1];
                            pass = pass[..^1];
                        }
                        break;
                    default:
                        if (!char.IsControl(keyInfo.KeyChar))
                        {
                            if (choose == 1)
                            {
                                username += keyInfo.KeyChar;
                                Console.Write(keyInfo.KeyChar);
                            }
                            else if (choose == 2)
                            {
                                password += keyInfo.KeyChar;
                                pass += "☻";
                                Console.Write("☻");
                            }
                        }
                        break;
                }
            }
        }
        private static bool IsValidUsername(string username)
        {
            try
            {
                Utility.Write(String.Format("{0,116}", ""), 1, POSITION_TOP + 3);
                Staff.CheckUsername(username);
                return true;
            }
            catch (Exception e)
            {
                Utility.Write(e.Message, ConsoleColor.Red, 40, POSITION_TOP + 3);
                Utility.Write(username, 42, POSITION_TOP + 1);
                return false;
            }
        }
        private static bool IsValidPassword(string password)
        {
            try
            {
                Utility.Write(String.Format("{0,116}", ""), 1, POSITION_TOP + 8);
                Staff.CheckPassword(password);
                return true;
            }
            catch (Exception e)
            {
                Utility.Write(e.Message, ConsoleColor.Red, 40, POSITION_TOP + 8);
                Console.SetCursorPosition(42, POSITION_TOP + 6);
                for (int i = 0; i < password.Length; i++) Console.Write("*");
                return false;
            }
        }
    }
}
