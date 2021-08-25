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
            Console.OutputEncoding = Encoding.UTF8; //fjsdakfjaks
            Console.InputEncoding = Encoding.Unicode;
            // staff.Username = "afdd','afs');drop database laptop_store;";
            Staff staff;
            do
            {
                Console.Write("Username: "); string username = Console.ReadLine();
                Console.Write("Password: "); string password = Console.ReadLine();
                staff = new StaffBL().Login(new Staff { Username = username, Password = password });
                if (staff == null)
                {
                    Console.WriteLine("Incorrect UserName or Password!");
                }
            } while (staff == null);
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

    }
}
