
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
        public Seat(long id) { SeatId = id; }

        public long SeatId { get; set; }

        public long PlaneTypeId { get; set; } //Ez kell?

        public SeatType SeatType { get; set; } = SeatType.Normal;

        public bool Reserved { get; set; }

        public Cord Coordinates { get; set; }
    }
}
