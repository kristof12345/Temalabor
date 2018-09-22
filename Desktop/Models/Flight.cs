using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data_Transfer_Objects;

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
                seats.Add(new Seat_DTO(i));
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

        //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged
        //Valahogy ennek kellene értesítenie a listát a változásról, de nem megy
        /*private void PropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
