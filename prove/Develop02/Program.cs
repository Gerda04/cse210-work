using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    static List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static List<Entry> journalEntries = new List<Entry>();

    static void Main()
    {
        int choice = 0;
        while (choice != 5)
        {
            Console.WriteLine("\nWelcome to Your Journal!");
            Console.WriteLine("1 - Write a New Entry");
            Console.WriteLine("2 - Display Journal Entries");
            Console.WriteLine("3 - Save Journal to File");
            Console.WriteLine("4 - Load Journal from File");
            Console.WriteLine("5 - Quit");
            Console.Write("Enter your choice: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out choice) || choice < 1 || choice > 5)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    WriteNewEntry();
                    break;
                case 2:
                    DisplayEntries();
                    break;
                case 3:
                    SaveJournalToFile();
                    break;
                case 4:
                    LoadJournalFromFile();
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    break;
            }
        }
    }

    static void WriteNewEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Count)];

        Console.WriteLine($"\nJournal Prompt: {prompt}");
        Console.Write("Your Response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        journalEntries.Add(new Entry(date, prompt, response));

        Console.WriteLine("Entry saved!");
    }

    static void DisplayEntries()
    {
        if (journalEntries.Count == 0)
        {
            Console.WriteLine("No journal entries found.");
            return;
        }

        Console.WriteLine("\n--- Journal Entries ---");
        foreach (var entry in journalEntries)
        {
            Console.WriteLine($"\nDate: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine("-----------------------");
        }
    }

    static void SaveJournalToFile()
    {
        Console.Write("Enter filename to save (e.g., journal.txt): ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in journalEntries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }

        Console.WriteLine($"Journal saved to {filename}!");
    }

    static void LoadJournalFromFile()
    {
        Console.Write("Enter filename to load (e.g., journal.txt): ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        journalEntries.Clear();

        foreach (string line in File.ReadLines(filename))
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                journalEntries.Add(new Entry(parts[0], parts[1], parts[2]));
            }
        }

        Console.WriteLine($"Journal loaded from {filename}!");
    }

    class Entry
    {
        public string Date { get; }
        public string Prompt { get; }
        public string Response { get; }

        public Entry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }
    }
}
