﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DTO
{
    public class Flight_DTO
    {
        //Konstruktor
        public Flight_DTO(String type, long typeId)
        {
            PlaneType = new PlaneType(type, typeId);
            PlaneTypeName = type;
            PlaneTypeID = typeId;
        }

        //PlaneType neve
        public String PlaneTypeName { get; set; }

        //PlaneType ID-ja
        public long PlaneTypeID { get; set; }

        //Egyedi azonosító
        public long FlightId { get; set; }

        //Dátum
        public DateTime Date { get; set; }

        //Indulás helye
        public string Departure { get; set; }

        //Érkezés helye
        public string Destination { get; set; }

        //Repülő típusa, tartalmazza a székeket
        public PlaneType PlaneType { get; set; }

        //A járat státusza (pl: Cancelled, Sceduled, Delayed)
        public string Status { get; set; }

        //A normál jegyek ára
        public int NormalPrice { get; set; }

        //az első osztályú jegyek ára
        public int FirstClassPrice { get; set; }

        //A székek száma
        public int NumberOfSeats
        {
            get
            {
                if (PlaneType == null) return -1;
                return PlaneType.TotalSeatsCount;
            }
        }

        //A szabad székek száma
        public int FreeSeats
        {
            get
            {
                if (PlaneType == null) return -1;
                return PlaneType.FreeSeatsCount;
            }
        }

        //Kiíráshoz ToStirng
        public override string ToString()
        {
            return "id: " + FlightId.ToString() + " from: " + Departure + " to: " + Destination + " type: " + PlaneType.PlaneTypeName;
        }
    }
}
