using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Desktop.Services
{
    public class HttpService
    {
        private static HttpClient client = new HttpClient();
        private static string uri = "https://www.google.hu/";
        //private static string uri = "https://localhost:5001/";

        public static async Task PostAddFlightAsync(Flight_DTO addRequest)
        {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, addRequest);
                var contents = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(contents);
        }

        public static async Task PostReservationAsync(long planeId, long seatId)
        {
                ReserveSeat_DTO reserveRequest = new ReserveSeat_DTO(planeId, seatId);
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, reserveRequest);
                Debug.WriteLine(response);
        }

        public static async Task PostLoginAsync(string name, string pass)
        {
                Login_DTO loginRequest = new Login_DTO(new User_DTO(name, pass));
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, loginRequest);
                Debug.WriteLine(response);
        }

        public static bool PostLogin(string name, string pass)
        {
            //PostLoginAsync(name, pass);

            //TODO: Ha van ilyen felhasználó
            if (pass == "Password")
                return true;
            else
                return false;
        }

        public static async Task PostListAsync(ListFlights_DTO list)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, list);
                Debug.WriteLine(response);
            }
        }

    }
}
