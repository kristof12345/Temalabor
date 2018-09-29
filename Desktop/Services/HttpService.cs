using DTO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

namespace Desktop.Services
{
    public class HttpService
    {
        private static HttpClient client = new HttpClient();
        private static HttpClientHandler handler = new HttpClientHandler();
        //private static string uri = "https://www.google.hu/";
        private static string uri = "https://localhost:44376/api/reserve";

        //Repülő hozzáadása
        public static async Task PostAddFlightAsync(Flight_DTO addRequest)
        {
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;

            handler.ServerCertificateCustomValidationCallback =

                (httpRequestMessage, cert, cetChain, policyErrors) =>

                {

                    return true;

                };
            client = new HttpClient(handler);
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, addRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Foglalás hozzáadása
        public static async Task PostReservationAsync(ReserveSeat_DTO reserveRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, reserveRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Járatok listázása
        public static async Task PostListAsync(ListFlights_DTO listRequest)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(uri, listRequest);
            var contents = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(contents);
        }

        //Bejelentkezési kérés
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

            //TODO: Ha van ilyen felhasználó és megfelelő a jelszó
            if (pass == "Password")
                return true;
            else
                return false;
        }
    }
}
