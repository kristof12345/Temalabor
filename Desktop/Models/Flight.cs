using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DTO;

namespace Desktop.Models
{
    public class Flight : INotifyPropertyChanged
    {
        private List<Seat_DTO> seats;
        private int freeSeats;

        public event PropertyChangedEventHandler PropertyChanged;

        public Flight(int numOfSeats)
        {
            seats = new List<Seat_DTO>(numOfSeats);

            for (int i = 0; i < numOfSeats; i++)
            {
                var temp = new Seat_DTO(i);
                //Csak hogy értelmes helyen legyenek
                temp.Coordinates.X = 100+i * 50;
                temp.Coordinates.Y = 610;
                seats.Add(temp);
            }

            freeSeats = numOfSeats;
        }

        public long FlightId { get; set; }

        public DateTime Date { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public string PlaneType { get; set; }

        public string Status { get; set; }

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
            return FlightId.ToString() + " " + PlaneType + " " + FreeSeats;
        }

        public void ReserveSeat(int id)
        {
            //Ha még nem foglalt, akkor lefoglaljuk
            if (seats[id].Reserved == false)
            {
                seats[id].Reserved = true;
                freeSeats--;
                PropertyChanged(this, new PropertyChangedEventArgs("FreeSeats"));
            }
        }

        public Seat_DTO GetSeat(int id)
        {
            return seats[id];
        } 
    }
}
