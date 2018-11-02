using System;

namespace DTO
{
    public class Cord
    {
        public Cord(int x, int y){ X = x; Y = y; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public enum SeatType
    {
        Normal,
        FirstClass
    }

    public class Seat
    {
        private Cord c;

        public Seat()
        {
            SeatType = SeatType.Normal;
        }

        public Seat(long id) { SeatId = id; }

        public long SeatId { get; set; }

        public long PlaneTypeId { get; set; }

        public SeatType SeatType { get; set; }   
        
        public bool Reserved { get; set; }

        public Cord Coordinates { get { return c; } set { c = value; } }
    }
}
