using CabInvoiceGenerator;
using NUnit.Framework;
using System.Collections.Generic;

namespace CabInvoiceGenTest
{
    public class Tests
    {
        InvoiceGenerator invoiceGenerator;
        Ride ride;
        Ride[] rides;
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
            double fare = invoiceGenerator.CalculateFare(distance, time);

            Assert.AreEqual(160.0d, fare);
        }
        [Test]
        public void Given_InvalidDistance_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = -10; //in km
                int time = 60;   //in minutes
                double fare = invoiceGenerator.CalculateFare(distance, time);
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
                double distance = 10; //in km
                int time = -20;   //in minutes
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException exception)
            {
                Assert.AreEqual(exception.type, CabInvoiceException.ExceptionType.INVALID_TIME);
            }
        }
        [Test]
        public void Given_MultipleNoOfRides_Should_Return_TotalFare()
        {

            rides = new Ride[] { new Ride(10, 60), new Ride(5, 30), new Ride(3, 20) };
            invoiceGenerator = new InvoiceGenerator();
            double fare = invoiceGenerator.CalculateFare(rides);

            Assert.AreEqual(290, fare);

        }
        [Test]
        public void Given_ZeroRides_Should_Return_CabInvoiceException()
        {
            try
            {
                rides = new Ride[] { };
                invoiceGenerator = new InvoiceGenerator();
                double fare = invoiceGenerator.CalculateFare(rides);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(CabInvoiceException.ExceptionType.NULL_RIDES, ex.type);
            }
        }
        [Test]
        public void Given_ValidData_Should_Return_EnhancedCabInvoiceSummary()
        {
            rides = new Ride[] { new Ride(10, 60), new Ride(5, 30), new Ride(3, 20) };
            invoiceGenerator = new InvoiceGenerator();

            double fare = invoiceGenerator.CalculateFare(rides);
            double average = fare / rides.Length;
            InvoiceSummary data = new InvoiceSummary(rides.Length, fare);

            Assert.AreEqual(data.numberOfRides, rides.Length);
            Assert.AreEqual(data.averageFare, average);
            Assert.AreEqual(data.totalFare, fare);
        }
    }
}