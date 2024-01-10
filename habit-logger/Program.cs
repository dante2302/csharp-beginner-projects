// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;

Console.WriteLine("Hello, World!");
string connectionString = @"Data Source=habit-logger.db";
using(var connection = new SqliteConnection(connectionString))

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
Console.ReadKey();
