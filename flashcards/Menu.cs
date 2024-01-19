using DBManagement;
using DBClasses;
using Visualization;

namespace Menu
{
    public class MenuHandler
    {
        public static void MainMenu()
        {
            MenuPrinter.PrintMainMenu();
            string option = InputHandler.GetOptionInput();
            switch (option) 
            {
                case "S":
                    break;
                case "MS":
                    StackChoiceMenu();
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
        public static void NoStackMenu()
        {
            string input = InputHandler.NoStacksInput();
            switch (input)
            {
                case "0":
                    MainMenu();
                    break;
                case "1":
                    Console.Clear();
                    Console.WriteLine("Type The Stack Name(Topic) You Want");
                    string stackName = Console.ReadLine();
                    StackRepo.Create(stackName);
                    break;
            }
        }
        public static void StackChoiceMenu()
        {
            List<Stack> stackNames = StackRepo.GetAllStacks();
            if (stackNames.Count == 0)
                NoStackMenu();
            
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
                    MainMenu();
                    break;
                case "G":
                    StackChoiceMenu();
                    break;
                case "M":
                    ManageStackMenu();
                    break;
                case "V":
                    List<Flashcard> stackFlashcards = FlashcardsRepo.GetAllFromAStack(stack.Id);
                    CardPrinter.PrintFlashcards(stackFlashcards, stack.Topic);
                    break;
                case "N":
                    break;
                case "E":
                    break;
                case "D":
                    break;
            }
        }

        public static void ManageStackMenu()
        {

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
    }
}
