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

            PlaneTypeID = id;
            Seats = new List<Seat>();

            //TODO: Ide majd az adatbázis alapján kell valami
            /*
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
            }*/
        }

        //A repülő típus azonosítója
        public long PlaneTypeID { get; set; }

        //A repülő típus neve (pl: "Airbus A380")
        public string PlaneTypeName { get; set; }

        //A székek tömbje
        public List<Seat> Seats { get; set; }

        //Az összes szék száma
        public int TotalSeatsCount { get { return Seats.Count; } }

        public String SeatsCount { get { return Seats.Count + " seats"; } }

        //A szabad székek száma
        public int FreeSeatsCount
        {
            get
            {
                int ret = 0;
                foreach (Seat s in Seats)
                {
                    if (!s.Reserved) ret++;
                }
                return ret;
            }
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
