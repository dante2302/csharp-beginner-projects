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
                break;

            case ConsoleKey.D4:
                Console.Clear();
                DataAcessManager.Delete();
                break;
        }
    }

    public static string getDateInput()
    {
        Console.WriteLine(@"Please insert a date(format : dd/mm/yy). Type 0 to return to the main menu");
        string date = Console.ReadLine();
        if (date == "0")
            MenuHandler.MainMenu();
        return date;
    }

    public static int getNumberInput(string message)
    {
        Console.WriteLine(message);
        int number;
        int.TryParse(Console.ReadLine(), out number);
        return number;
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
        string date = MenuHandler.getDateInput();
        int quantity = MenuHandler.getNumberInput("Please enter a whole number for quantity.");
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
                            Date = reader.GetString(2)
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
        int recordId = MenuHandler.getNumberInput("Please enter the Id of the record you want to delete: ");
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
}

public class DrinkingWaterRecord
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string Date { get; set; }
}
