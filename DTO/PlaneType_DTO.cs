using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PlaneType_DTO
    {
        public PlaneType_DTO(string type) { PlaneType = type; }
        public long PlaneTypeID { get; set; }
        public string PlaneType { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
