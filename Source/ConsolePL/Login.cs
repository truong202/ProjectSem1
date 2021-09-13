using System;
using Persistance;
using BL;

namespace ConsolePL
{
    public class Login
    {
        private static int width = 119;
        private static int height = 29;
        private const int POSITION = 12;
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
            int posTitle = Utility.GetPosition(title[0], width);
            for (int i = 0; i < title.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Console.Write("{0," + posTitle + "}", title[i]);
            }
            Console.ResetColor();
            Console.SetCursorPosition(1, POSITION); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, POSITION + 1); Console.Write("{0,78}", "Username:     │                                │");
            Console.SetCursorPosition(1, POSITION + 2); Console.Write("{0,78}", "└────────────────────────────────┘");
            Console.SetCursorPosition(1, POSITION + 5); Console.Write("{0,78}", "┌────────────────────────────────┐");
            Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,78}", "Password:     │                                │");
            Console.SetCursorPosition(1, POSITION + 7); Console.Write("{0,78}", "└────────────────────────────────┘");
            WriteLogin(ConsoleColor.White, ConsoleColor.DarkCyan);
        }
        private static Staff Handle()
        {
            string password = string.Empty, username = string.Empty, pass = string.Empty;
            ConsoleKey key = new ConsoleKey();
            Staff staff;
            int choose = 1;
            Console.SetCursorPosition(1, POSITION + 1); Console.Write("{0,46}", "Username:     │ ");
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
                                Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
                                choose = 2;
                            }
                        }
                        else if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                WriteLogin(ConsoleColor.White, ConsoleColor.Red);

                                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                                if (staff == null)
                                {
                                    WriteLogin(ConsoleColor.White, ConsoleColor.DarkCyan);
                                    ShowMessage("Incorrect Username or Password!", 44, POSITION + 8);
                                    Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,46}", "Password:     │ ");
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
                                ShowMessage("Incorrect Username or Password!", 44, POSITION + 8);
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
                                Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,46}", "Password:     │ ");
                                Console.Write(pass);
                                choose = 2;
                            }
                        }
                        else if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                WriteLogin(ConsoleColor.White, ConsoleColor.Red);
                                choose = 3;
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (choose == 2)
                        {
                            if (IsValidPassword(password))
                            {
                                Console.SetCursorPosition(1, POSITION + 1); Console.Write("{0,46}", "Username:     │ ");
                                Console.Write(username);
                                choose = 1;
                            }
                        }
                        else if (choose == 3)
                        {
                            WriteLogin(ConsoleColor.White, ConsoleColor.DarkCyan);
                            Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,46}", "Password:     │ ");
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
                Console.SetCursorPosition(1, POSITION + 3);
                Console.Write("{0,38}{1}", "", "                                                          ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message, 38, POSITION + 3);
                Console.SetCursorPosition(1, POSITION + 1); Console.Write("{0,46}", "Username:     │ ");
                Console.Write(username);
                return false;
            }
        }
        private static void WriteLogin(ConsoleColor fColor, ConsoleColor bColor)
        {
            Console.SetCursorPosition(1, POSITION + 10); Console.Write("{0, 55}", "");
            Utility.PrintColor("███████████████", bColor, bColor);
            Console.SetCursorPosition(1, POSITION + 11); Console.Write("{0,55}", "");
            Utility.PrintColor("     LOGIN     ", fColor, bColor);
            Console.SetCursorPosition(1, POSITION + 12); Console.Write("{0,55}", "");
            Utility.PrintColor("███████████████", bColor, bColor);
        }
        private static void ShowMessage(string message, int posLef, int posTop)
        {
            Console.SetCursorPosition(1, posTop);
            Console.Write("{0,38}{1}", "", "                                                               ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(1, posTop);
            Console.Write("{0," + posLef + "}{1}", "", message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static bool IsValidPassword(string password)
        {
            try
            {
                Staff.CheckPassword(password);
                Console.SetCursorPosition(1, POSITION + 8);
                Console.Write("{0,38}{1}", "", "                                                               ");
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message, 38, POSITION + 8);
                Console.SetCursorPosition(1, POSITION + 6); Console.Write("{0,46}", "Password:     │ ");
                for (int i = 0; i < password.Length; i++) Console.Write("*");
                return false;
            }
        }
    }
}
