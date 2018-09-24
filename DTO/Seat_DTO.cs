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
        public Seat_DTO(long id) { SeatId = id; }
        public Seat_DTO(long id, int x, int y)
        {
            SeatId = id;
            Coordinates.X = x;
            Coordinates.Y = y;
        }
        public long SeatId { get; set; }
        public String SeatType { get; set; }
        public int Price { get; set; }
        public bool Reserved { get; set; }
        public Cord Coordinates { get; set; }
    }
}
