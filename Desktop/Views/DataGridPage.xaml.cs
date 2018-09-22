using System;
using System.Diagnostics;
using Desktop.Models;
using Desktop.Services;
using Desktop.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Desktop.Views
{
    public sealed partial class DataGridPage : Page
    {
        private int PlaneID =0;

        private DataGridViewModel ViewModel
        {
            get { return DataContext as DataGridViewModel; }
        }

        public DataGridPage()
        {
            InitializeComponent();
            //A kijelölt sor változását jelző event
            dataTable.SelectionChanged += selected;
            cbType.ItemsSource = CreateComboBox();
            cbType.SelectedIndex = 0;
        }

        //Combo box feltöltése
        private object CreateComboBox()
        {
            string[] strArray =
                {
                "Airbus A380",
                "Boeing 747",
                "Boeing 777",
                "Antonov 124",
                //További repülő típusok
            };
            return strArray;
        }

        //Ha változott a kijelölt sor
        private void selected(object sender, SelectionChangedEventArgs e)
        {
            //Esetleg részleteket írhatunk ki a kijelölt járatról
        }

        //Új járat felvétele
        private void btAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int tempId;
            //Ha nem sikerül parsolni az ID-t, akkor az alap generált, növekvő id-t kapja
            if (!int.TryParse(tbId.Text, out tempId))
            {
                PlaneID++;
                tempId = PlaneID;
            }

            //Idő összerakása a Date pickerből és a Time pickerből
            DateTime tempTime = dpDate.Date.UtcDateTime;
            TimeSpan ts = dpTime.Time;
            tempTime = tempTime.Date + ts;

            //Járat hozzáadása
            DataService.AddFlight(tempId, tempTime,tbDep.Text,tbDes.Text,cbType.SelectedItem.ToString());
        }

        private void btReserve_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Flight f = (Flight)dataTable.SelectedItem;
            if (f != null)
            {
                f.ReserveSeat(1);
                Debug.WriteLine(f.FreeSeats);
            } //else nincs kijelölt elem
        }

        //Navigálás tesztelése
        private void btNav_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlanePage));
        }
    }
}
