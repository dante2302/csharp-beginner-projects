using DBClasses;
using DBManagement;
using Visualization;

namespace Menu
{
    public class MainMenu
    {
        public static void Init()
        {
            MenuPrinter.PrintMainMenu();
            string option = InputHandler.GetOptionInput();
            switch (option)
            {
                case "S":
                    break;
                case "MS":
                    StackMenu.StackChoice();
                    break;
                case "MF":
                    break;
                case "R":
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
    public class StackMenu()
        {
            public static void NoStack()
            {
                string input = InputHandler.NoStacksInput();
                switch (input)
                {
                    case "0":
                        MainMenu.Init();
                        break;
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Type The Stack Name(Topic) You Want");
                        string stackName = Console.ReadLine();
                        StackRepo.Create(stackName);
                        break;
                }
            }
        public static void StackChoice()
        {
            List<Stack> stackNames = StackRepo.GetAllStacks();
            if (stackNames.Count == 0)
                NoStack();

            MenuPrinter.PrintStackChoice(stackNames);
            string input = Console.ReadLine();
            Stack workingStack = stackNames.FirstOrDefault(stack => stack.Topic == input);
            WorkingStackMenu(workingStack);
        }

        public static void WorkingStackMenu(Stack stack)
        {
            MenuPrinter.PrintWorkingStackMenu(stack.Topic);
            string input = InputHandler.GetOptionInput();
            switch (input)
            {
                case "0":
                    MainMenu.Init();
                    break;
                case "G":
                    StackChoice();
                    break;
                case "M":
                    ManageStackMenu();
                    break;
                case "V":
                    List<Flashcard> allStackFlashcards = FlashcardsRepo.GetNFromAStack(stack.Id);
                    CardPrinter.PrintFlashcards(allStackFlashcards, stack.Topic);
                    break;
                case "N":
                    int number = InputHandler.GetNumberInput();
                    List<Flashcard> nStackFlashcards = FlashcardsRepo.GetNFromAStack(stack.Id, number);
                    CardPrinter.PrintFlashcards(nStackFlashcards, stack.Topic);
                    break;
                case "E":
                    break;
                case "D":
                    break;
            }
        }

        public static void ManageStackMenu()
        {
            Print

        }
    }
    public class InputHandler
    {
        public static string GetOptionInput()
        {
            return Console.ReadLine().ToUpper();
        }
        public static string NoStacksInput()
        {
            ErrorPrinter.NoStacks();
            return Console.ReadLine();
        }
        public static int GetNumberInput()
        {
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
