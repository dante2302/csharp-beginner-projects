
public class InputController
{
    private static readonly ShiftService shiftSrv = new ShiftService();
    public static async Task Initialize()
    {
        Console.Clear();
        Printer.PrintMainMenu();
        string input = Console.ReadLine();
        switch (input)
        {
            case "0":
                Environment.Exit(0);
                break;

            case "1":
                Console.Clear();
                Console.WriteLine("Creating A New Shift: ");

                DateTime start = Convert.ToDateTime(Console.ReadLine());
                DateTime end = Convert.ToDateTime(Console.ReadLine());
                bool isCreated = await shiftSrv.Create(new Shift { End = end, Start = start });

                if (isCreated)
                    Console.WriteLine("Successfully created!");
                else
                    Console.WriteLine("Something went wrong!");

                break;

            case "2":
                List<Shift> shifts = await shiftSrv.GetAll();

                if (shifts is null)
                    Console.WriteLine("No shifts entered.");
                else
                    Printer.PrintShifts(shifts);

                await BackToMain();
                break;

            case "3":
                int id = Convert.ToInt32(Console.ReadLine());
                Shift shift = await shiftSrv.GetOne(id);

                if (shift is null)
                    Console.WriteLine("No shift with the given id");
                else
                    Printer.PrintShifts([shift]);

                await BackToMain();
                break;

            case "4":
                    Console.Clear();
                break;

            case "5":
                Console.Clear();
                Console.Write("Id of the shift for deletion: ");

                int idForDeletion = Convert.ToInt32(Console.ReadLine());
                bool isDeleted = await shiftSrv.Delete(idForDeletion);

                if (isDeleted)
                    Console.WriteLine("Successfully deleted");
                else
                    Console.WriteLine("Something went wrong!");

                await BackToMain();
                break;

        }
    }
    public static async Task BackToMain()
    {
        Console.WriteLine("Press Any Key To Go Back...");
        Console.ReadKey();
        await Initialize();
    }

}
