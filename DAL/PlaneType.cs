using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class PlaneType
    {
        public long planeTypeID { get; set; }
        public string planeType { get; set; }
        public List<Seat> seats { get; set; }
    }
}
