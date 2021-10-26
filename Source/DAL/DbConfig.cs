using MySql.Data.MySqlClient;

namespace DAL {
    public class DbConfig {
        private static MySqlConnection connection;

        private DbConfig() { }

        public static MySqlConnection GetConnection() {
            try {
                string conString;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead("DbConfig.txt")) {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream)) {
                        conString = reader.ReadLine();
                    }
                }
                return GetConnection(conString);
            }
            catch {
                return null;
            }
        }
        public static MySqlConnection GetConnection(string conString) {
            if (connection == null) {
                connection = new MySqlConnection();
            }
            connection.ConnectionString = conString;
            return connection;
        }
    }
}