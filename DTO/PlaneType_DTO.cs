using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PlaneType
    {
        public PlaneType(string type) { PlaneTypeName = type; }

        public long PlaneTypeID { get; set; }
        public string PlaneTypeName { get; set; }
        //public List<Seat> Seats { get; set; }
    }
}
