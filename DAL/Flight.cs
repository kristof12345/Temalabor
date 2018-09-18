using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Flight
    {
        public int flightID { get; set; }
        public DateTime date { get; set; }
        public String departure { get; set; }
        public String destination { get; set; }
        public List<Seat> seats { get; set; }
    }
}
