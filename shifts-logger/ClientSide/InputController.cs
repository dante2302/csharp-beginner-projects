
using System.Runtime.Serialization;

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
                Shift newShift = GetNewShiftInput();
                bool isCreated = await shiftSrv.Create(newShift);

                if (isCreated)
                    Console.WriteLine("Successfully created!");
                else
                    Console.WriteLine("Something went wrong!");

                await BackToMain();
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
                Console.Write("Id of the shift you want to see: ");
                int id = Int32.Parse(Console.ReadLine());
                Shift shift = await shiftSrv.GetOne(id);

                if (shift is null)
                    Console.WriteLine("No shift with the given id");
                else
                    Printer.PrintShifts([shift]);

                await BackToMain();
                break;

            case "4":
                Console.Clear();
                Console.Write("Enter the id of the shift you want to edit: ");
                int idForEdit = Int32.Parse(Console.ReadLine());

                while(await shiftSrv.GetOne(idForEdit) is null)
                {
                    Console.Clear();
                    Console.WriteLine("No shift with he given id");
                    Console.Write("Enter the id of the shift you want to edit: ");
                    idForEdit = Convert.ToInt32(Console.ReadLine());
                }

                Shift editedShift = GetNewShiftInput();
                bool success =  await shiftSrv.Edit(idForEdit, editedShift);

                if (success)
                    Console.WriteLine("Edited successfully!");
                else
                    Console.WriteLine("Something went wrong!");

                await BackToMain();
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

    public static Shift GetNewShiftInput()
    {

        string dateFormat = "yyyy-MM-dd";
        string timeFormat = "HH:mm";

        Console.Write("Date: ");
        string dateString = Console.ReadLine();

        DateOnly date;

        while(!DateOnly.TryParseExact(dateString,format: dateFormat ,out date))
        {
            Console.WriteLine($"Invalid input! Follow this format: {dateFormat}");
            dateString = Console.ReadLine();
        }

        Console.Write("Start: ");
        string startString = Console.ReadLine();

        TimeOnly startTime;

        while(!TimeOnly.TryParseExact(startString, format: timeFormat, out startTime))
        {
            Console.WriteLine($"Invalid input! Follow this format: {timeFormat}");
            startString = Console.ReadLine();
        }

        Console.Write("End: ");
        string endString = Console.ReadLine();

        TimeOnly endTime;

        while(!TimeOnly.TryParseExact(endString, format: timeFormat, out endTime))
        {
            Console.WriteLine($"Invalid input! Follow this format: {timeFormat}");
            endString = Console.ReadLine();
        }

        return new Shift() { Date = date, Start = startTime, End = endTime };
    }
}
