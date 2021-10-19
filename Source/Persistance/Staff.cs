using System;
using System.Text.RegularExpressions;

namespace Persistance
{
    public class Staff
    {
        public int? ID { set; get; }
        public string Name { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public int Role { set; get; }
        public const int SELLER = 1;
        public const int ACCOUNTANT= 2;
    }
}
