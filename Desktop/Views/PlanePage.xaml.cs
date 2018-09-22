using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Data_Transfer_Objects;
using Newtonsoft.Json;
using System.Net.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        public PlanePage()
        {
            this.InitializeComponent();
            tb.Text = "hello world";
            //imgPlane.Sco
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Az 1. repülő 1. széke
            POSTReserveRequest(1,1);
        }

        public async void POSTReserveRequest(long planeId, long seatId)
        {
            Uri requestUri = new Uri("https://www.userauth"); //Ide majd a tényleges adatbázis elérés kell

            ReserveSeat_DTO reserve = new Data_Transfer_Objects.ReserveSeat_DTO(1,1);
            string json = JsonConvert.SerializeObject(reserve, Formatting.Indented);

            var objClint = new System.Net.Http.HttpClient();
            //A Http válasz
            System.Net.Http.HttpResponseMessage response;
            //Aszinkron Http kérés
            response = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            //A válasz szöveggé alakítása
            string responJsonText = await response.Content.ReadAsStringAsync();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
