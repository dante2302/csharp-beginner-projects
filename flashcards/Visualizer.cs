
namespace Visualization 
{
    public class MenuPrinter 
    {
        public static void PrintMainMenu()
        {

            string symbol = "-";
            string symbolLine = symbol;
            for (int i = 0; i < 15; i++)
                symbolLine += $" {symbol}";
            Console.Clear();
            Console.WriteLine(symbolLine);
            Console.WriteLine("\t Main Menu \t");
            Console.WriteLine("S - Study");
            Console.WriteLine("MS - Manage Stacks");
            Console.WriteLine("MF - Manage Flashcards");
            Console.WriteLine("R - View Session Report");
            Console.WriteLine("0 - Exit");
            Console.WriteLine(symbolLine);
        }

        public static void PrintStackMenu(string currentStack)
        {
            Console.Clear();
            Console.WriteLine("View All Stacks");
            Console.WriteLine("Delete A Stack");
            Console.WriteLine("View Flashcards In A Stack");
            Console.WriteLine("0 - Return To Main Menu");
        }
        public static bool PrintStackChoice(List<string> stackNameList)
        {
            Console.Clear();
            Console.WriteLine("Choose A Working Stack: ");

            for(int i = 0; i < stackNameList.Count; i++)
                Console.WriteLine(stackNameList[i]);
            return true;
        }
    }

    public class ErrorPrinter 
    { 
        public static void NoStacks()
        {
            Console.Clear();
            Console.WriteLine("No Stacks To Work With!\n");
            Console.WriteLine("Type 1 To Create A New Stack\nType 0 To Return To The Main Menu");
        }
    }

}
