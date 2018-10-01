using DTO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace Desktop.Services
{
    public class HttpService
    {
        private static HttpClient client = new HttpClient();
        private static HttpClientHandler handler = new HttpClientHandler();

        private static string baseUri = "https://localhost:5001/api/"; //Kristóf
        //private static string baseUri = "https://localhost:44376/api/"; //Gábor

        private static string UriFlights;
        private static string UriReservation;
        private static string UriUsers;

        //Repülő hozzáadása
        public static async Task PostAddFlightAsync(Flight_DTO addRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriFlights, addRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);

        }

        public static void Initialize()
        {
            //Ne változtasd meg, így működik
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            UriFlights = baseUri + "reserve/"; //UriFlights = baseUri + "flights/";
            UriReservation = baseUri + "reservation/";
            UriUsers = baseUri + "users/";
        }

        //Járatok listázása
        public static async Task PostListAsync(ListFlights_DTO listRequest)
        {
            client = new HttpClient(handler);

            //HttpResponseMessage response = await client.PostAsJsonAsync(uri, listRequest);
            HttpResponseMessage response = await client.GetAsync(UriFlights);
            List<Flight_DTO> fl = await response.Content.ReadAsAsync<List<Flight_DTO>>();
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(fl[1]);
            Debug.WriteLine(fl[4]);
        }

        //Járat törlése
        public static async Task PostDeleteFlightAsync(DeleteFlight_DTO deleteRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.DeleteAsync(UriFlights + deleteRequest.FlightId); //TODO: id
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Járat módosítása
        public static async Task PostUpdateFlightAsync(UpdateFlight_DTO updateRequest)
        {
            client = new HttpClient(handler);

            //HttpResponseMessage response = await client.PostAsJsonAsync(uri, updateRequest);
            HttpResponseMessage response = await client.PutAsJsonAsync(UriFlights + updateRequest.Flight.FlightId, updateRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Foglalás hozzáadása
        public static async Task PostReservationAsync(ReserveSeat_DTO reserveRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriReservation, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Bejelentkezési kérés
        public static async Task PostLoginAsync(Login_DTO loginRequest)
        {
            client = new HttpClient(handler);

            HttpResponseMessage response = await client.PostAsJsonAsync(UriUsers, loginRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        public static bool PostLogin(string name, string pass)
        {
            Login_DTO loginRequest = new Login_DTO(new User_DTO(name, pass));

            //PostLoginAsync(loginRequest);

            //TODO: Ha van ilyen felhasználó és megfelelő a jelszó
            if (pass == "Password")
                return true;
            else
                return false;
        }
    }
}
