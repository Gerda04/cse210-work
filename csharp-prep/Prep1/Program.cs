using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string f_name = Console.ReadLine();

        Console.Write("What is your last name? ");
        string l_name = Console.ReadLine();

        Console.Write($"Your name is {f_name} {l_name}");
    }
}
