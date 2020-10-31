using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        //Variable.
        RideType rideType;
        public RideRepository rideRepository;
        Dictionary<string, List<Ride>> userRides = null;

        //Constants.
        private readonly double MINIMUM_COST_PER_KM;
        private readonly int COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        public InvoiceGenerator(RideType rideType)
        {
            this.rideType = rideType;
            this.rideRepository = new RideRepository();
            try
            {
                //If Ride type is Premium Then Rates Set For Premium else For Normal.
                if (rideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (rideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }
        /// Constrcutor To Create RideRepository instance.
        /// </summary>
        /// <summary>
        public InvoiceGenerator()
        {
        }

        /// <summary>
        /// Function to Calculate Fare.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                //Calculsting Total Fare.
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoiceException)
            {
                if (rideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        /// <summary>
        /// Function To Calculate Total Fare and Generating Summary For Multiple Rides.
        /// </summary>
        /// <param name="rides"></param>
        /// <returns></returns>
        public double CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                //Calculating Total Fare For All Rides.
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return totalFare;
        }
        /// <summary>
        /// Function to Add Rides For UserId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rides"></param>
        public void AddRides(string userId, Ride[] rides)
        {
            try
            {
                //Adding Ride To The Specified User.
                rideRepository.AddRide(userId, rides);
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
        }
        /// <summary>
        /// Function To Calculate Total Fare and Generating Summary For Multiple Rides.
        /// </summary>
        /// <param name="rides"></param>
        /// <returns></returns>
        public InvoiceSummary CalculateFareOnGivingUserId(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                //Calculating Total Fare For All Rides.
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }

        /// <summary>
        /// Function To Get Rides List As an Array for specified UserId. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Ride[] GetRides(string userId)
        {
            try
            {
                return this.userRides[userId].ToArray();
            }
            catch (Exception)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid UserID");
            }
        }
    }
}
