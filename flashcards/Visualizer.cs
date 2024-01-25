using ConsoleTableExt;
using DBClasses;
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
            Console.WriteLine("M- Manage Stacks And Flashcards");
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
        public static void PrintStackChoice(List<Stack> stackNameList)
        {
            Console.Clear();
            Console.WriteLine("\n------------\n");
            Console.WriteLine("0 - Go Back To The Main Menu");
            Console.WriteLine("1 - Create A New Stack");
            Console.WriteLine("yourStackName - Manage The Stack");
            Console.WriteLine("\n\n------------");
            Console.WriteLine("Your Stacks:");
            for (int i = 0; i < stackNameList.Count; i++)
            {
                Console.WriteLine("***");
                Console.WriteLine(stackNameList[i]?.Topic);
            }
        }

        public static void PrintWorkingStackMenu(string stackName) 
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Current Working Stack: {stackName}");
            Console.WriteLine("0 - Return To The Main Menu");
            Console.WriteLine("G - Change The Current Working Stack");
            Console.WriteLine("M - Manage Current Stack");
            Console.WriteLine("V - View All Flashcards");
            Console.WriteLine("N - View N Number Of Flashcards");
            Console.WriteLine("C - Create A Flashcard");
            Console.WriteLine("E - Edit A Flashcard");
            Console.WriteLine("D - Delete A Flashcard");
            Console.WriteLine("-------------------------");

        }

        public static void PrintStackManageMenu(string stackTopic)
        {
            Console.WriteLine($"Managing Stack: {stackTopic}\n");
            Console.WriteLine("-------------------------");
            Console.WriteLine("");
        }
        public static void PrintConfirmation(string forDeletion = "")
        {
        }
    }
    public class CardPrinter
    {
        public static void PrintFlashcards(List<Flashcard> cards, string stackName)
        {
            Console.Clear();
            ConsoleTableBuilder
               .From(cards)
                .WithTitle(stackName)
                .ExportAndWriteLine();
        }

        public static void PrintFlashcardDTO(List<FlashcardDTO> cards, string stackName)
        {
            Console.Clear();
            ConsoleTableBuilder
               .From(cards)
                .WithTitle(stackName)
                .ExportAndWriteLine();
        }
    }

    public class SessionPrinter
    {
        public static void PrintSessions(List<StudySession> sessions, string stackName)
        {
            Console.Clear();
            ConsoleTableBuilder
               .From(sessions)
                .WithTitle(stackName)
                .ExportAndWriteLine();
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
