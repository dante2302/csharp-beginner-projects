using Microsoft.Data.Sqlite;
using Visualization;

static class DataAccessManager
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
        Console.Clear();
        string dateInput = InputHandler.GetDateInput(Messages.DateInput);
        string startTimeInput = InputHandler.GetTimeInput(Messages.StartTimeInput);
        string endTimeInput = InputHandler.GetTimeInput(Messages.EndTimeInput);

        while (!isValidDuration(startTimeInput, endTimeInput))
        {
            Console.WriteLine(Messages.DurationError);
            startTimeInput = InputHandler.GetTimeInput("");
            endTimeInput = InputHandler.GetTimeInput("");
        }

        ExecuteNonQueryCommand(
            @$"INSERT INTO coding(date, startTime, endTime) 
                   VALUES('{dateInput}', '{startTimeInput}', '{endTimeInput}')"
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

                        Date = DateOnly.FromDateTime(DateTime.ParseExact(reader.GetString(1), "dd/MM/yyyy", null)),
                        StartTime = TimeOnly.FromDateTime(DateTime.ParseExact(reader.GetString(2), "HH:mm", null)),
                        EndTime = TimeOnly.FromDateTime(DateTime.ParseExact(reader.GetString(3), "HH:mm", null))
                    }
                );
            }
            connection.Close();
            return tableRecords;
        }
    }

    public static void Delete()
    {
        int id = InputHandler.GetId($"{Messages.RecordChange} delete: ");
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            while (!RecordExists(id))
            {
                Console.WriteLine();
                int.TryParse(Console.ReadLine(), out id);
            }

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE FROM coding WHERE id = {id}";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
        Console.WriteLine($"{Messages.RecordChangeSuccess}deleted!");
        Console.WriteLine(Messages.BackToMainMenu);
        Console.ReadLine();
        MenuHandler.MainMenu();
    }

    public static void Update()
    {
        Console.Clear();
        int id = InputHandler.GetId($"{Messages.RecordChange} update: ");

        while (!RecordExists(id))
        {
            Console.Clear();
            id = InputHandler.GetId(Messages.InvalidId);
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

        ExecuteNonQueryCommand(
        @$"UPDATE coding
               SET '{updateKey}' = '{updateValue}'
               WHERE id = {id}"
        );
        Console.WriteLine($"{Messages.RecordChangeSuccess} updated!");
        Console.WriteLine(Messages.BackToMainMenu);
        Console.ReadLine();
        MenuHandler.MainMenu();
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
    public static bool isValidDuration(string startTimeInput, string endTimeInput)
    {
        TimeOnly startTime = TimeOnly.ParseExact(startTimeInput, "HH:mm", null);
        TimeOnly endTime = TimeOnly.ParseExact(endTimeInput, "HH:mm", null);
        return startTime < endTime;
    }

}

