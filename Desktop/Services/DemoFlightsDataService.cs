using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    public static class FlightsDataService
    {
        private static ObservableCollection<Flight> flightList;

        public static ObservableCollection<Flight> FlightList
        {
            get
            {
                if (flightList == null) {
                    flightList = new ObservableCollection<Flight>();

                    Flight f1 = new Flight(1, new DateTime(2018, 5, 1), "London", "Budapest", "Airbus A380", 1, "Ok");
                    f1.NormalPrice = 100;
                    f1.FirstClassPrice = 1000;
                    flightList.Add(f1);
                    
                    Flight f2 = new Flight(2, new DateTime(2019, 1, 2), "New York", "Budapest", "Airbus A270", 1, "Cancelled");
                    f2.NormalPrice = 200;
                    f2.FirstClassPrice = 2000;
                    flightList.Add(f2);
                }
                return flightList;
            }
        }

        public static long MaxId
        {
            get
            {
                long max = 0;
                foreach (Flight f in flightList) { if (f.FlightId >= max) max = f.FlightId; }
                return max;
            }
        }

        //Kapcsolat inicializálása
        public static async Task Initialize()
        {
            List<String> strArray = new List<string>();
            //strArray = await ListPlaneTypesAsync();

            strArray.Add("Boeing 777");
            strArray.Add("Airbus A380");
            
            PlaneType.Initialize(strArray.ToArray());
        }

        //A járatok letöltése a szerverről
        private static async void ReloadFlightListAsync()
        {
            var last = flightList[flightList.Count - 1];
            flightList.Remove(last);
            flightList.Add(last);
        }

        //Járat hozzáadása
        public static async void AddFlightAsync(long id, DateTime date, String dep, String dest, String type, long typeId)
        {
            var f = new Flight(id, type, typeId)
            {
                Date = date,
                Departure = dep,
                Destination = dest,
                //PlaneType = type,
                Status = "Sceduled",
            };

            //Http kérés kiadása
            //await HttpService.AddFlightAsync(f.ToDTO());
            flightList.Add(f);

            //Táblázat frissítése
            //ReloadFlightListAsync();
        }

        //Járat hozzáadása 2
        public static async void AddFlightAsync(Flight f)
        {
            //Http kérés kiadása
            //await HttpService.AddFlightAsync(f.ToDTO());
            flightList.Add(f);

            //Táblázat frissítése
            //ReloadFlightListAsync();
        }

        //Járat törlése
        public static async void DeleteFlightAsync(Flight f)
        {
            //Http kérés kiadása
            //await HttpService.DeleteFlightAsync(f.ToDTO());
            flightList.Remove(f);

            //Táblázat frissítése
            //ReloadFlightListAsync();
        }

        //Járat módosítása
        public static async void UpdateFlightAsync(Flight f)
        {
            //Http kérés kiadása
            //await HttpService.UpdateFlightAsync(f.ToDTO());
            
            //Táblázat frissítése
            ReloadFlightListAsync();
        }

        //Repülő képek URI-je
        internal static Uri LoadImageUri(long planeTypeID)
        {
        return new Uri("https://image.ibb.co/gOHBVf/Antonov124white.png");
        }
    }
}
