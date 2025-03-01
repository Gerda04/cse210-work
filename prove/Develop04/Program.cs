using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;

    public void Start()
    {
        Console.Clear();
        Console.WriteLine(GetStartingMessage());
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready to begin...");
        ShowSpinner(3);
        RunActivity();
        Console.WriteLine(GetEndingMessage());
        ShowSpinner(3);
    }

    protected abstract void RunActivity();
    protected abstract string GetStartingMessage();
    protected abstract string GetEndingMessage();

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    protected override string GetStartingMessage() =>
        "Breathing Exercise: This activity helps you relax through controlled breathing. Focus on your breath.";

    protected override string GetEndingMessage() =>
        $"Great job! You completed a {duration}-second breathing exercise.";

    protected override void RunActivity()
    {
        for (int i = 0; i < duration / 6; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(3);
            Console.WriteLine("Breathe out...");
            ShowCountdown(3);
        }
    }

    private void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?"
    };

    protected override string GetStartingMessage() =>
        "Reflection Exercise: Reflect on a past experience to find strength and insight.";

    protected override string GetEndingMessage() =>
        $"Well done! You reflected for {duration} seconds.";

    protected override void RunActivity()
    {
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        ShowSpinner(3);

        int elapsedTime = 0;
        while (elapsedTime < duration)
        {
            Console.WriteLine(questions[rand.Next(questions.Count)]);
            ShowSpinner(5);
            elapsedTime += 5;
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    protected override string GetStartingMessage() =>
        "Listing Exercise: List as many positive things as you can in the given area.";

    protected override string GetEndingMessage() =>
        $"Great job! You completed the listing exercise for {duration} seconds.";

    protected override void RunActivity()
    {
        Random rand = new Random();
        Console.WriteLine(prompts[rand.Next(prompts.Count)]);
        ShowSpinner(3);

        Console.WriteLine("Start listing now:");
        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.Write($"> ");
            Console.ReadLine();
            count++;
        }
        Console.WriteLine($"You listed {count} items!");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Exercise");
            Console.WriteLine("2. Reflection Exercise");
            Console.WriteLine("3. Listing Exercise");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => throw new InvalidOperationException("Invalid option.")
            };
            
            if (activity == null) break;
            activity.Start();
        }
    }
}
