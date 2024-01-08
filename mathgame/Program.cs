// See https://aka.ms/new-console-template for more information

using System.Data;

startScreen.initial();
class startScreen
{
    public static void initial()
    {
        Helper.PrintHomeScreen();
        var option = Console.ReadKey();
        startKeyHandler(option);
    }

    static void startKeyHandler(ConsoleKeyInfo option)
    {
        switch (option.Key)
        {
            case ConsoleKey.A:
                GameEngine.Play("+");
                break;

            case ConsoleKey.S:
                GameEngine.Play("-");
                break;

            case ConsoleKey.M:
                GameEngine.Play("*");
                break;

            case ConsoleKey.D:
                GameEngine.Play("/");
                break;

            case ConsoleKey.H:
                GameEngine.getHistory();
                break;

            case ConsoleKey.Q:
                Environment.Exit(0);
                break;

            default:
                Console.WriteLine("ok");
                break;
        }
    }
}
public static class Helper
{
    public static void PrintHomeScreen()
    {
        Console.WriteLine("Hello");
        Console.WriteLine("This is a MathGame! Choose an option:\n");
        Console.WriteLine("A - Addition");
        Console.WriteLine("S - Subtraction");
        Console.WriteLine("M - Multiplication");
        Console.WriteLine("D - Division");
        Console.WriteLine("H - History");
        Console.WriteLine("Q - Quit\n");
    }
    public static int GetAnswer()
    {
        int answer;
        bool isValidAnswer = Int32.TryParse(Console.ReadLine(), out answer);

        while (!isValidAnswer)
        {
            Console.WriteLine("Invalid Answer!");
            isValidAnswer = Int32.TryParse(Console.ReadLine(), out answer);
        }

        return answer;
    }
    public static void printCorrect(bool correct)
    {
        Console.WriteLine(correct ? "Correct! +1 point" : "Wrong!");
    }
    public static bool determineWin(int points, int maxPoints)
    {
        Console.Clear();
        Console.WriteLine($"You finish with {points} points!");
        bool isWin;
        if(points >= maxPoints / 2)
        {
            Console.WriteLine("You win!  Good job!");
            isWin = true;
        }


        else
        {
            Console.WriteLine($"Sorry, you lost!");
            isWin = false;
        }
        Console.WriteLine("Press any key to continue.");
        return isWin;
    }
}
public class GameEngine
{
    private static Random random = new();
    readonly static int lowerRandomBoundary = 0;
    readonly static int upperRandomBoundary = 10;
    readonly static int rounds = 5;
    readonly static int maxPoints = rounds;
    private static List<string> history = [];
    public static void Play(string mode)
    {
        Console.Clear();
        int points = 0;

        for(int i = 0; i < rounds; i++)
        {
            int randNum1 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            int randNum2 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            string expression = $"{randNum1} {mode} {randNum2}";

            Console.WriteLine("-------");
            Console.WriteLine($"expression");
            Console.WriteLine("-------");

            object result = new DataTable().Compute(expression, null);
            int correctAnswer = Convert.ToInt32(result);
            Console.WriteLine($"You've got {points} points");
            int answer = Helper.GetAnswer();
            history.Add($"\t{expression}  |  answer: {answer} | correctAnswer: {correctAnswer}\n");
            Console.Clear();
            bool correct = (answer == correctAnswer);
            if (correct)
                points++;
            Helper.printCorrect(correct);
        }

        bool win = Helper.determineWin(points, maxPoints);
        history.Add(win ? "\n\t ---- You won this one! ---- \n" : "\n\t ---- You lost this one! ---- \n" ); 
        Console.ReadKey();
        Console.Clear();
        startScreen.initial();
    }
    public static void getHistory()
    {
        Console.Clear();
        Console.WriteLine("Your History: \n");
        foreach(string record in history)
            Console.WriteLine(record);
        Console.WriteLine("Press any key to go back./");
    }
}

