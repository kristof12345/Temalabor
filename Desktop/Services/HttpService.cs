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

        public static async void InitializeAsync()
        {
            //Ne változtasd meg, így működik
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            baseUri = System.IO.File.ReadAllText(@"ConfigUri.txt"); //C:\Users\pappkristof\source\repos\Temalabor\Desktop\bin\x86\Debug
            UriFlights = baseUri + "flight/";
            UriReservation = baseUri + "reservation/";
            UriUsers = baseUri + "users/";
            UriTypes = baseUri + "types/"; //TODO: Gábor ezt légyszi rakd a webapiba

            List<String> strArray = new List<String>();

            strArray.Add("Boeing 777");
            strArray.Add("Airbus A380");
            strArray.Add("Boeing 747");
            strArray.Add("Boeing 222");
            strArray.Add("Antonov 124");

            PlaneType.Initialize(strArray.ToArray());
        }

        public static async void PostAddPlaneTypeAsync(PlaneType addRequest)
        {
            client = new HttpClient(handler);
            Debug.WriteLine("A hozzáadott design neve: " + addRequest.PlaneTypeName);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
            var contents = await response.Content.ReadAsStringAsync();
        }

        //Repülőtípusok lekérdezése
        private static async Task<List<String>> GetTypesAsync()
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriTypes);
            List<String> list = await response.Content.ReadAsAsync<List<String>>();
            
            return list;
        }

        //Repülő hozzáadása
        public static async Task PostAddFlightAsync(Flight_DTO addRequest)
        {
            client = new HttpClient(handler);

            Debug.WriteLine("A hozzáadott repülő: " + addRequest);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
            var contents = await response.Content.ReadAsStringAsync();           
        }

        //Járatok listázása
        public static async Task<List<Flight_DTO>> PostListAsync()
        {
            client = new HttpClient(handler);

            Debug.WriteLine("Kérés elküldve.");

            HttpResponseMessage response = await client.GetAsync(UriFlights);
            List<Flight_DTO> list = await response.Content.ReadAsAsync<List<Flight_DTO>>();
            foreach (Flight_DTO f in list) Debug.WriteLine(f);

            return list;
        }

        //Foglalások listázása
        public static async Task<List<Reservation>> GetReservationsAsync()
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.GetAsync(UriReservation);
            List<Reservation> list = await response.Content.ReadAsAsync<List<Reservation>>();

            return list;
        }

        //Járat törlése
        public static async Task PostDeleteFlightAsync(Flight_DTO deleteRequest)
        {
            client = new HttpClient(handler);

            Debug.WriteLine("A törölt repülő ID-ja: " + deleteRequest.FlightId);

            HttpResponseMessage response = await client.DeleteAsync(UriFlights + deleteRequest.FlightId);
            var contents = await response.Content.ReadAsStringAsync();
            
        }

        //Járat módosítása
        public static async Task PostUpdateFlightAsync(Flight_DTO updateRequest)
        {
            client = new HttpClient(handler);

            Debug.WriteLine("A módosított repülő ID-ja: " + updateRequest.FlightId);
            Debug.WriteLine("A módosított repülő adatai: " + updateRequest.ToString());

            HttpResponseMessage response = await client.PutAsJsonAsync(UriFlights + updateRequest.FlightId, updateRequest);
            var contents = await response.Content.ReadAsStringAsync();

        }

        //Foglalás hozzáadása
        public static async Task PostReservationAsync(Reservation reserveRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriReservation, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine(contents);
        }

        //Bejelentkezési kérés
        public static async Task<bool> PostLoginAsync(Login_DTO loginRequest)
        {
            bool contents=false;
            try
            {
                client = new HttpClient(handler);

                HttpResponseMessage response = await client.PostAsJsonAsync(UriUsers, loginRequest);
                contents = await response.Content.ReadAsAsync<bool>();
                Debug.WriteLine(contents);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to login.");
            }

            return contents;
        }

        public static async Task<bool> PostLoginAsync(string name, string pass)
        {
            Login_DTO loginRequest = new Login_DTO(new User(name, pass));

            bool ret = await PostLoginAsync(loginRequest);

            //TODO: Ha van ilyen felhasználó és megfelelő a jelszó
            if (pass == "Password")
                return true;
            else
                return false;
        }
    }
}
