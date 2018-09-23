using System;

namespace DTO
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
