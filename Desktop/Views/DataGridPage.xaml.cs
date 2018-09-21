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
        }

        //Ha változott a kijelölt sor
        private void selected(object sender, SelectionChangedEventArgs e)
        {
            //Esetleg részleteket írhatunk ki
        }

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
            DataService.AddFlight(tempId, tempTime,tbDep.Text,tbDes.Text);
        }

        private void btReserve_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Flight f = (Flight)dataTable.SelectedItem;
            f.ReserveSeat(1);
            Debug.WriteLine(f.FreeSeats);
        }
    }
}
