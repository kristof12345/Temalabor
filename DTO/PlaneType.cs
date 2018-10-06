using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PlaneType
    {
        //Konstruktor
        public PlaneType(string type)
        {
            PlaneTypeName = type;
            Seats = new List<Seat>();

            //TODO: Ide majd az adatbázis alapján kell valami
            if(type.Equals("Airbus A380"))
            {
                for(int i=1; i<7; i++)
                    Seats.Add(new Seat(i));
            }
            else
            {
                for (int i = 1; i < 5; i++)
                    Seats.Add(new Seat(i));
            }
        }

        //public long PlaneTypeID { get; set; } //Ez nem kell, csak az adatbázisbeli azonosításra. Ezt a kliens nem állítja.

        //A repülő típus neve (pl: "Airbus A380")
        public string PlaneTypeName { get; set; }

        //A székek tömbje
        public List<Seat> Seats { get; set; }

        //Az összes szék száma
        public int GetTotalSeatsCount()
        {
            return Seats.Count;
        }

        //A szabad székek száma
        public int GetFreeSeatsCount()
        {
            int ret = 0;
            foreach(Seat s in Seats)
            {
                if (!s.Reserved) ret++;
            }
            return ret;
        }

        //Combo box feltöltése
        public static object CreateComboBox()
        {
            //TODO: Adatbázisból kell lekérdezni
            string[] strArray =
                {
                "Airbus A380",
                "Boeing 747",
                "Boeing 777",
                "Antonov 124",
                //További repülő típusok
            };
            return strArray;
        }
    }
}
