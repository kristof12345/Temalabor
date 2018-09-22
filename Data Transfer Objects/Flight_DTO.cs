using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    public class Flight_DTO
    {
        private List<Seat_DTO> seats;
        private int freeSeats;

        public Flight_DTO(int numOfSeats)
        {
            seats = new List<Seat_DTO>(numOfSeats);

            for (int i = 0; i < numOfSeats; i++)
            {
                seats.Add(new Seat_DTO(i));
            }

            freeSeats = numOfSeats;
        }

        public long FlightId { get; set; }

        public DateTime Date { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public string PlaneType { get; set; } //Ez lehet, hogy enumként jobb lenne

        public string Status { get; set; } //Ez lehet, hogy enumként jobb lenne

        public int NumberOfSeats
        {
            get { return seats.Count; }
        }

        public int FreeSeats
        {
            get { return freeSeats; }
        }

        public override string ToString()
        {
            return FlightId.ToString() + " " + PlaneType;
        }
    }
}
