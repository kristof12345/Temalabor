﻿using Desktop.UserControls;
using Desktop.ViewModels;
using DTO;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Desktop.Views
{
    public sealed partial class PlaneTypeManagerPage : Page
    {
        private PlaneTypeManagerViewModel ViewModel
        {
            get { return DataContext as PlaneTypeManagerViewModel; }
        }

        public PlaneTypeManagerPage()
        {
            this.InitializeComponent();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listView.SelectedIndex;
            ViewModel.SelectedAt(index);
            //Kép betöltése
            ViewModel.LoadImage();
            //User controlok felrakása
            AddUserControls();

        }

        //Amikor erre a lapra érkezünk
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Kép betöltése
            ViewModel.LoadImage();
            //User controlok felrakása
            AddUserControls();
        }

        private async void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Töröljük a kijelölt típust
            var deleteRequest = (PlaneType)listView.SelectedItem;
            await ViewModel.DeletePlaneTypeAsync(deleteRequest);
            //listView.SelectedIndex = 0;
        }

        private void AddUserControls()
        {
            canvas.Children.Clear();
            if (ViewModel.PlaneType != null)
            {
                for (int i = 0; i < ViewModel.PlaneType.TotalSeatsCount; i++)
                {
                    Seat s = ViewModel.PlaneType.GetSeat(i);
                    UserControl newSeat;
                    if (s.SeatType == SeatType.Normal)
                    {
                        newSeat = new NormalSeatUserControl(s);
                    }
                    else
                    {
                        newSeat = new FirstClassSeatUserControl(s);
                    }
                    newSeat.Margin = new Thickness(ViewModel.PlaneType.GetSeat(i).Coordinates.X, ViewModel.PlaneType.GetSeat(i).Coordinates.Y, 0, 0);
                    canvas.Children.Add(newSeat);
                }
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlaneTypeDesignerPage), null);
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PlaneTypeDesignerPage), ViewModel.PlaneType);
        }
    }
}
