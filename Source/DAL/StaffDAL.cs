using MySql.Data.MySqlClient;
using Persistance;
using System.Text;

namespace DAL {
    public class StaffDAL {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Staff Login(Staff staff) {
            Staff _staff = null;
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand($"call sp_login(@username, @password)", connection);
                command.Parameters.AddWithValue("@username", staff.Username);
                command.Parameters.AddWithValue("@password", CreateMD5(staff.Password));
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        _staff = new Staff();
                        _staff = GetStaff(reader);
                    }
                }
            }
            catch { }
            finally {
                try { connection.Close(); } catch { }
            }
            return _staff;
        }
        internal Staff GetStaff(MySqlDataReader reader) {
            Staff staff = new Staff();
            staff.ID = reader.GetInt32("staff_id");
            staff.Name = reader.GetString("staff_name");
            staff.Username = reader.GetString("username");
            staff.Password = reader.GetString("password");
            staff.Role = reader.GetInt32("role");
            return staff;
        }
        private string CreateMD5(string input) {
            using (var provider = System.Security.Cryptography.MD5.Create()) {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    builder.Append(b.ToString("x2").ToLower());
                return builder.ToString();
            }
        }
    }
}