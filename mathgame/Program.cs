// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Security.Cryptography;


PrintHomeScreen();
var option = Console.ReadLine().ToUpper();
switch (option)
{
    case "A":
        Console.WriteLine("A");
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
    private static int rounds = 3;

    private static void PrintNums(int num1, int num2, string mode)
    {
        Console.WriteLine($"{num1} {mode} {num2}");
    }

    private static int GetAnswer()
    {
        int realAnswer;
        bool isValidAnswer = Int32.TryParse(Console.ReadLine(), out realAnswer);

        while (!isValidAnswer)
        {
            Console.WriteLine("Invalid Answer!");
            isValidAnswer = Int32.TryParse(Console.ReadLine(), out realAnswer);
        }

        return realAnswer;
    }

    public static void Addition()
    {
        for(int i = 0; i < rounds; i++)
        {
            int randNum1 = random.Next(0, 9);
            int randNum2 = random.Next(0, 9);
            PrintNums(randNum1, randNum2, "+");
            GetAnswer();
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

