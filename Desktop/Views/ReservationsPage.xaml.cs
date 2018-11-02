using Desktop.Dialogs;
using Desktop.Services;
using Desktop.ViewModels;
using DTO;
using System;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace Desktop.Views
{
    public sealed partial class ReservationsPage : Page
    {
        public ReservationsPage()
        {
            this.InitializeComponent();
            grid.SelectionChanged += ItemSelected;
        }

        private void ItemSelected(object sender, DataGridSelectionChangedEventArgs e)
        {
            if(grid.SelectedItem!=null)
                ViewModel.IsReservationSelected = true;

        }

        private ReservationViewModel ViewModel
        {
            get { return DataContext as ReservationViewModel; }
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
                //TODO: kiírjuk a felhasználó foglalásait
                //textBlock.Text = SignInService.User.Name;
            }
        }

        private void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ReservationsDataService.DeleteReservation((Reservation) grid.SelectedItem);
            ViewModel.IsReservationSelected = false;
        }
    }
}
