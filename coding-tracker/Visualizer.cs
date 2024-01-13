using Database;

namespace Visualization
{
    public static class Visualizer
    {
        public static void PrintMenu()
        {
            var panel = new Panel(
                "0 - Close the application \n" +
                "1 - Create a record \n" +
                "2 - Read all records \n" +
                "3 - Update a record \n" +
                "4 - Delete a record \n"
            );

            panel.Header = new PanelHeader("MAIN MENU").Centered();
            AnsiConsole.Write(panel);
        }
        public static void PrintRecords<T>(List<T> recordList) where T : Record
        {
            Console.Clear();
            ConsoleTableBuilder
                .From(recordList)
                .WithTitle("coding")
                .ExportAndWriteLine();
        }

    }

}
