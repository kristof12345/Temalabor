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
                if (flightList == null) { flightList = new ObservableCollection<Flight>(); ReloadFlightListAsync(); }
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
            await HttpService.InitializeAsync();
            flightList = new ObservableCollection<Flight>();
            ReloadFlightListAsync();
        }

        //A járatok letöltése a szerverről
        private static async void ReloadFlightListAsync()
        {
            List<Flight_DTO> dtoList = await HttpService.ListFlightsAsync();
            flightList.Clear();
            foreach (Flight_DTO dto in dtoList)
            {
                Flight f = new Flight(dto.FlightId, dto.PlaneTypeName, dto.PlaneTypeID);
                f.FromDTO(dto);
                flightList.Add(f);
            }
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
            await HttpService.AddFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadFlightListAsync();
        }

        //Járat hozzáadása 2
        public static async void AddFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.AddFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadFlightListAsync();
        }

        //Járat törlése
        public static async void DeleteFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.DeleteFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadFlightListAsync();
        }

        //Járat módosítása
        public static async void UpdateFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.UpdateFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadFlightListAsync();
        }
    }
}
