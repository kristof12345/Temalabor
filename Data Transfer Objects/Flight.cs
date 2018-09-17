using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    class Flight
    {
        private DateTime date;
        private String departure;
        private String destination;
        private List<Seat> seats = new List<Seat>();

        public Flight(int numSeats)
        {
            for (int i = 0; i < numSeats; i++)
                seats.Add(new Seat());
        }

        public DateTime Date { get => date; set => date = value; }
        public string Departure { get => departure; set => departure = value; }
        public string Destination { get => destination; set => destination = value; }

        //Szabad helyek száma
        public int FreeSeats()
        {
            int ret = 0;
            for (int i = 0; i < seats.Count; i++) if (!seats[i].IsReserved) ret++;
            return ret;
        }
    }
}
