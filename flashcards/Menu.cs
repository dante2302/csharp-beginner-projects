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
                    var Study = new StudyMenu();
                    Study.Init();
                    break;
                case "M":
                    ManageMenu.StackChoice();
                    ManageMenu.WorkingStackMenu();
                    break;
                case "R":
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

        public static void BackToMain()
        {
            Console.WriteLine("Type Anything To Go Back To The Main Menu...");
            Console.ReadLine();
            Init();
        }
    }

    public class ManageMenu()
    {
        private static Stack workingStack;

        private static void NoStack()
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
        public static Stack StackChoice()
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
            Stack chosenStack = stackNames.FirstOrDefault(stack => stack.Topic == input);
            workingStack = chosenStack;
            return chosenStack;
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
                    Console.WriteLine("Type Anything To Go Back...");
                    Console.ReadLine();
                    WorkingStackMenu();
                    break;

                case "N":
                    int number = InputHandler.GetIntInput("Type The Number Of Flashcards You Want To See: ");
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

        private static void ManageStackMenu()
        {
            Console.WriteLine($"Current Working Stack: {workingStack.Topic}");
            MenuPrinter.PrintStackManageMenu(workingStack.Topic);
            string input = InputHandler.GetOptionInput();
            switch(input) 
            {
                case "E":
                    string editInfo = InputHandler.GetStringInput("Edit");
                    StackRepo.Edit(workingStack.Id, editInfo);
                    break;

                case "D":
                    bool isConfirmed = InputHandler.GetConfirmation($"Are You Sure You Want To Delete Stack: {workingStack.Topic}?");

                    if (isConfirmed)
                        StackRepo.Delete(workingStack.Id);

                    else
                        WorkingStackMenu();

                    break;

                case "0":
                    WorkingStackMenu();
                    break;
            }
        }
        private static void CreateFlashcardMenu()
        {
            string Front = InputHandler.GetStringInput("Type The Front Part Of The Card: ");
            string Back = InputHandler.GetStringInput("Type The Back Part Of THe Card: ");

            Console.Clear();

            if (FlashcardsRepo.Create(Front, Back, workingStack.Id))
            {
                Console.WriteLine("Flashcard was created");
                BackToWorkingStack();
            }

            else
            {
                Console.WriteLine("Something Went Wrong.");
                MainMenu.BackToMain();
            }
        }

        private static void EditFlashcardMenu()
        {
            int cardIdForEdit = InputHandler.GetIntInput("Which Card Do You Want To Edit(Id)"); 
            string propForEdit = InputHandler.GetStringInput("Type Which Property You Want To Edit\n Front(f) or Back(b)").ToUpper();

            Console.Clear();

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

        private static void DeleteFlashcardMenu()
        {
            int cardId = InputHandler.GetIntInput("Which Card Do You Want To Delete(Id)");
            InputHandler.GetConfirmation($"Are You Sure You Want To Delete Flashcard - #{cardId}");

            Console.Clear();

            if (FlashcardsRepo.Delete(cardId))
            {
                Console.WriteLine("Flashcard Was Deleted");
                BackToWorkingStack();
            }

            else
            {
                Console.WriteLine("Something Went Wrong.");
                MainMenu.BackToMain();
            }

        }

        private static void BackToWorkingStack()
        {
            Console.WriteLine("Type Anything To Go Back...");
            Console.ReadLine();
            WorkingStackMenu();
        }
    }

    public class StudyMenu
    {
        private Stack workingStack;
        private int points = 0;
        private int maxPoints;

        public void Init()
        {
            workingStack = ManageMenu.StackChoice();
            Console.Clear();
            Console.WriteLine($"Stack: {workingStack.Topic}");
            List<Flashcard> stackCards = FlashcardsRepo.GetNFromAStack(workingStack.Id);

              if(stackCards.Count < 2)
            {
                Console.WriteLine("Not Enough Flashcards");
                Console.WriteLine("Type Anything To Go Back To The Main Menu");
                Console.ReadLine();
                MainMenu.Init();
                return;
            }

            Console.WriteLine("Type anything to start the working session");
            StartSession(stackCards);
        }

        private void StartSession(List<Flashcard> cards)
        {
            maxPoints = cards.Count;
            foreach (Flashcard card in cards)
            {
                Console.Clear();
                Console.WriteLine($"FRONT: {card.Front}");
                Console.Write("Your Answer: ");
                string answer = Console.ReadLine();

                if(answer == card.Back)
                {
                    points++;
                    Console.WriteLine("Correct! +1 Point");
                    Console.WriteLine($"Points: {points}");
                }

                else
                {
                    Console.WriteLine("Wrong Answer! ");
                    Console.WriteLine($"{card.Front} | {card.Back}");
                    Console.WriteLine($"The Right Answer Is: {card.Back}!");
                }

                Console.WriteLine("Type 0 To Terminate Session");
                Console.WriteLine("Type Anything Else To Go On");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    MainMenu.Init();
                    return;
                }
            }
            Console.WriteLine("End Of Study Session");
            Console.WriteLine($"Good Job! You Got {points} out of {maxPoints} Points!");
            SessionRepo
            MainMenu.BackToMain();
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

        public static bool GetConfirmation(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Y -Yes");
            Console.WriteLine("N - No");

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
    }
}
