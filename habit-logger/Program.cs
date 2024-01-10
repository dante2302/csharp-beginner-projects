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

    public static int getQuantityInput()
    {
        Console.WriteLine(@"Please insert a whole number for the quantity.");
        int quantity;
        Int32.TryParse(Console.ReadLine(), out quantity);
        return quantity;
    }
}
class DataAcessManager()
{
    internal static void ExecuteNonQueryCommand(string command)
    {
        string connectionString = @"Data Source=habit-logger.db";
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
            @"CREATE TABLE IF NOT EXISTS drinking_aceton(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Quantity INTEGER,
                Date TEXT
            )"
        );
    }
    public static void Insert()
    {
        string date = MenuHandler.getDateInput();
        int quantity = MenuHandler.getQuantityInput();
        ExecuteNonQueryCommand(
            $"INSERT INTO drinking_aceton(date, quantity) VALUES('{date}', {quantity})"
        );
        Console.ReadKey();
        MenuHandler.MainMenu();
    }

    public static void GetAll()
    {
        string connectionString = @"Data Source=habit-logger.db";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"SELECT * FROM drinking_aceton ";

            List<DrinkingAcetonRecord> tableData = new();
            SqliteDataReader reader = tableCmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tableData.Add(
                        new DrinkingAcetonRecord
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
                Console.WriteLine("No rows found.");
            }
            connection.Close();
            Console.WriteLine("-------------------------\n");
            foreach(var data in tableData)
            {
                Console.WriteLine($"Id:{data.Id} - Quantity:{data.Quantity} - Date:{data.Date}\n");
            }
            Console.WriteLine("-------------------------\n");
            Console.ReadKey();
        }
    }
}

public class DrinkingAcetonRecord()
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string Date { get; set; }
}
