// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Security.Cryptography;


PrintHomeScreen();
var option = Console.ReadLine().ToUpper();
switch (option)
{
    case "A":
        PlayGame.Addition();
        break;
    case "S":
        PlayGame.Subtraction();
        break;
    case "M":
        PlayGame.Multiplication();
        break;
    case "D":
        PlayGame.Division();
        break;
 //   case "Q":
  //      Environment.Exit(0);
   //     break;
    default:
        Console.WriteLine("ok");
        break;
}

Console.ReadKey();
static void PrintHomeScreen()
{
    Console.WriteLine("Hello");
    Console.WriteLine("This is a MathGame! Choose an option:\n");
    Console.WriteLine("A - Addition");
    Console.WriteLine("S - Subtraction");
    Console.WriteLine("M - Multiplication");
    Console.WriteLine("D - Division");
    Console.WriteLine("Q - Quit\n");
}

public static class PlayGame
{
    private static Random random = new();
    private static int lowerRandomBoundary = 0;
    private static int upperRandomBoundary = 10;
    private static int rounds = 3;
    private static int maxPoints = rounds;

    private static void PrintNums(int num1, int num2, string mode)
    {
        Console.WriteLine("------");
        Console.WriteLine($"{num1} {mode} {num2}");
        Console.WriteLine("------");
    }

    private static int GetAnswer()
    {
        int answer;
        bool isValidAnswer = Int32.TryParse(Console.ReadLine(), out answer);
        Console.Clear();

        while (!isValidAnswer)
        {
            Console.WriteLine("Invalid Answer!");
            isValidAnswer = Int32.TryParse(Console.ReadLine(), out answer);
        }

        return answer;
    }

    public static void Addition()
    {
        Console.Clear();
        int points = 0;

        for(int i = 0; i < rounds; i++)
        {
            int randNum1 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            int randNum2 = random.Next(lowerRandomBoundary, upperRandomBoundary);
            PrintNums(randNum1, randNum2, "+");
            int answer = GetAnswer();
            if (answer == randNum1 + randNum2)
                points++;
        }

        if(points >= maxPoints / 2)
        {
            Console.WriteLine("You win!  Good job!");
        }
        else
        {
            Console.WriteLine($"Sorry, you lost!, you only got {points} points");
        }
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

