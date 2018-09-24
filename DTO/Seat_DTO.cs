using System;

namespace DTO
{
    public class Cord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Seat_DTO
    {
        private Cord c = new Cord();
        public Seat_DTO(long id) { SeatId = id; }

        public long SeatId { get; set; }
        public String SeatType { get; set; }
        public int Price { get; set; }
        public bool Reserved { get; set; }
        public Cord Coordinates { get { return c; } set { c = value; } }
    }
}
