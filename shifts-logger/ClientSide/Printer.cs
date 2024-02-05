using ConsoleTableExt;

public class Printer
{
    public static void PrintMainMenu()
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

    public static void PrintShifts(List<Shift> shiftList)
    {
        Console.Clear();
        ConsoleTableBuilder
            .From(shiftList)
            .ExportAndWriteLine();
    }

}
