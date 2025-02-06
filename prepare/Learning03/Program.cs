using System;

class Program
{
    static void Main(string[] args)
    {
               // Create fraction objects
        Fraction fraction1 = new Fraction();       // 1/1
        Fraction fraction2 = new Fraction(5);      // 5/1
        Fraction fraction3 = new Fraction(3, 4);   // 3/4
        Fraction fraction4 = new Fraction(1, 3);   // 1/3

        // Print fraction representations
        Console.WriteLine(fraction1.GetFractionString());  // 1/1
        Console.WriteLine(fraction1.GetDecimalValue());    // 1.0

        Console.WriteLine(fraction2.GetFractionString());  // 5/1
        Console.WriteLine(fraction2.GetDecimalValue());    // 5.0

        Console.WriteLine(fraction3.GetFractionString());  // 3/4
        Console.WriteLine(fraction3.GetDecimalValue());    // 0.75

        Console.WriteLine(fraction4.GetFractionString());  // 1/3
        Console.WriteLine(fraction4.GetDecimalValue());    // 0.333...
    }
}