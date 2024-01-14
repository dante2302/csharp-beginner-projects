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
                    startTime TEXT,
                    endTime TEXT
                );"
            );
        }

        public static void Create()
        {
            string dateInput = InputHandler.GetDateInput("Please enter a date in the following format: [yellow]dd/MM/yyyy[/]");
            string startTimeInput = InputHandler.GetTimeInput("Please enter a valid time: hh:mm");
            string endTimeInput = InputHandler.GetTimeInput("Enter an end time in the following format: hh:mm");
            ExecuteNonQueryCommand(
                @$"INSERT INTO coding(date, startTime, endTime) VALUES('{dateInput}', '{startTimeInput}', '{endTimeInput}')"
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
                            Date = DateTime.ParseExact(reader.GetString(1), "dd/MM/yyyy", null),
                            StartTime = DateTime.ParseExact(reader.GetString(2), "HH:mm", null),
                            EndTime = DateTime.ParseExact(reader.GetString(3), "HH:mm", null)
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
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
    }
}
