using DTO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace Desktop.Services
{
    public static class HttpService
    {
        private static HttpClient client = new HttpClient();
        private static HttpClientHandler handler = new HttpClientHandler();

        //Config fájlból olvassa be
        private static string baseUri;

        private static string UriFlights;
        private static string UriReservation;
        private static string UriUsers;
        private static string UriTypes;
        private static string UriSeats;

        internal static async void InitializeAsync()
        {
            //Ne változtasd meg, így működik
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            baseUri = System.IO.File.ReadAllText(@"ConfigUri.txt"); //C:\Users\pappkristof\source\repos\Temalabor\Desktop\bin\x86\Debug
            UriFlights = baseUri + "flight/";
            UriReservation = baseUri + "reservation/";
            UriUsers = baseUri + "users/";
            UriTypes = baseUri + "planetype/"; 
            UriSeats = baseUri + "seat/flightID/";

            List<String> strArray = new List<string>();
            strArray = await ListPlaneTypesAsync();
            
            /*
            strArray.Add("Boeing 777");
            strArray.Add("Airbus A380");
            strArray.Add("Boeing 747");
            strArray.Add("Boeing 222");
            strArray.Add("Antonov 124");
            */
            PlaneType.Initialize(strArray.ToArray());
        }

        /// <summary>
        /// Flight műveletek
        /// </summary>

        //Járatok listázása
        internal static async Task<List<Flight_DTO>> ListFlightsAsync()
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriFlights);
            List<Flight_DTO> list = await response.Content.ReadAsAsync<List<Flight_DTO>>();
            foreach (Flight_DTO f in list)
            {
                HttpResponseMessage seatResponse = await client.GetAsync(UriSeats + f.FlightId);
                List<Seat> seatList = await seatResponse.Content.ReadAsAsync<List<Seat>>();
                f.PlaneType = new PlaneType(f.PlaneTypeName, seatList);
            }

            return list;
        }

        //Járat hozzáadása
        internal static async Task AddFlightAsync(Flight_DTO addRequest)
        {
            client = new HttpClient(handler);
            
            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
            var contents = await response.Content.ReadAsStringAsync();
        }

        //Járat törlése
        public static async Task DeleteFlightAsync(Flight_DTO deleteRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.DeleteAsync(UriFlights + deleteRequest.FlightId);
            var contents = await response.Content.ReadAsStringAsync();

        }

        //Járat módosítása
        internal static async Task UpdateFlightAsync(Flight_DTO updateRequest)
        {
            client = new HttpClient(handler);
            Debug.WriteLine("Added: " + updateRequest.PlaneTypeID);
            HttpResponseMessage response = await client.PutAsJsonAsync(UriFlights + updateRequest.FlightId, updateRequest);
            var contents = await response.Content.ReadAsStringAsync();

        }

        /// <summary>
        /// User műveletek
        /// </summary>

        //Felhasználó hozzáadása
        internal static async void AddUserAsync(User addRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriUsers, addRequest);
        }

        //Bejelentkezési kérés
        internal static async Task<bool> LoginAsync(User loginRequest)
        {
            bool contents = false;
            try
            {
                client = new HttpClient(handler);

                HttpResponseMessage response = await client.PutAsJsonAsync(UriUsers, loginRequest);
                contents = await response.Content.ReadAsAsync<bool>();
                Debug.WriteLine("A bejelentkezés eredménye: " + contents);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to login.");
            }
            //return contents;
            return true;
        }

        /// <summary>
        /// PlaneType műveletek
        /// </summary>

        //Repülőtípusok lekérdezése
        internal static async Task<List<String>> ListPlaneTypesAsync()
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriTypes);
            List<PlaneType> typesList = await response.Content.ReadAsAsync<List<PlaneType>>();

            List<String> list = new List<String>();
            for (int i = 1; i < 5; i++) //TODO: 5 helyett list.Count
            {
                HttpResponseMessage typesResponse = await client.GetAsync(UriTypes + i.ToString());
                var t = await typesResponse.Content.ReadAsStringAsync();
                list.Add(t);
            }

            return list;
        }

        //Repülőtípus hozzáadása
        internal static async void AddPlaneTypeAsync(PlaneType addRequest)
        {
            client = new HttpClient(handler);
            Debug.WriteLine("A hozzáadott design neve: " + addRequest.PlaneTypeName);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriTypes, addRequest);
            //var contents = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Reservation műveletek
        /// </summary>   

        //Foglalások listázása
        internal static async Task<List<Reservation>> ListReservationsAsync()
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriReservation);
            List<Reservation> list = await response.Content.ReadAsAsync<List<Reservation>>();

            return list;
        }

        //Foglalás hozzáadása
        internal static async Task ReservationAsync(Reservation reserveRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriReservation, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
        }
    }
}
