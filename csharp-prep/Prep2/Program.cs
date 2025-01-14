using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the grade percentage?");
        string percentage = Console.ReadLine();
        int number = int.Parse(percentage);

        if (number >= 90)
        {
            Console.WriteLine("You have an A");
        }
        else if (number < 90 && number >= 80)
        {
            Console.WriteLine("You have a B");
        }
        else if (number < 80 && number >= 70)
        {
            Console.WriteLine("You have a C");
        }
        else if (number < 70 && number >= 60)
        {
            Console.WriteLine("You have a D");
        }
        else if (number<60)
        {
            Console.Write("You have an F");
        }
        else
        {
            Console.WriteLine("Something's wrong");
        }
    }
}