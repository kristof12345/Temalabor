using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Desktop.Services;
using DTO;

namespace Desktop.Models
{
    public class Flight : INotifyPropertyChanged
    {
        private List<Seat_DTO> seats;
        private int freeSeats;

        public event PropertyChangedEventHandler PropertyChanged;

        public Flight(long id, int numOfSeats)
        {
            FlightId = id;
            seats = new List<Seat_DTO>(numOfSeats);

            freeSeats = numOfSeats;

            for (int i = 0; i < numOfSeats; i++)
            {
                var temp = new Seat_DTO(i);

                //Csak hogy értelmes helyen legyenek
                if ((i % 2) == 0) { temp.Coordinates.X = 350; } //Vízszintes érték (balról)
                else { temp.Coordinates.X = 330; }

                //Csak, hogy legyen néhány foglalt hely is
                if (i % 3 == 0)
                {
                    temp.Reserved = true;
                    freeSeats--;
                } 

                temp.Price = 100; //Ára is legyen

                temp.Coordinates.Y = 100+50*i; //Függőleges érték (felülről)
                seats.Add(temp);
            } 
        }

        public long FlightId { get; private set; }

        public DateTime Date { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public string PlaneType { get; set; }

        internal Flight_DTO ToDTO()
        {
            Flight_DTO ret = new Flight_DTO(this.NumberOfSeats);
            ret.FlightId = this.FlightId;
            ret.Departure = this.Departure;
            ret.Destination = this.Destination;
            ret.Date = this.Date;
            ret.PlaneType = this.PlaneType;
            ret.Status = this.Status;

            return ret;
        }

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
            return "Flight "+ FlightId.ToString() + " from " + Departure + " to " + Destination;
        }

        public void ReserveSeat(int id)
        {
            //Ha még nem foglalt, akkor lefoglaljuk
            if (seats[id].Reserved == false)
            {
                seats[id].Reserved = true;
                freeSeats--;
                PropertyChanged(this, new PropertyChangedEventArgs("FreeSeats")); //Értesítés a változásról
                HttpService.PostReservationAsync(new ReserveSeat_DTO(FlightId,id)); //Http kérés a foglaláshoz
            }
        }

        public Seat_DTO GetSeat(int id)
        {
            return seats[id];
        } 
    }
}
