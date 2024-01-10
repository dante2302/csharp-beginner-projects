// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;

DataAcessManager.StartConnection();

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
    static string connectionString = @"Data Source=habit-logger.db";
    public static void StartConnection()
    {
        using (var connection = new SqliteConnection(connectionString))

        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @"CREATE TABLE IF NOT EXISTS drinking_aceton(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Quantity INTEGER,
            Date TEXT
            )";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public static void Insert()
    {
        string date = MenuHandler.getDateInput();
        int quantity = MenuHandler.getQuantityInput();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"INSERT INTO drinking_aceton(date, quantity) VALUES('{date}', {quantity})";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
