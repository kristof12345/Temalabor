using System;
using Desktop.Dialogs;
using Desktop.Services;
using Desktop.UserControls;
using Desktop.ViewModels;
using DTO;
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

            //User ellenőrzése
            if (SignInService.User == null)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoUserDialog(this);
            }
            else
            {
                //Az első listaelem
                listView.SelectedIndex = 0;
                //Kép betöltése
                ViewModel.LoadImage();
                //User controlok felrakása
                AddUserControls();
            }
        }

        private void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //TODO:töröljük a kijelölt foglalást
        }

        private void AddUserControls()
        {
            canvas.Children.Clear();

            for (int i = 0; i < ViewModel.Flight.TotalSeatsCount; i++)
            {
                Seat s = ViewModel.Flight.GetSeat(i);
                SeatUserControl newSeat = new SeatUserControl(s);
                                                  //Left=0, Top=X, Right=Y, Bottom=0
                newSeat.Margin = new Thickness(ViewModel.Flight.GetSeat(i).Coordinates.X, ViewModel.Flight.GetSeat(i).Coordinates.Y, 0, 0);
                canvas.Children.Add(newSeat);
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DesignerPage), null);
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(DesignerPage), ViewModel.Flight);
        }
    }
}
