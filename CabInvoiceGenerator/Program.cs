using System;

namespace CabInvoiceGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Cab Invoice Generator");
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            double fare = invoiceGenerator.CalculateFare(2.0, 5);
            Console.WriteLine($"Fare : {fare}");
        }
    }
}
