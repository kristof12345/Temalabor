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

namespace Desktop.Services
{
    public class HttpService
    {
        private static string uri = "https://localhost:5001/api/values";

        public static void PostReservation(long planeId, long seatId)
        {
            ReserveSeat_DTO postParameters = new ReserveSeat_DTO(planeId, seatId);
            string postData = JsonConvert.SerializeObject(postParameters);
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = bytes.Length;
            httpWebRequest.ContentType = "text/xml";
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Count());
            }
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            {
                string message = String.Format("POST failed. Received HTTP {0}", httpWebResponse.StatusCode);
                throw new ApplicationException(message);
            }
        }

        public static bool PostLogin(string name, string pass)
        {
            Login_DTO request = new Login_DTO(new User_DTO(name, pass));

            Debug.WriteLine(request.User.Name);

            //Elküldeni

            //Ha van ilyen felhasználó
            if (pass == "Password")
                return true;
            else
                return false;

        }
    }
}
