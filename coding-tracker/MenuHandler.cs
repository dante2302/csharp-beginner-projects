using Visualization;

public static class MenuHandler
{
    public static void MainMenu()
    {
        Visualizer.PrintMainMenu();
        var option = AnsiConsole.Ask<int>("[grey]Choose an option: [/]");
        switch (option)
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


