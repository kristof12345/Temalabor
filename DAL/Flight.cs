using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Flight
    {
        public long flightID { get; set; }
        public long planeTypeID { get; set; }
        public DateTime date { get; set; }
        public String departure { get; set; }
        public String destination { get; set; }
        public string status { get; set; }
        public int normalPrice { get; set; }
        public int firstClassPrice { get; set; }
        public bool isDeleted { get; set; }
    }
}
