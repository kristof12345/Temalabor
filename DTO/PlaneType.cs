using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DTO
{
    public class PlaneType
    {
        //Konstruktor
        public PlaneType(String name, long id)
        {
            if (name != null)
                PlaneTypeName = name;
            else
                PlaneTypeName = "";
            if (id != null)
                PlaneTypeID = id;
            else
                PlaneTypeID = -1;
            Seats = new List<Seat>();

            //TODO: Ide majd az adatbázis alapján kell valami
            if (name != null && name.Contains("Airbus"))
            {
                for (int i = 0; i < 7; i++)
                {
                    Seat s = new Seat(i);
                    s.Coordinates = new Cord(640, 50 + 50 * i);
                    Seats.Add(s);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Seat s = new Seat(i);
                    s.Coordinates = new Cord(640, 50 + 50 * i);
                    Seats.Add(s);
                }
            }
        }

        public PlaneType()
        {
            PlaneTypeName = "";
            PlaneTypeID = 0;
            Seats = new List<Seat>();
        }
        //A repülő azonosítója
        public long PlaneTypeID { get; set; }

        //A repülő típus neve (pl: "Airbus A380")
        public string PlaneTypeName { get; set; }

        //A székek tömbje
        public List<Seat> Seats { get; set; }

        //Az összes szék száma
        public int TotalSeatsCount
        {
            get
            {
                if (Seats == null) return -1;
                return Seats.Count;
            }
        }

        public String SeatsCount
        {
            get
            {
                if (Seats == null) return -1 + " seats";
                return Seats.Count + " seats";
            }
        }

        //A szabad székek száma
        public int FreeSeatsCount
        {
            get
            {
                if (Seats == null) return -1;

                int ret = 0;
                foreach (Seat s in Seats)
                {
                    if (!s.Reserved) ret++;
                }
                return ret;
            }
        }

        //Szék lefoglalása
        public bool ReserveSeat(int id)
        {
            if (Seats[id].Reserved == true) return false; //Ha már foglalt, akkor nem sikerül

            Seats[id].Reserved = true;
            return true;
        }

        //Szék elkérése ID alapján
        public Seat GetSeat(int id)
        {
            return Seats[id];
        }

        public override string ToString()
        {
            return PlaneTypeID.ToString() + " " + PlaneTypeName.ToString() + " " + TotalSeatsCount + " " + FreeSeatsCount;
        }

        //Statikus adatok
        private static String[] typesArray;

        //Repülőtípusok száma
        public static int NumberOfTypes
        {
            get { return typesArray.Length; }
        }

        //Repülőtípusok betöltése
        public static void Initialize(string[] strArray)
        {
            typesArray = strArray;
        }

        //Combo box feltöltése
        public static object CreateComboBox()
        {
            return typesArray;
        }
    }
}
