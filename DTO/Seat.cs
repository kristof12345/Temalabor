using System;

namespace DTO
{
    public class Cord
    {

        public Cord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Seat
    {
        private Cord c;

        public Seat()
        {
        }

        public Seat(long id) { SeatId = id; }

        public long SeatId { get; set; }
        public String SeatType { get; set; }
        public int Price { get; set; }
        public bool Reserved { get; set; }
        public Cord Coordinates { get { return c; } set { c = value; } }
    }
}
