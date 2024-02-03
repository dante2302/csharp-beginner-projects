namespace shifts_logger
{
    public class InputController
    {
        public static void Initialize()
        {
            Console.Clear();
            PrintMainMenu();
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    Environment.Exit(0);
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                     
            }
        }

        private static void PrintMainMenu()
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
    }
}
