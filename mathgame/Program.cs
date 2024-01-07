// See https://aka.ms/new-console-template for more information

using System.Data;

Helper.PrintHomeScreen();
var option = Console.ReadKey();

switch (option.Key)
{
    case ConsoleKey.A:
        PlayGame.Addition();
        break;
    case ConsoleKey.S:
        PlayGame.Subtraction();
        break;
    case ConsoleKey.M:
        PlayGame.Multiplication();
        break;
    case ConsoleKey.D:
        PlayGame.Division();
        break;
    case ConsoleKey.Q:
        Environment.Exit(0);
        break;
    default:
        Console.WriteLine("ok");
        break;
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
        Console.WriteLine("Q - Quit\n");
    }
    public static void PrintNums(int num1, int num2, string mode)
    {
        Console.WriteLine("------");
        Console.WriteLine($"{num1} {mode} {num2}");
        Console.WriteLine("------");
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
    public static void determineWin(int points, int maxPoints)
    {
        Console.Clear();
        Console.WriteLine($"You finish with {points} points!");
        if(points >= maxPoints / 2)
        {
            Console.WriteLine("You win!  Good job!");
            Console.WriteLine("Press any key to continue.");
        }

        else
        {
            Console.WriteLine($"Sorry, you lost!");
            Console.WriteLine("Press any key to continue.");
        }
    }
}
public static class PlayGame
{
    private static Random random = new();
    private static int lowerRandomBoundary = 0;
    private static int upperRandomBoundary = 10;
    private static int rounds = 5;
    private static int maxPoints = rounds;

    public static void Addition()
    {
        Console.Clear();
        int points = 0;

        for(int i = 0; i < rounds; i++)
        {
            int randNum1 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            int randNum2 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            object result = new DataTable().Compute($"{randNum1} + {randNum2}", null);
            Console.WriteLine($"You've got {points} points");
            Console.WriteLine(Convert.ToInt32(result));
            Helper.PrintNums(randNum1, randNum2, "+");
            int answer = Helper.GetAnswer();
            Console.Clear();
            bool correct = (answer == randNum1 + randNum2);
            if (correct)
                points++;
            Helper.printCorrect(correct);
        }
        Helper.determineWin(points, maxPoints);
    }

    public static void Subtraction()
    {

    }

    public static void Multiplication()
    {

    }

    public static void Division()
    {

    }
}

