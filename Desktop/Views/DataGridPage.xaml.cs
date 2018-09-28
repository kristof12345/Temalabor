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
        private static int PlaneID = 0;
        //public static Flight lastSelected;

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
            Flight param = (Flight)dataTable.SelectedItem;
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

        //Foglalás gomb
        private void btReserve_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Paraméterek összeállítása
            Flight param = (Flight)dataTable.SelectedItem;
            //Navigálás a PlanePage-re
            this.Frame.Navigate(typeof(PlanePage), param);
        }

        //Amikor erre a lapra érkezünk
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //User ellenőrzése
            if (SignInService.User == null)
            {
                DisplayNoUserDialog();               
            }
            else
            {
                //Ha customer, akkor nem mutatjuk az admin funkciókat
                if (SignInService.User.UserType == UserType.Customer)
                {
                    inputArea.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    searchArea.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }

                //Ha adminisztrátor, akkor adhat hozzá repülőt
                if (SignInService.User.UserType == UserType.Administrator)
                {
                    inputArea.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    searchArea.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }

        //Dialógusablak
        private async void DisplayNoUserDialog()
        {
            ContentDialog noUser = new ContentDialog
            {
                Title = "You're not signed in.",
                Content = "Plese sign in to continue.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noUser.ShowAsync();
            this.Frame.Navigate(typeof(UserPage));
        }

        private void btSearch_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ListFlights_DTO list = new ListFlights_DTO();
            //A textboxok alapján beállítjuk az adatokat
            if (cbDate.IsChecked == true) { list.AtDate = dpDate2.Date.UtcDateTime; }
            if (cbLocation.IsChecked == true) { list.From = tbDep2.Text; list.To = tbDes2.Text; }
            if (cbAvailable.IsChecked == true) { list.OnlyAvailable = true; }
            HttpService.PostListAsync(list);
        }
    }
}
