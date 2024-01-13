using Microsoft.Data.Sqlite;
using Menu;

namespace Database
{
    static class DataAcessManager
    {
        static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
        private static void ExecuteNonQueryCommand(string command)
        {
            using (var connection = new SqliteConnection(connectionString))
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

        public static void Create()
        {
            string dateInput = InputHandler.GetDateInput("Please enter a date in the following format: [yellow]dd/MM/yyyy[/]");
            string timeInput = InputHandler.GetTimeInput("Please enter a valid time: ");
            ExecuteNonQueryCommand(
                @$"INSERT INTO coding(date, duration) VALUES('{dateInput}', '{timeInput}')"
            );
        }

        public static List<Record> ReadAll()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM coding";
                var reader = tableCmd.ExecuteReader();
                List<Record> tableRecords = [];
                while (reader.Read())
                {
                    tableRecords.Add(
                        new Record
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetString(1),
                            Time = reader.GetString(2)
                        }
                    );
                }
                connection.Close();
                return tableRecords;
            }
        }
    }
    public class Record
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
