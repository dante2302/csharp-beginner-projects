using Microsoft.Data.Sqlite;

namespace Database 
{
    static class DataAcessManager
    {
        static readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        private static void ExecuteNonQueryCommand(string command)
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = command;
                var a = tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Initialize()
        {
            ExecuteNonQueryCommand(
                @"CREATE TABLE IF NOT EXISTS coding(
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    date TEXT,
                    duration TEXT
                );"
            );
        }

        public static void getDateInput()
        {

        }
        public static void getTimeInput()
        {

        }
    } 
}
