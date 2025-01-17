using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is the magic number? ");
        string number = Console.ReadLine();
        int magic_number = int.Parse(number);
        int guess = -1;

        while (guess != magic_number)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magic_number > guess)
            {
                Console.Write("Higher\n");
            }
            else if (magic_number < guess)
            {
                Console.Write("Lower\n");
            }
            else
            {
                Console.Write("You guessed it!");
            }
        }

    }
}