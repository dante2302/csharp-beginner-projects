using ConsoleTableExt;

namespace Visualization 
{
    public class Printer 
    {
        public static void PrintMainMenu()
        {
            string symbol = "-";
            string symbolLine = symbol;

            for(int i = 0; i < 15; i++)
                symbolLine += $" {symbol}";

            Console.WriteLine(symbolLine);
            Console.WriteLine("\t Main Menu \t");
            Console.WriteLine("S - Study");
            Console.WriteLine("MS - Manage Stacks");
            Console.WriteLine("MF - Manage Flashcards");
            Console.WriteLine("V - View Session Report");
            Console.WriteLine("0 - Exit");
            Console.WriteLine(symbolLine);
        }
    }
}
