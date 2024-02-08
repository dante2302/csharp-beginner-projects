
public static class InputHandler
{
    public static string GetDateInput(string message)
    {
        string answer = AnsiConsole.Prompt<string>(
            new TextPrompt<string>(message)
            .ValidationErrorMessage("[red]Invalid date![/]")
            .Validate(Validator.ValidateDate)
            );
        return answer;
    }

    public static string GetTimeInput(string message)
    {
        string answer = AnsiConsole.Prompt<string>(
            new TextPrompt<string>(message)
            .ValidationErrorMessage("[red]Invalid time![/]")
            .Validate(Validator.ValidateTime)
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
