using CabInvoiceGenerator;
using NUnit.Framework;

namespace CabInvoiceGenTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator;
        Ride ride;
        [SetUp]
        public void Setup()
        {
            ride = new Ride();
            invoiceGenerator = new InvoiceGenerator();
        }
        [Test]
        public void Given_Valid_DistanceAndTime_Should_Return_CabFare()
        {
            double distance = 10; // in Km
            int time = 60;  // in minutes
            double fare = invoiceGenerator.CalculateFare(distance,time);

            Assert.AreEqual(160.0d, fare);
        }
        [Test]
        public void Given_InvalidDistance_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = -5; //in km
                int time = 20;   //in minutes
                double fare = invoiceGenerator.CalculateFare(distance,time);
            }
            catch (CabInvoiceException exception)
            {
                Assert.AreEqual(exception.type, CabInvoiceException.ExceptionType.INVALID_DISTANCE);
            }
        }
        [Test]
        public void Given_InvalidTime_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = 5; //in km
                int time = -20;   //in minutes
                double fare = invoiceGenerator.CalculateFare(distance,time);
            }
            catch (CabInvoiceException exception)
            {
                Assert.AreEqual(exception.type, CabInvoiceException.ExceptionType.INVALID_TIME);
            }
        }
    }
}