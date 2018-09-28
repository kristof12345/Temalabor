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

namespace Desktop.Services
{
    public class HttpService
    {
        private static string uri = "https://localhost:5001/api/reserve";

        public static async Task PostReservationAsync(long planeId, long seatId)
        {
            using (var client = new HttpClient())
            {
                ReserveSeat_DTO reserveRequest = new ReserveSeat_DTO(1, 1);
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, reserveRequest);
                response.EnsureSuccessStatusCode();
                Debug.WriteLine(response);
            }
        }

        public static async Task PostAddFlightAsync()
        {
            using (var client = new HttpClient())
            {
                Flight_DTO reserveRequest = new Flight_DTO(3);
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, reserveRequest);
                response.EnsureSuccessStatusCode();
                Debug.WriteLine(response);
            }
        }

        public static async Task PostLoginAsync(string name, string pass)
        {
            using (var client = new HttpClient())
            {
                Login_DTO loginRequest = new Login_DTO(new User_DTO(name, pass));
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, loginRequest);
                response.EnsureSuccessStatusCode();
                Debug.WriteLine(response);
            }
        }
        public static bool PostLogin(string name, string pass)
        {
            PostLoginAsync(name, pass);

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
                response.EnsureSuccessStatusCode();
                Debug.WriteLine(response);
            }
        }

    }
}
