using System;
using Persistance;
using BL;

namespace ConsolePL
{
    public class Login
    {
        private static int width = 119;
        private static int height = 29;
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Console.WriteLine("{0," + Utility.GetPosition(title[0], width) + "}", title[i]);
            }
            Console.ResetColor();
            Console.SetCursorPosition(1, 13); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, 14); Console.Write("{0,78}", "Username:     │                                │");
            Console.SetCursorPosition(1, 15); Console.Write("{0,78}", "└────────────────────────────────┘");
            Console.SetCursorPosition(1, 18); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, 19); Console.Write("{0,78}", "Password:     │                                │");
            Console.SetCursorPosition(1, 20); Console.Write("{0,78}", "└────────────────────────────────┘");
            Utility.PrintColor("     LOGIN     ", 3, 69, 23, ConsoleColor.White, ConsoleColor.DarkCyan);
        }
        private static Staff Handle()
        {
            string password = string.Empty, username = string.Empty, pass = string.Empty;
            ConsoleKey key = new ConsoleKey();
            Staff staff;
            int choose = 1;
            Console.SetCursorPosition(1, 14); Console.Write("{0,46}", "Username:     │ ");
            while (true)
            {
                if (choose == 1 || choose == 2)
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
                                Console.SetCursorPosition(1, 19); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
                                choose = 2;
                            }
                        }
                        else if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Utility.PrintColor("     LOGIN     ", 3, 69, 23, ConsoleColor.White, ConsoleColor.Red);
                                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                                if (staff == null)
                                {
                                    Utility.PrintColor("     LOGIN     ", 3, 69, 23, ConsoleColor.White, ConsoleColor.DarkCyan);
                                    ShowMessageError("Incorrect Username or Password!", 44, 21);
                                    Console.SetCursorPosition(1, 19);
                                    Console.Write("{0,46}", "Password:     │ ");
                                    Console.Write(pass);
                                }
                                else
                                {
                                    Console.CursorVisible = true;
                                    return staff;
                                }
                            }
                        }
                        else
                        {
                            staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                            if (staff == null)
                                ShowMessageError("Incorrect Username or Password!", 44, 21);
                            else
                            {
                                Console.CursorVisible = true;
                                return staff;
                            }   
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (choose == 1)
                        {
                            if (IsValidUsername(username))
                            {
                                Console.SetCursorPosition(1, 19); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
                                choose = 2;
                            }
                        }
                        else if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Utility.PrintColor("     LOGIN     ", 3, 69, 23, ConsoleColor.White, ConsoleColor.Red);
                                choose = 3;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Console.SetCursorPosition(1, 14); Console.Write("{0,46}", "Username:     │ ");
                                Console.Write(username);
                                choose = 1;
                            }
                        }
                        else if (choose == 3)
                        {
                            Utility.PrintColor("     LOGIN     ", 3, 69, 23, ConsoleColor.White, ConsoleColor.DarkCyan);
                            Console.SetCursorPosition(1, 19); Console.Write("{0,46}", "Password:     │ ");
                            Console.Write(pass);
                            choose = 2;
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
                Console.SetCursorPosition(1, 16);
                Console.Write("{0,38}{1}", "", "                                                          ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessageError(e.Message, 38, 16);
                Console.SetCursorPosition(1, 14); Console.Write("{0,46}", "Username:     │ ");
                Console.Write(username);
                return false;
            }
        }
        private static void ShowMessageError(string message, int posLef, int posTop)
        {
            Console.SetCursorPosition(1, posTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0," + posLef + "}{1}", "", message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static bool IsValidPassword(string password)
        {
            try
            {
                Staff.CheckPassword(password);
                Console.SetCursorPosition(1, 21);
                Console.Write("{0,38}{1}", "", "                                                           ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessageError(e.Message, 38, 21);
                Console.SetCursorPosition(1, 19); Console.Write("{0,46}", "Password:     │ ");
                for (int i = 0; i < password.Length; i++) Console.Write("*");
                return false;
            }
        }
    }
}
