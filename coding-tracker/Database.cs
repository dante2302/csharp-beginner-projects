using Microsoft.Data.Sqlite;
using Menu;
using System.Data;
using Visualization;

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

        public static void CreateRecord()
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
         
        public static void Delete()
        {
            int id = InputHandler.GetId("Type the Id of the record you want to delete: ");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                while (!RecordExists(id))
                {
                    Console.WriteLine("No Such Record Exists.\n Type the correct id or type 0 to exit.");
                    int.TryParse(Console.ReadLine(), out id);
                }

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE FROM coding WHERE id = {id}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Update()
        {
            Console.Clear();
            int id = InputHandler.GetId("Type the Id of the record you want to update: ");
            while (!RecordExists(id))
            {
                Console.Clear();
                id = InputHandler.GetId("Id of the record doesn't exist!\nPlease provide a new one, or type 0 to return to the main menu: ");
                if (id == 0)
                    MenuHandler.MainMenu();
            }

            string updateKey = "";
            string updateValue = "";

            Visualizer.PrintUpdateMenu();
            string option = Console.ReadLine();

            switch (option) 
            {
                case "d":
                    updateKey = "date";
                    updateValue = InputHandler.GetDateInput("asd");
                    break;

                case "st":
                    updateKey = "startTime";
                    updateValue = InputHandler.GetTimeInput("asd");
                    break;

                case "et": 
                    updateKey = "endTime";
                    updateValue = InputHandler.GetTimeInput("asd");
                    break;

                case "0":
                    MenuHandler.MainMenu();
                    break;

                default:
                    break;
            }
            Console.WriteLine(updateKey + updateValue);
        }
        public static bool RecordExists(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM coding WHERE id = {id}";
                var reader = tableCmd.ExecuteReader();
                bool exists = reader.HasRows;
                connection.Close();
                return exists;
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
