using System;
using System.Collections.Generic;

// Program 1: Abstraction with YouTube Videos

// Class to represent a comment
public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string name, string text)
    {
        CommenterName = name;
        CommentText = text;
    }
}

// Class to represent a YouTube video
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public List<Comment> GetComments()
    {
        return comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("C# Basics Tutorial", "CodeAcademy", 600);
        video1.AddComment(new Comment("Alice", "Great tutorial, very clear!"));
        video1.AddComment(new Comment("Bob", "Thanks! This really helped me."));
        video1.AddComment(new Comment("Charlie", "Can you make one for interfaces?"));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Intro to Unity Game Dev", "GameDev101", 1200);
        video2.AddComment(new Comment("Daisy", "Love your content!"));
        video2.AddComment(new Comment("Ethan", "What version of Unity is this?"));
        video2.AddComment(new Comment("Fay", "Waiting for part 2."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Understanding Abstraction", "TechWithTim", 800);
        video3.AddComment(new Comment("George", "This made abstraction click for me."));
        video3.AddComment(new Comment("Hannah", "Very useful explanation."));
        video3.AddComment(new Comment("Ian", "Awesome, thank you!"));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("How the Internet Works", "TechExplained", 950);
        video4.AddComment(new Comment("Jack", "This was great, thanks."));
        video4.AddComment(new Comment("Kara", "Can you do one on TCP/IP?"));
        video4.AddComment(new Comment("Liam", "Very detailed and clear."));
        videos.Add(video4);

        // Display each video and its comments
        foreach (Video video in videos)
        {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine("Author: " + video.Author);
            Console.WriteLine("Length (seconds): " + video.LengthInSeconds);
            Console.WriteLine("Number of comments: " + video.GetCommentCount());
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.CommenterName}: {comment.CommentText}");
            }

            Console.WriteLine(new string('-', 40)); // separator
        }
    }
}


// Program 2: Encapsulation with Online Ordering

// Address class
public class Address
{
    //store address, street, city, state, country of customers
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    // tracks international addresses
    {
        return country.Trim().ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// Customer class
// stores customer information 
public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }

    public bool IsInUSA()
    {
        return address.IsInUSA();
    }
}

// Product class
// contains product name, ID, price, amount
public class Product
{
    private string name;
    private string productId;
    private double pricePerUnit;
    private int quantity;

    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        // quanitity x price = total
        return pricePerUnit * quantity;
    }

    public string GetPackingLabel()
    {
        return $"Product: {name}, ID: {productId}";
    }
}

// Order class
public class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalCost()
    {
        double productTotal = 0;
        foreach (Product product in products)
        {
            productTotal += product.GetTotalCost();
        }

        double shipping = customer.IsInUSA() ? 5.0 : 35.0;
        return productTotal + shipping;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in products)
        {
            label += product.GetPackingLabel() + "\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Customer 1 (USA)
        Address addr1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Customer cust1 = new Customer("John Smith", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Notebook", "N123", 2.50, 4));
        order1.AddProduct(new Product("Pen Pack", "P456", 1.20, 3));

        // Customer 2 (International)
        Address addr2 = new Address("45 Maple Rd", "Toronto", "ON", "Canada");
        Customer cust2 = new Customer("Emily Wong", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Headphones", "H789", 29.99, 1));
        order2.AddProduct(new Product("USB Cable", "U101", 5.75, 2));

        // Display Order 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}");
        Console.WriteLine(new string('-', 40));

        // Display Order 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}");
    }
}

// Program 3: Inheritance with Event Planning