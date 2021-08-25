using System;
using Persistance;
using BL;
using System.Text;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            // staff.Username = "afdd','afs');drop database laptop_store;";
            Staff staff = Login();
            int role = staff.Role;
            switch (role)
            {
                case StaffRole.SELLER_ROLE:

                    break;
                case StaffRole.ACCOUNTANCE_ROLE:
                    Console.WriteLine("accountance");

                    break;
            }
        }
        static Staff Login()
        {
            string username, password;
            Staff staff;
            do
            {
                Console.Write("Username: ");
                while (true)
                {
                    username = Console.ReadLine();
                    try
                    {
                        Staff.CheckUsername(username);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        Console.Write("Re-enter Username: ");
                    }
                }
                Console.Write("Password: ");
                while (true)
                {
                    password = GetPassword();
                    Console.WriteLine();
                    try
                    {
                        Staff.CheckPassword(password);
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                        Console.Write("Re-enter Password: ");
                    }
                }
                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                if (staff == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect UserName or Password!");
                    Console.ResetColor();
                }
            } while (staff == null);
            return staff;
        }
        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}
