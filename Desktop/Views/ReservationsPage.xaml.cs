using Desktop.Services;
using Desktop.ViewModels;
using DTO;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Controls;


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

        private async void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ReservationsDataService.DeleteReservationAsync((Reservation) grid.SelectedItem);
            //ViewModel.DeleteReservationAsync();
            ViewModel.IsReservationSelected = false;
        }
    }
}
