using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Welcome to the program\n");

        Console.WriteLine("Please enter your name: ");
        string name = Console.ReadLine();

        Console.Write("Please enter your favorite number ");
        int number = int.Parse(Console.ReadLine());

        int total =number * number;

        Console.WriteLine($"{name}, the square of your number is {total}");

    }
}