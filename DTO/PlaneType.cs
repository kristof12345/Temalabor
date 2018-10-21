using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DTO
{
    public class PlaneType
    {
        private static String[] typesArray;

        //Konstruktor
        [JsonConstructor]
        public PlaneType(String name, long id)
        {
            PlaneTypeName = name;
            PlaneTypeID = id;
            Seats = new List<Seat>();

            //TODO: Ide majd az adatbázis alapján kell valami
            for (int i = 0; i < 7; i++)
            {
                Seat s = new Seat(i);
                s.Coordinates = new Cord(640, 50+50*i);
                Seats.Add(s);
            }
        }

        //Konstruktor új repülő típushoz
        public PlaneType(String name, List<Seat> list)
        {
            PlaneTypeName = name;
            Seats = list;
        }

        //A repülő azonosítója
        public long PlaneTypeID { get; set; }

        //A repülő típus neve (pl: "Airbus A380")
        public string PlaneTypeName { get; private set; }

        //A székek tömbje
        private List<Seat> Seats { get; set; }

        //Az összes szék száma
        public int GetTotalSeatsCount()
        {
            if (Seats == null) return -1;
            return Seats.Count;
        }

        //A szabad székek száma
        public int GetFreeSeatsCount()
        {
            if (Seats == null) return -1;

            int ret = 0;
            foreach(Seat s in Seats)
            {
                if (!s.Reserved) ret++;
            }
            return ret;
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
