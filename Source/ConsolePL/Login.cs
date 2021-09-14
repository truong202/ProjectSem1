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
            LoginFrom();
            return Handle();
        }
        private static void LoginFrom()
        {
            string[] title = {"██       ██████   ██████  ██ ███    ██", 
                              "██      ██    ██ ██       ██ ████   ██",
                              "██      ██    ██ ██   ███ ██ ██ ██  ██",
                              "██      ██    ██ ██    ██ ██ ██  ██ ██",
                              "███████  ██████   ██████  ██ ██   ████"};
            Utility.PrintBorder(width, height);
            int posLeft = Utility.GetPosition(title[0], width);
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Utility.PrintColor(title[i], posLeft, ConsoleColor.Cyan, ConsoleColor.Black);
            }
            Console.SetCursorPosition(1, POSITION_TOP); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, POSITION_TOP + 1); Console.Write("{0,78}", "Username:     │                                │");
            Console.SetCursorPosition(1, POSITION_TOP + 2); Console.Write("{0,78}", "└────────────────────────────────┘");
            Console.SetCursorPosition(1, POSITION_TOP + 5); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, POSITION_TOP + 6); Console.Write("{0,78}", "Password:     │                                │");
            Console.SetCursorPosition(1, POSITION_TOP + 7); Console.Write("{0,78}", "└────────────────────────────────┘");
            Console.SetCursorPosition(1, POSITION_TOP + 10);
            Utility.PrintColor("███████████████", 55, ConsoleColor.Red, ConsoleColor.Black);
            Console.SetCursorPosition(1, POSITION_TOP + 11);
            Utility.PrintColor("     LOGIN     ", 55, ConsoleColor.White, ConsoleColor.Red);
            Console.SetCursorPosition(1, POSITION_TOP + 12);
            Utility.PrintColor("███████████████", 55, ConsoleColor.Red, ConsoleColor.Black);

        }
        private static Staff Handle()
        {
            string password = string.Empty, username = string.Empty, pass = string.Empty;
            ConsoleKey key = new ConsoleKey();
            Staff staff;
            int choose = 1;
            Console.SetCursorPosition(1, POSITION_TOP + 1); Console.Write("{0,46}", "Username:     │ ");
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
                                Console.SetCursorPosition(1, POSITION_TOP + 6); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
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
                                    ShowMessage("Incorrect Username or Password!", 44, POSITION_TOP + 8);
                                    Console.SetCursorPosition(1, POSITION_TOP + 6); Console.Write("{0,46}", "Password:     │ ");
                                    Console.Write(pass);
                                }
                                else
                                {
                                    // Console.CursorVisible = true;
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
                                Console.SetCursorPosition(1, POSITION_TOP + 6); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
                                choose = 2;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Console.SetCursorPosition(1, POSITION_TOP + 1); Console.Write("{0,46}", "Username:     │ ");
                                Console.Write(username);
                                choose = 1;
                            }
                        }
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
                                pass += "*";
                                Console.Write("*");
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
                Staff.CheckUsername(username);
                Console.SetCursorPosition(1, POSITION_TOP + 3);
                Console.Write("{0,38}{1}", "", "                                                          ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message, 38, POSITION_TOP + 3);
                Console.SetCursorPosition(1, POSITION_TOP + 1); Console.Write("{0,46}", "Username:     │ ");
                Console.Write(username);
                return false;
            }
        }
        private static void ShowMessage(string message, int posLef, int posTop)
        {
            Console.SetCursorPosition(1, posTop);
            Console.Write("{0,38}{1}", "", "                                                               ");
            Console.SetCursorPosition(1, posTop);
            Utility.PrintColor(message, posLef, ConsoleColor.Red, ConsoleColor.Black);
        }
        private static bool IsValidPassword(string password)
        {
            try
            {
                Staff.CheckPassword(password);
                Console.SetCursorPosition(1, POSITION_TOP + 8);
                Console.Write("{0,38}{1}", "", "                                                               ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message, 38, POSITION_TOP + 8);
                Console.SetCursorPosition(1, POSITION_TOP + 6); Console.Write("{0,46}", "Password:     │ ");
                for (int i = 0; i < password.Length; i++) Console.Write("*");
                return false;
            }
        }
    }
}
