﻿using System;
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
            cbType.ItemsSource = PlaneTypes.CreateComboBox();
            cbType.SelectedIndex = 0;
            //Eseménykezelő az adatok változására
            DataService.ChangedEvent += Update;
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Flight param = (Flight)dataTable.SelectedItem;
            this.Frame.Navigate(typeof(PlanePage), param);
        }

        //Ha változott a kijelölt sor
        private void selected(object sender, SelectionChangedEventArgs e)
        {
            //Esetleg részleteket írhatunk ki a kijelölt járatról
        }

        //Új járat felvétele a gomb megnyomásakor
        private void btAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int tempId;
            //Ha nem sikerül parsolni az ID-t, akkor az alap generált, növekvő id-t kapja
            if (!int.TryParse(tbId.Text, out tempId))
            {
                ViewModel.PlaneID++;
                tempId = ViewModel.PlaneID;
            }

            //Idő összerakása a Date pickerből és a Time pickerből
            DateTime tempTime = ViewModel.CombineDateAndTime(dpDate.Date, dpTime.Time);

            //Járat hozzáadása
            DataService.AddFlight(tempId, tempTime, tbDep.Text, tbDes.Text, cbType.SelectedItem.ToString());
        }


        //Foglalás gomb
        private void btReserve_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                //Paraméterek összeállítása
                Flight param = (Flight)dataTable.SelectedItem;
                //Navigálás a PlanePage-re
                this.Frame.Navigate(typeof(PlanePage), param);
            }
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
            //HttpService.PostListAsync(list);
        }

        private void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Ha van kiválasztott repülő, töröljük
            if (dataTable.SelectedItem != null)
            {
                DataService.DeleteFlight((Flight)dataTable.SelectedItem);
            }
        }

        private async void btUpdate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                Flight f = (Flight)dataTable.SelectedItem;
                UpdateDialog dialog = new UpdateDialog(f);
                ContentDialogResult result = await dialog.ShowAsync();
                //Ha az Apply-re kattintott
                if(result==ContentDialogResult.Secondary)
                {
                    f.Date = ViewModel.CombineDateAndTime(dialog.Date, dialog.Time);
                    f.Departure = dialog.Departure;
                    f.Destination = dialog.Destination;
                    f.Status = dialog.Status;
                    f.PlaneType = dialog.PlaneType;

                    //Táblázat frissítése
                    DataService.UpdateFlight(f);
                }
            }
        }

        //A táblázat frissítése
        private void Update(Object sender, EventArgs e)
        {
            dataTable.ItemsSource = ViewModel.Source;
        }
    }
}
