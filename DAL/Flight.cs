using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Flight
    {
        public long flightID { get; set; }
        public PlaneType planeType { get; set; }
        public DateTime date { get; set; }
        public String departure { get; set; }
        public String destination { get; set; }
        public int freeSeats { get; set; }
        public string status { get; set; }        
    }
}
