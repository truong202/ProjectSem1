using DAL;
using Persistance;

namespace BL {
    public class StaffBL {
        private StaffDAL staffDAL = new StaffDAL();
        public Staff Login(Staff staff) {
            return staffDAL.Login(staff);
        }
    }
}
