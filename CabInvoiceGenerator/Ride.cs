using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class Ride
    {
        //Variables.
        public double distance;
        public int time;

        public Ride()
        {
        }

        /// <summary>
        /// Parameter Constructor For Setting Data.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        public Ride(double distance, int time)
        {
            this.distance = distance;
            this.time = time;
        }
    }

}
