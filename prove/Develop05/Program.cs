using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    // Set vatriables in place
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsComplete = false;
    }

    public abstract int RecordProgress();

    public override string ToString()
    {
        return $"{Name} - {Description} ({Points} points)";
    }
}

// One-time goals
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordProgress()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            return Points;
        }
        return 0;
    }
}

// Repeating goals (e.g., daily scripture study)
class EternalGoal : Goal
// never completed, continue eternally
{
    public EternalGoal(string name, string description, int points) : base(name, description, points) { }

    public override int RecordProgress()
    {
        return Points;
    }

    // override points to replace
}

// Goals that require multiple completions (Attend temple 10 times)
class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int Bonus { get; private set; }

    // track current, targer, add bonus

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus) 
        : base(name, description, points)
    {
        TargetCount = targetCount;
        // desired count
        Bonus = bonus;
        // track bonus points
        CurrentCount = 0;
        //override when adding
    }

    public override int RecordProgress()
    {
        if (IsComplete) return 0;
        
        CurrentCount++;
        int earnedPoints = Points;

        if (CurrentCount >= TargetCount)
        {
            IsComplete = true;
            earnedPoints += Bonus;
            Console.WriteLine($"Bonus Earned! You got {Bonus} extra points!");
        }
        
        return earnedPoints;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - Completed {CurrentCount}/{TargetCount}";
    }
}

class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int score = 0;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoal(string goalName)
    {
        Goal goal = goals.Find(g => g.Name == goalName);
        if (goal != null)
        {
            int pointsEarned = goal.RecordProgress();
            score += pointsEarned;
            Console.WriteLine($"{goal.Name} recorded. You earned {pointsEarned} points.");
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }

    public void ShowGoals()
    {
        Console.WriteLine("\nYour Goals:");
        foreach (var goal in goals)
        {
            string status = goal.IsComplete ? "[X]" : "[ ]";
            Console.WriteLine($"{status} {goal}");
            // display whether complete and the goal
        }
    }

    public void ShowScore()
    {
        Console.WriteLine($"\nYour Score is {score}");
        ShowLevel();
    }

    private void ShowLevel()
    {
        string level = score switch
        {
            >= 5000 => "Master",
            >= 2000 => "Dedicated",
            >= 1000 => "Aspiring",
            _ => "Novice"
        };
        Console.WriteLine($"🎖 Level: {level}");
    }

    public void SaveProgress()
    {
        using (StreamWriter writer = new StreamWriter("progress.txt"))
        {
            writer.WriteLine(score);
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}|{goal.Name}|{goal.Description}|{goal.Points}|{(goal is ChecklistGoal cg ? $"{cg.CurrentCount}/{cg.TargetCount}|{cg.Bonus}" : "N/A")}");
            }
        }
        Console.WriteLine("Progress saved!");
    }

    public void LoadProgress()
    {
        if (!File.Exists("progress.txt")) return;

        //check if file exists before continuing

        string[] lines = File.ReadAllLines("progress.txt");
        score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0], name = parts[1], description = parts[2];
            int points = int.Parse(parts[3]);

            if (type == "SimpleGoal") goals.Add(new SimpleGoal(name, description, points));
            else if (type == "EternalGoal") goals.Add(new EternalGoal(name, description, points));
            else if (type == "ChecklistGoal")
            // determines how type of goal will be stored
            {
                string[] checklistData = parts[4].Split('/');
                int currentCount = int.Parse(checklistData[0]);
                int targetCount = int.Parse(checklistData[1]);
                int bonus = int.Parse(parts[5]);
                var goal = new ChecklistGoal(name, description, points, targetCount, bonus);
                while (goal.CurrentCount < currentCount) goal.RecordProgress();
                goals.Add(goal);
            }
        }
        Console.WriteLine(" Progress loaded!");
    }
}

// Main Program
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        manager.LoadProgress();

        while (true)
        {
            Console.WriteLine("\n Eternal Quest Menu:");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Goal Progress");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save & Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter points: ");
                    int points = int.Parse(Console.ReadLine());

                    Console.WriteLine("Goal Type: (1) Simple (2) Eternal (3) Checklist");
                    string type = Console.ReadLine();

                    if (type == "1") manager.AddGoal(new SimpleGoal(name, description, points));
                    else if (type == "2") manager.AddGoal(new EternalGoal(name, description, points));
                    else if (type == "3")
                    {
                        Console.Write("Target count: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus points: ");
                        int bonus = int.Parse(Console.ReadLine());
                        manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                    }
                    break;

                case "2":
                    Console.Write("Enter goal name: ");
                    manager.RecordGoal(Console.ReadLine());
                    break;

                case "3":
                    manager.ShowGoals();
                    break;

                case "4":
                    manager.ShowScore();
                    break;

                case "5":
                    manager.SaveProgress();
                    return;

                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }
        }
    }
}
