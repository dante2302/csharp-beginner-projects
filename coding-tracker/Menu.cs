using Database;
using Visualization;

namespace Menu 
{
    public static class MenuHandler
    {
        public static void MainMenu()
        {
            Visualizer.PrintMainMenu();
            var option = AnsiConsole.Ask<int>("[grey]Choose an option: [/]");
            switch(option)
            {
                case 0: 
                    Environment.Exit(0);
                    break;
                case 1:
                    DataAcessManager.CreateRecord();
                    break;
                case 2:
                    Visualizer.PrintRecords(DataAcessManager.ReadAll());
                    break;
                case 3:
                    DataAcessManager.Update();
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

        public static int GetId(string message)
        {
            int id;
            int.TryParse(AnsiConsole.Ask<string>(message), out id);
            return id;
        }
    }
}
