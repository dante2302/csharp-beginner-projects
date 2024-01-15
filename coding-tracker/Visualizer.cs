using Database;
using Spectre.Console;

namespace Visualization
{
    public static class Visualizer
    {
        public static void PrintMainMenu()
        {
            PrintPanel(
                "0 - Close the application \n" +
                "1 - Create a record \n" +
                "2 - Read all records \n" +
                "3 - Update a record \n" +
                "4 - Delete a record \n",
                "MAIN MENU");
        }

        public static void PrintPanel(string panelData, string panelHeader)
        {
            Console.Clear();
            var panel = new Panel(panelData);
            panel.Header = new PanelHeader(panelHeader).Centered();
            AnsiConsole.Write(panel);
        }
        public static void PrintUpdateMenu()
        {
            PrintPanel(
                " 0 - Return to the main menu \n" + 
                " d - Update the date of the record \n" +
                "st - Update the start time of the record \n" +
                "et - Update the start time of the record \n"
                ,
                "UPDATE");
        }
        public static void PrintRecords<T>(List<T> recordList) where T : Record
        {
            Console.Clear();
            ConsoleTableBuilder
                .From(recordList)
                .WithTitle("coding")
                .ExportAndWriteLine()
                ;
        }

    }

}
