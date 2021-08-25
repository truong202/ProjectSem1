using System;
using Persistance;
using MySql.Data.MySqlClient;

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
                    command.Parameters["@username"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@password", Md5Algorithms.CreateMD5(staff.Password));
                    command.Parameters["@password"].Direction = System.Data.ParameterDirection.Input;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _staff = new Staff();
                        _staff = GetStaff(reader);
                    }
                    reader.Close();
                    connection.Close();
                }
                // catch (Exception e)
                // {
                //     Console.WriteLine(e.Message);
                // }
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
    }
}