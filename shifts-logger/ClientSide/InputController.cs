
public class InputController
{
    private static readonly ShiftService srv = new ShiftService();
    public static async Task Initialize()
    {
        Console.Clear();
        PrintMainMenu();
        string input = Console.ReadLine();
        switch (input)
        {
            case "0":
                Environment.Exit(0);
                break;

            case "1":
                await srv.GetAllShifts();
                break;

            case "2":
                await srv.GetOneShift(6);
                Console.ReadKey();
                break;
            case "3":

                break;
            case "4":

                break;
            case "5":

                break;

        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine("Hello! This Is Your Shift Logger!");
        Console.WriteLine("0 - Exit");
        Console.WriteLine("1 - Create A Shift");
        Console.WriteLine("2 - Read All Shifts");
        Console.WriteLine("3 - Read One Shift");
        Console.WriteLine("4 - Edit A Shift");
        Console.WriteLine("5 - Delete A Shift");
        Console.WriteLine("Choose an option: ");
    }
}
