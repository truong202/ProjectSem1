using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class Staff
    {
        public int? StaffId { set; get; }
        public string StaffName { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public int Role { set; get; }

    }
    public static class StaffRole
    {
        public const int SELLER_ROLE = 1;
        public const int ACCOUNTANCE_ROLE = 2;
    }

}
