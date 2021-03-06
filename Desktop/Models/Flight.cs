﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using Desktop.Services;
using DTO;

namespace Desktop.Models
{
    public class Flight
    {
        //A járat repülőjének típusa
        internal PlaneType PlaneType { get; set; }

        //Konstruktor
        public Flight(long id, String type, long typeId)
        {
            FlightId = id;

            if(type!=null) PlaneType = new PlaneType(type, typeId);
        }

        //Konstruktor teljes paramétrlistával
        public Flight(long id, DateTime date, string dep, string des, string type, long typeId, string stat, int normalPrice, int premiumPrice)
        {
            FlightId = id;
            Date = date;
            Departure = dep;
            Destination = des;
            PlaneType = new PlaneType(type, typeId);
            Status = stat;
            NormalPrice = normalPrice;
            FirstClassPrice = premiumPrice;
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
        public String PlaneTypeName
        {
            get { return PlaneType.PlaneTypeName; }

        }

        //A járat státusza (pl: Cancelled, Sceduled, Delayed)
        public string Status { get; set; }

        //A székek száma
        public int NumberOfSeats
        {
            get { return PlaneType.TotalSeatsCount; }
        }

        public int FreeSeats
        {
            get { return PlaneType.FreeSeatsCount; }
        }

        //A szabad székek száma
        public override string ToString()
        {
            return "Flight "+ FlightId.ToString() + " from " + Departure + " to " + Destination;
        }

        //Szék elkérése ID alapján
        public Seat GetSeat(int id)
        {
            return PlaneType.GetSeat(id);
        }

        public int NormalPrice { get; set; }

        public int FirstClassPrice { get; set; }

        //Átalakítás DTO-ba
        internal Flight_DTO ToDTO()
        {
            Flight_DTO ret = new Flight_DTO(this.PlaneTypeName, this.PlaneType.PlaneTypeID);

            ret.FlightId = this.FlightId;
            ret.Date = this.Date;
            ret.Departure = this.Departure;
            ret.Destination = this.Destination;
            ret.PlaneTypeName = this.PlaneTypeName;
            ret.PlaneType = this.PlaneType;
            ret.Status = this.Status;
            ret.FirstClassPrice = this.FirstClassPrice;
            ret.NormalPrice = this.NormalPrice;

            return ret;
        }

        //Átalakítás DTO-ból
        internal void FromDTO(Flight_DTO dto)
        {
            this.FlightId = dto.FlightId;
            this.Date = dto.Date;
            this.Departure = dto.Departure;
            this.Destination = dto.Destination;
            this.PlaneType = dto.PlaneType;
            this.Status = dto.Status;
            this.NormalPrice = dto.NormalPrice;
            this.FirstClassPrice = dto.FirstClassPrice;
        }

        //Flight másolása (kb. copy construktor)
        internal Flight Copy()
        {
            var copied = new Flight(FlightId, null, PlaneType.PlaneTypeID);

            copied.Date = Date;
            copied.Departure = Departure;
            copied.Destination = Destination;
            copied.PlaneType = PlaneType;
            copied.Status = Status;
            copied.NormalPrice = NormalPrice;
            copied.FirstClassPrice = FirstClassPrice;
            
            return copied;
        }
    }
}
