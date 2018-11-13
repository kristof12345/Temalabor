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
        private static string token = "";

        //Config fájlból olvassa be
        private static string baseUri;

        private static string UriFlights;
        private static string UriReservation;
        private static string UriUsers;
        private static string UriTypes;
        private static string UriSeats;
        private static string UriImages;

        internal static async Task Initialize()
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            client = new HttpClient(handler);

            baseUri = System.IO.File.ReadAllText(@"ConfigUri.txt"); //C:\Users\pappkristof\source\repos\Temalabor\Desktop\bin\x86\Debug
            //baseUri = "https://localhost:5001/API/";
            UriFlights = baseUri + "flight/";
            UriReservation = baseUri + "reservation/";
            UriUsers = baseUri + "user/";
            UriTypes = baseUri + "planetype/";
            UriSeats = baseUri + "seat/flightID/";
            UriImages = baseUri + "image/";

            //Lehetséges PlaneTypok betöltése
            var strArray = await ListPlaneTypeNamesAsync();
            PlaneType.Initialize(strArray.ToArray());
        }

        /// <summary>
        /// Flight műveletek
        /// </summary>

        //Járatok listázása
        internal static async Task<List<Flight_DTO>> ListFlightsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(UriFlights);
            List<Flight_DTO> list = await response.Content.ReadAsAsync<List<Flight_DTO>>();
            foreach (Flight_DTO f in list)
            {
                HttpResponseMessage seatResponse = await client.GetAsync(UriSeats + f.FlightId);
                List<Seat> seatList = await seatResponse.Content.ReadAsAsync<List<Seat>>();
                f.PlaneType = new PlaneType(f.PlaneTypeName, f.PlaneTypeID);
                f.PlaneType.Seats = seatList;
            }
            return list;
        }

        //Járat hozzáadása
        internal static async Task AddFlightAsync(Flight_DTO addRequest)
        {
            Debug.WriteLine(addRequest.Departure);
            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
        }

        //Járat törlése
        public static async Task DeleteFlightAsync(Flight_DTO deleteRequest)
        {
            HttpResponseMessage response = await client.DeleteAsync(UriFlights + deleteRequest.FlightId);
        }

        //Járat módosítása
        internal static async Task UpdateFlightAsync(Flight_DTO updateRequest)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(UriFlights + updateRequest.FlightId, updateRequest);
        }

        /// <summary>
        /// User műveletek
        /// </summary>

        //Felhasználó hozzáadása
        internal static async void AddUserAsync(User addRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(UriUsers, addRequest);
        }

        //Bejelentkezési kérés
        internal static async Task<Session> LoginAsync(User loginRequest)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(UriUsers, loginRequest);
            Session session = await response.Content.ReadAsAsync<Session>();
            if (session == null) { Debug.WriteLine("A szerver nem válaszolt."); session = new Session(loginRequest); }
            token = session.Token;

            //Token hozzáadása a headerekhez
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            Debug.WriteLine("A bejelentkezéshez tartozó token: " + token);

            return session;
        }

        /// <summary>
        /// PlaneType műveletek
        /// </summary>

        //Repülőtípusok nevének lekérdezése
        internal static async Task<List<String>> ListPlaneTypeNamesAsync()
        {
            HttpResponseMessage response = await client.GetAsync(UriTypes);
            List<String> typesList = await response.Content.ReadAsAsync<List<String>>();
            return typesList;
        }

        //Repülőtípusok lekérdezése
        internal static async Task<List<PlaneType>> ListPlaneTypesAsync()
        {
            List<PlaneType> typesList = new List<PlaneType>();

            //Lehetséges PlaneTypok betöltése
            var strArray = await ListPlaneTypeNamesAsync();
            PlaneType.Initialize(strArray.ToArray());
            int max = strArray.Count;
            for (int i = 1; i <= max; i++)
            {
                HttpResponseMessage response = await client.GetAsync(UriTypes + i);
                PlaneType t = await response.Content.ReadAsAsync<PlaneType>();
                if (t == null) { max++; continue; }
                typesList.Add(t);
            }
            return typesList;
        }

        //Repülőtípus hozzáadása
        internal static async Task AddPlaneTypeAsync(PlaneType addRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(UriTypes, addRequest);
        }

        //Repülőtípus törlése
        internal static async Task DeletePlaneTypeAsync(PlaneType deleteRequest)
        {
            HttpResponseMessage response = await client.DeleteAsync(UriTypes + deleteRequest.PlaneTypeID);
        }

        //Kép URL előállítása
        internal static Uri LoadImageUri(long planeTypeID)
        {
            //TODO: a típus alapján
            Uri uri = new Uri(UriImages + planeTypeID);
            return new Uri("https://image.ibb.co/gOHBVf/Antonov124white.png");
        }

        /// <summary>
        /// Reservation műveletek
        /// </summary>   

        //Foglalások listázása
        internal static async Task<List<Reservation>> ListReservationsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(UriReservation);
            List<Reservation> list = await response.Content.ReadAsAsync<List<Reservation>>();
            //if (list == null) { Debug.WriteLine("NULL lista jött az adatbázistól!"); list = new List<Reservation>(); }
            return list;
        }

        //Egy felhasználóhoz tartozó foglalások listázása
        internal static async Task<List<Reservation>> ListMyReservationsAsync()
        {
            var userId = SignInService.User.UserId;
            HttpResponseMessage response = await client.GetAsync(UriReservation + "UserID/" + userId);
            List<Reservation> list = await response.Content.ReadAsAsync<List<Reservation>>();
            //if (list == null) { Debug.WriteLine("NULL lista jött az adatbázistól!"); list = new List<Reservation>(); }
            return list;
        }

        //Foglalás hozzáadása
        internal static async Task ReservationAsync(Reservation reserveRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(UriReservation, reserveRequest);
        }

        //Foglalás törlése
        internal static async Task DeleteReservationAsync(Reservation selectedItem)
        {
            HttpResponseMessage response = await client.DeleteAsync(UriReservation + selectedItem.ReservationId);
        }
    }
}
