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
                case "M":
                    ManageMenu.StackChoice();
                    break;
                case "R":
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public class ManageMenu()
    {
        private static Stack workingStack;
        public static void NoStack()
        {
            string input = InputHandler.NoStacksInput();
            switch (input)
            {
                case "0":
                    MainMenu.Init();
                    break;
                case "1":
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
            switch (input)
            {
                case "0":
                    MainMenu.Init();
                    break;
                case "1":
                    string newStackName = InputHandler.GetStringInput("Type The Name(Topic) Of The Stack: ");
                    StackRepo.Create(newStackName);
                    MainMenu.Init();
                    break;
            }
            workingStack = stackNames.FirstOrDefault(stack => stack.Topic == input);
            WorkingStackMenu();
        }

        public static void WorkingStackMenu()
        {
            MenuPrinter.PrintWorkingStackMenu(workingStack.Topic);
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
                    List<Flashcard> allStackFlashcards = FlashcardsRepo.GetNFromAStack(workingStack.Id);
                    CardPrinter.PrintFlashcards(allStackFlashcards, workingStack.Topic);
                    break;
                case "N":
                    int number = InputHandler.GetNumberInput();
                    List<Flashcard> nStackFlashcards = FlashcardsRepo.GetNFromAStack(workingStack.Id, number);
                    CardPrinter.PrintFlashcards(nStackFlashcards, workingStack.Topic);
                    break;
                case "C":
                    CreateFlashcardMenu();
                    break;
                case "E":
                    EditFlashcardMenu();
                    break;
                case "D":
                    DeleteFlashcardMenu();
                    break;
            }
        }

        public static void ManageStackMenu()
        {
            Console.WriteLine($"Current Working Stack: {workingStack.Topic}");
            MenuPrinter.PrintStackManageMenu();
            string input = InputHandler.GetOptionInput();
            switch(input) 
            {
                case "E":
                    string editInfo = InputHandler.GetEditInfo();
                    StackRepo.Edit(workingStack.Id, editInfo);
                    break;
                case "D":
                    if (InputHandler.GetConfirmation())
                        StackRepo.Delete(workingStack.Id);
                    else
                        WorkingStackMenu();
                    break;
            }
        }


        public static void CreateFlashcardMenu()
        {
            string Front = InputHandler.GetStringInput("Type The Front Part Of The Card: ");
            string Back = InputHandler.GetStringInput("Type The Back Part Of THe Card: ");
            FlashcardsRepo.Create(Front, Back, workingStack.Id);
            WorkingStackMenu();
        }

        public static void EditFlashcardMenu()
        {
            int cardIdForEdit = InputHandler.GetIntInput("Which Card Do You Want To Edit(Id)"); 
            string propForEdit = InputHandler.GetStringInput("Type Which Property You Want To Edit\n Front(f) or Back(b)").ToUpper();
            switch (propForEdit)
            {
                case "F":
                case "FRONT":
                    string frontEditInfo = InputHandler.GetStringInput("Type The New Value Of The Card Front");
                    FlashcardsRepo.Edit(cardIdForEdit, "Front", frontEditInfo);
                    break;
                case "B":
                case "BACK":
                    string backEditInfo = InputHandler.GetStringInput("Type The New Value Of The Card Back");
                    FlashcardsRepo.Edit(cardIdForEdit, "Back", backEditInfo);
                    break;
            }
        }

        public static void DeleteFlashcardMenu()
        {
            int cardId = InputHandler.GetIntInput("Which Card Do You Want To Delete(Id)");
            InputHandler.GetConfirmation($"Flashcard - #{cardId}");
            FlashcardsRepo.Delete(cardId);
        }
    }
    public class InputHandler
    {
        public static int GetIntInput(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            int input = Convert.ToInt32(Console.ReadLine());
            return input;
        }

        public static string GetStringInput(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return input;
        }

        public static string GetEditInfo()
        {
            return Console.ReadLine();
        }

        public static bool GetConfirmation(string typeOfConfirmation="")
        {
            MenuPrinter.PrintConfirmation(typeOfConfirmation);
            string input = Console.ReadLine();
            return (input == "y" ? true : false);
        }

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
