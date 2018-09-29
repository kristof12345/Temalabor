using DTO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

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

        public static async Task PostReservationAsync(ReserveSeat_DTO reserveRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        public static async Task PostListAsync(ListFlights_DTO listRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, listRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        public static async Task PostLoginAsync(Login_DTO loginRequest)
        {              
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, loginRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        public static bool PostLogin(string name, string pass)
        {
            Login_DTO loginRequest = new Login_DTO(new User_DTO(name, pass));
            //PostLoginAsync(loginRequest);

            //TODO: Ha van ilyen felhasználó
            if (pass == "Password")
                return true;
            else
                return false;
        }
    }
}
