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
        private PlaneType planeType;

        public event PropertyChangedEventHandler PropertyChanged;

        //Konstruktor
        public Flight(long id=0, String type="Airbus A380")
        {
            FlightId = id;
            planeType = new PlaneType(type);
        }

        /*private void GenerateSeats(int numOfSeats)
        {
            seats = new List<Seat>(numOfSeats);

            freeSeats = numOfSeats;

            for (int i = 0; i < numOfSeats; i++)
            {
                var temp = new Seat(i);

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

                temp.Coordinates.Y = 100 + 50 * i; //Függőleges érték (felülről)
                seats.Add(temp);
            }
        }*/

        //Konstruktor DTO-ból
        public Flight(Flight_DTO dto)
        {
            this.FromDTO(dto);
        }

        //Konstruktor teljes paramétrlistával
        public Flight(long id, DateTime date, string dep, string des, string type, string stat)
        {
            FlightId = id;
            Date = date;
            Departure = dep;
            Destination = des;
            PlaneType = type;
            Status = stat;
        }

        //Egyedi azonosító a kliensben
        public long FlightId { get; private set; }

        //Dátum
        public DateTime Date { get; set; }

        //Indulás helye
        public string Departure { get; set; }

        //Érkezés helye
        public string Destination { get; set; }

        //Repülő típusa, tartalmazza a székeket
        public string PlaneType
        {
            get { return planeType.PlaneTypeName; }
            set { planeType = new PlaneType(value); }
        }

        //A járat státusza (pl: Cancelled, Sceduled, Delayed)
        public string Status { get; set; }

        //A székek száma
        public int NumberOfSeats
        {
            get { return planeType.GetTotalSeatsCount(); }
        }

        public int FreeSeats
        {
            get { return planeType.GetFreeSeatsCount(); }
        }

        //A szabad székek száma
        public override string ToString()
        {
            return "Flight "+ FlightId.ToString() + " from " + Departure + " to " + Destination;
        }

        //Egy szék lefoglalása
        public void ReserveSeat(int id)
        {
            //Ha még nem foglalt, akkor lefoglaljuk
            if (planeType.ReserveSeat(id))
            {
                PropertyChanged(this, new PropertyChangedEventArgs("FreeSeats")); //Értesítés a változásról
                HttpService.PostReservationAsync(new ReserveSeat_DTO(FlightId, id)); //Http kérés a foglaláshoz
            }
        }


        //Szék elkérése ID alapján
        public Seat GetSeat(int id)
        {
            return planeType.GetSeat(id);
        }

        //Átalakítás DTO-ba
        internal Flight_DTO ToDTO()
        {
            Flight_DTO ret = new Flight_DTO(this.PlaneType);

            ret.FlightId = this.FlightId;
            ret.Date = this.Date;
            ret.Departure = this.Departure;
            ret.Destination = this.Destination;
            ret.PlaneType = this.planeType;        
            ret.Status = this.Status;

            return ret;
        }

        //Átalakítás DTO-ból
        internal void FromDTO(Flight_DTO dto)
        {
            this.FlightId = dto.FlightId;
            this.Date = dto.Date;
            this.Departure = dto.Departure;
            this.Destination = dto.Destination;
            this.planeType = dto.PlaneType;
            this.Status = dto.Status;
        }

        //Flight másolása (kb. copy construktor)
        internal Flight Copy()
        {
            var copied = new Flight(FlightId);

            copied.FlightId = FlightId;
            copied.Date = Date;
            copied.Departure = Departure;
            copied.Destination = Destination;
            copied.planeType = planeType;
            copied.Status = Status;
            
            return copied;
        }
    }
}
