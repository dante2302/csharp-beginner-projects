using DBManagement;
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
                    StackMenu();
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

        public static void StackMenu()
        {
            List<string> stackNames = DataManager.GetAllStackNames();
            string input;
            if (stackNames.Count == 0)
            {
                input = InputHandler.NoStacksInput();
                switch (input) 
                {
                    case "0":
                        MainMenu();
                        break;
                }
            }
            MenuPrinter.PrintStackChoice(stackNames);


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
