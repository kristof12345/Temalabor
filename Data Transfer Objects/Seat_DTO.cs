using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Csak teszt komment

namespace Data_Transfer_Objects
{
    public class Seat_DTO
    {
        public Seat_DTO(long id) { SeatId = id; }
        public long SeatId { get; set; }
        public String SeatType { get; set; }
        public int Price { get; set; }
        public bool Reserved { get; set; }
    }
}
