// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;
using System.Globalization;

DataAcessManager.InitialSetup();
MenuHandler.MainMenu();

class MenuHandler
{
    public static void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("Main Menu\n");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("0 - Close the application");
        Console.WriteLine("1 - Read all records");
        Console.WriteLine("2 - Create record");
        Console.WriteLine("3 - Update record");
        Console.WriteLine("4 - Delete record\n");
    }
    public static void BackToMainMenu()
    {
        Console.WriteLine("Press any key to go back to the main menu.");
        Console.ReadKey();
        MenuHandler.MainMenu();
    }
    public static void MainMenu()
    {
        PrintMenu();
        var inputKey = Console.ReadKey().Key;
        switch (inputKey)
        {
            // Dn - stands for number key pressed by user, n being the corresponding number
            case ConsoleKey.D0:
                Environment.Exit(0);
                break;

            case ConsoleKey.D1:
                Console.Clear();
                DataAcessManager.GetAll();
                break;

            case ConsoleKey.D2:
                Console.Clear();
                DataAcessManager.Insert();
                break;

            case ConsoleKey.D3:
                Console.Clear();
                DataAcessManager.Update();
                break;

            case ConsoleKey.D4:
                Console.Clear();
                DataAcessManager.Delete();
                break;
            default:
                Console.WriteLine("Invalid Command.");
                Console.WriteLine("Press a number from 0 to 4");
                break;
        }
    }

    public static string GetDateInput()
    {
        Console.WriteLine(@"Please insert a date(format : dd/mm/yyyy). Type 0 to return to the main menu");
        string date = Console.ReadLine();

        if (date == "0")
            MenuHandler.MainMenu();

        if (!ValidateDateInput(date))
            GetDateInput();

        return date;
    }

    public static int GetNumberInput(string message)
    {
        Console.WriteLine(message);
        string numInput = Console.ReadLine();
        int num = ValidateNumberInput(numInput);
        if (num == -1)
        {
            Console.WriteLine("Invalid Number.");
            GetNumberInput("Please provide a valid one:");
        }
        return num;
    }

    internal static int ValidateNumberInput(string input)
    {
        int num;
        bool isValid = int.TryParse(input, out num);
        if (num < 0)
            isValid = false;

        if (!isValid)
            return -1;

        else 
            return num;
    }

    internal static bool ValidateDateInput(string input)
    {
        return DateTime.TryParseExact(
            input, 
            "dd/MM/yyyy", 
            new CultureInfo("fr-FR"), 
            DateTimeStyles.None,
            out _
        );
    }

}
class DataAcessManager()
{

    internal static string connectionString = @"Data Source=habit-logger.db";
    internal static void ExecuteNonQueryCommand(string command)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                command;
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }

    }
    public static void InitialSetup()
    {
        ExecuteNonQueryCommand(
            @"CREATE TABLE IF NOT EXISTS drinking_water(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Quantity INTEGER,
                Date TEXT
            )"
        );
    }
    public static void Insert()
    {
        string date = MenuHandler.GetDateInput();
        int quantity = MenuHandler.GetNumberInput("Please enter a whole number for quantity.");
        ExecuteNonQueryCommand(
            $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})"
        );
        MenuHandler.BackToMainMenu();
    }

    public static void GetAll()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_water ";

            List<DrinkingWaterRecord> tableData = new();
            SqliteDataReader reader = tableCmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                        new DrinkingWaterRecord
                        {
                            Id = reader.GetInt32(0),
                            Quantity = reader.GetInt32(1),
                            Date = DateTime.ParseExact(reader.GetString(2), "dd/MM/yyyy", new CultureInfo("fr-FR"))
                        }
                    );
                }
            }
            else
            {
                Console.WriteLine("No records found.");
            }
            connection.Close();
            Console.WriteLine("-------------------------\n");
            foreach(var data in tableData)
            {
                Console.WriteLine($"Id:{data.Id} - Quantity:{data.Quantity} - Date:{data.Date}\n");
            }
            Console.WriteLine("-------------------------\n");
            MenuHandler.BackToMainMenu();
        }
    }
    public static void Delete()
    {
        int recordId = MenuHandler.GetNumberInput("Please enter the Id of the record you want to delete: ");
        if (recordId == 0) MenuHandler.BackToMainMenu();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE from drinking_water WHERE Id = '{recordId}'";
            int affectedRowCount = tableCmd.ExecuteNonQuery();
            if (affectedRowCount == 0)
            {
                Console.WriteLine("Record was not found");
                Delete();
            }
            connection.Close();
        };
        Console.WriteLine($"Record with the Id {recordId} was deleted.");
        MenuHandler.BackToMainMenu();
    }
    public static void Update()
    {
        int recordId = MenuHandler.GetNumberInput("Please enter the Id of the record you want to update:");
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE Id = {recordId})";
            int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

            if(checkQuery == 0)
            {
                Console.WriteLine("No such record");
                Update();
            }

            string date = MenuHandler.GetDateInput();
            int quantity = MenuHandler.GetNumberInput("Please enter new quantity:");

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"UPDATE drinking_water SET Date = '{date}', Quantity = {quantity} WHERE Id = {recordId}";
            int affectedRowCount = tableCmd.ExecuteNonQuery();
            if(affectedRowCount == 0)
            {
                Console.WriteLine("No such record.");
                MenuHandler.BackToMainMenu();
            }
            connection.Close();
        };
        Console.WriteLine($"Record with the Id {recordId} was updated.");
        MenuHandler.BackToMainMenu();
    }
}

public class DrinkingWaterRecord
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
}
