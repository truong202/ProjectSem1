using System;
using System.Text.RegularExpressions;

namespace Persistance
{
    public class Staff
    {
        public int? StaffId { set; get; }
        public string StaffName { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public int Role { set; get; }

        public static void CheckUsername(string username)
        {
            if (!Regex.IsMatch(username, @"^([A-Za-z-0-9])+$"))
                throw new Exception("Only letters (a-Z), numbers (0-9) are allowed!");
            else if (username.Length < 6 || username == null || username.Trim() == "")
                throw new Exception("Username must be at least 6 characters long!");
        }
        public static void CheckPassword(string password)
        {
            if (!Regex.IsMatch(password, @"^([A-Za-z0-9])+$"))
                throw new Exception("Only letters (a-Z), numbers (0-9) are allowed!");
            else if (password.Length < 8 || password == null || password.Trim() == "")
                throw new Exception("Password must be at least 8 characters long!");
        }
    }

    public static class StaffRole
    {
        public const int SELLER = 1;
        public const int ACCOUNTANCE = 2;
    }

}
