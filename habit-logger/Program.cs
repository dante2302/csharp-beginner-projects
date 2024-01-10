// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;
MenuHandler.DisplayAndInput();
class MenuHandler
{
    public static void PrintMenu()
    {
        Console.WriteLine("Main Menu\n");
        Console.WriteLine("Choose an option:");
        Console.WriteLine("0 - Close the application");
        Console.WriteLine("1 - Read all records");
        Console.WriteLine("2 - Create record");
        Console.WriteLine("3 - Update record");
        Console.WriteLine("4 - Delete record\n");
    }

    public static void DisplayAndInput()
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
                Console.WriteLine("sex");
                break;

            case ConsoleKey.D2:
                break;

            case ConsoleKey.D3:
                break;

            case ConsoleKey.D4:
                break;
        }
    }
}
class DataAcessManager()
{
    public static void StartConnection()
    {
        string connectionString = @"Data Source=habit-logger.db";
        using (var connection = new SqliteConnection(connectionString))

        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXIST drinking_aceton(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Quantity INTEGER,
            Date TEXT
            )";
        }
    }
    public static void Insert()
    {

    }
}
