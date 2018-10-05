using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class Seat
    {
        public long seatID { get; set; }
        public long planeTypeID { get; set; }
        public PlaneType planeType { get; set; }
        public bool IsReserved { get; set; }
        public String seatType { get; set; }
        public int price { get; set; }
        public int Xcord { get; set; }
        public int Ycord { get; set; }
    }
}

