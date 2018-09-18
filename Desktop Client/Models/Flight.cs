using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop_Client.Models
{
    public class Flight : INotifyPropertyChanged
    {
        private List<String> seats;
        private int freeSeats;

        public event PropertyChangedEventHandler PropertyChanged;

        public Flight(int numOfSeats)
        {
            seats = new List<string>(numOfSeats);

            for (int i=0; i<numOfSeats; i++)
            {
                seats.Add("a");
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
            return FlightId.ToString() +" "+ PlaneType;
        }

        public void ReserveSeat(int id)
        {
            seats[id] = "b";
            freeSeats--;
            NotifyPropertyChanged();
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged
        private void NotifyPropertyChanged([CallerMemberName] String propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
