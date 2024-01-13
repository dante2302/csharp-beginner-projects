using Spectre.Console;

namespace Menu 
{
    static class MenuHandler
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
        public static void MainMenu()
        {
            PrintMenu();
            var option = AnsiConsole.Ask<int>("[grey]Choose an option: [/]");
            Console.WriteLine(option);
        }
    }
}
