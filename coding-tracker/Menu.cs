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
                    DataAccessManager.CreateRecord();
                    break;
                case 2:
                    Visualizer.PrintRecords(DataAccessManager.ReadAll());
                    break;
                case 3:
                    DataAccessManager.Update();
                    break;
                case 4:
                    DataAccessManager.Delete();
                    break;
            }
        }
    }

    public static class InputHandler
    {
        public static string GetDateInput(string message)
        {
            Console.Clear();
            string answer = AnsiConsole.Prompt<string>(
                new TextPrompt<string>(message)
                .ValidationErrorMessage("[red]Invalid date![/]")
                .Validate(ValidateDate)
                );
            return answer;
        }

        public static string GetTimeInput(string message)
        {
            Console.Clear();
            string answer = AnsiConsole.Prompt<string>(
                new TextPrompt<string>(message)
                .ValidationErrorMessage("[red]Invalid time![/]")
                .Validate(ValidateTime)
                );
            return answer;
        }

        public static int GetId(string message)
        {
            int id;
            int.TryParse(AnsiConsole.Ask<string>(message), out id);
            return id;
        }
        public static bool ValidateTime(string time)
        {
            return TimeOnly.TryParseExact(time, "HH:mm", out _);
        }

        public static bool ValidateDate(string date)
        {
            return DateOnly.TryParseExact(date, "dd/MM/yyyy", out _);
        }
    }
}
