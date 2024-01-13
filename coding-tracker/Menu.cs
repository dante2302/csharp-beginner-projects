using ConsoleTableExt;
using Database;
using Spectre.Console;
using System.Reflection;

namespace Menu 
{
    public static class MenuHandler
    {
        static void PrintMenu()
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
        public static void MainMenu()
        {
            PrintMenu();
            var option = AnsiConsole.Ask<int>("[grey]Choose an option: [/]");
            switch(option)
            {
                case 0: 
                    Environment.Exit(0);
                    break;
                case 1:
                    DataAcessManager.Create();
                    break;
                case 2:
                    TableVisualizer.PrintRecords(DataAcessManager.ReadAll());
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
          }

    public static class InputHandler
    {
        public static string GetDateInput(string message)
        {
            string answer = AnsiConsole.Prompt<string>(
                new TextPrompt<string>(message)
                .ValidationErrorMessage("[red]Invalid date![/] Please input a valid date in the following format: [yellow]dd/MM/yyyy[/]")
                );
            return answer;
        }

        public static string GetTimeInput(string message)
        {
            string answer = AnsiConsole.Prompt<string>(
                new TextPrompt<string>(message)
                .ValidationErrorMessage("[red]Invalid time![/]")
                );
            return answer;
        }
    }
    public static class TableVisualizer 
    {
        public static void PrintRecords<T>(List<T> recordList) where T : Record
        {
            ConsoleTableBuilder
                .From(recordList)
                .WithTitle("coding")
                .ExportAndWriteLine();
        }
    }
}
