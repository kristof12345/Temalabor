using System;
using System.Diagnostics;
using Desktop.Models;
using Desktop.Services;
using Desktop.ViewModels;
using DTO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Desktop.Views
{
    public sealed partial class DataGridPage : Page
    {
        private int PlaneID = 0;
        private User_DTO user;

        private DataGridViewModel ViewModel
        {
            get { return DataContext as DataGridViewModel; }
        }

        public DataGridPage()
        {
            InitializeComponent();
            //A kijelölt sor változását jelző event
            dataTable.SelectionChanged += selected;
            //A dupla kattintást jelző event
            dataTable.DoubleTapped += doubleTapped;
            //ComboBox beállítása
            cbType.ItemsSource = CreateComboBox();
            cbType.SelectedIndex = 0;
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            PageParameter param = new PageParameter(user, (Flight)dataTable.SelectedItem);
            this.Frame.Navigate(typeof(PlanePage), param);
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
            DataService.AddFlight(tempId, tempTime, tbDep.Text, tbDes.Text, cbType.SelectedItem.ToString());
        }

        private void btReserve_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Flight f = (Flight)dataTable.SelectedItem;
            if (f != null)
            {
                f.ReserveSeat(1);
                Debug.WriteLine(f.ToString());
            } //else nincs kijelölt elem
        }

        //Navigálás tesztelése
        private void btNav_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Paraméterek összeállítása
            PageParameter param = new PageParameter(user, (Flight)dataTable.SelectedItem);

            this.Frame.Navigate(typeof(PlanePage), param);
        }

        //Amikor erre a lapra érkezünk
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                //Az átadott paraméterek értelmezése
                var p = (PageParameter)e.Parameter;

                //Elmentjük a felhasználót
                user = p.User;
            }
        }
    }
}
