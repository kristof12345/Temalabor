using System;
using System.Collections.Generic;

namespace DTO
{
    public class Flight_DTO
    {
        private List<Seat> seats;

        public Flight_DTO(int numOfSeats)
        {
            seats = new List<Seat>(numOfSeats);

            for (int i = 0; i < numOfSeats; i++)
            {
                seats.Add(new Seat(i));
            }

            FreeSeats = numOfSeats;
            PlaneType = new PlaneType("Airbus A380");
        }


        public long FlightId { get; set; }

        public long BusinessId { get; set; }

        public DateTime Date { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public PlaneType PlaneType { get; set; }

        public long PlaneTypeID { get; set; }

        public string Status { get; set; } //Ez lehet, hogy enumként jobb lenne
        public List<Seat> Seats { get; set; }

        public int NumberOfSeats { get; set; }

        public int FreeSeats { get; set; }

        public override string ToString()
        {
            return "id: " + FlightId.ToString() + " from: " + Departure + " to: " + Destination + " type: " + PlaneType.PlaneTypeName;
        }
    }
}
