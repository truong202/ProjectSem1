using System;
using Persistance;
using MySql.Data.MySqlClient;
using System.Text;

namespace DAL
{
    public class StaffDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(Staff staff)
        {
            Staff _staff = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand($"call sp_login(@username, @password)", connection);
                    command.Parameters.AddWithValue("@username", staff.Username);
                    // command.Parameters["@username"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@password", CreateMD5(staff.Password));
                    // command.Parameters["@password"].Direction = System.Data.ParameterDirection.Input;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _staff = new Staff();
                        _staff = GetStaff(reader);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch
                {
                    throw new Exception("Không thể kết nối đến database!");
                }
            }
            return _staff;
        }
        private Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.StaffId = reader.GetInt32("staff_id");
            staff.StaffName = reader.GetString("staff_name");
            staff.Username = reader.GetString("username");
            staff.Password = reader.GetString("password");
            staff.Role = reader.GetInt32("role");
            return staff;
        }
        private string CreateMD5(string input)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    builder.Append(b.ToString("x2").ToLower());
                return builder.ToString();
            }
        }
    }
}