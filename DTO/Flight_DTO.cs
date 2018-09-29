using System;
using System.Collections.Generic;

namespace DTO
{
    public class Flight_DTO
    {
        private List<Seat_DTO> seats;

        public Flight_DTO(int numOfSeats)
        {
            seats = new List<Seat_DTO>(numOfSeats);

            for (int i = 0; i < numOfSeats; i++)
            {
                seats.Add(new Seat_DTO(i));
            }

            FreeSeats = numOfSeats;
        }


        public long FlightId { get; set; }

        public DateTime Date { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public string PlaneType { get; set; } //Ez lehet, hogy enumként jobb lenne

        public string Status { get; set; } //Ez lehet, hogy enumként jobb lenne
        public List<Seat_DTO> Seats { get; set; }

        public int NumberOfSeats
        {
            get { return seats.Count; }
        }

        public int FreeSeats { get; set; }

        public override string ToString()
        {
            return FlightId.ToString() + " " + PlaneType;
        }
    }
}
