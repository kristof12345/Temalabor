using System;
using System.Diagnostics;
using Desktop.Models;
using Desktop.Services;
using Desktop.ViewModels;
using DTO;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Desktop.Views
{
    public sealed partial class FlightsPage : Page
    {
        private FlightViewModel ViewModel
        {
            get { return DataContext as FlightViewModel; }
        }

        public FlightsPage()
        {
            InitializeComponent();
            //A kijelölt sor változását jelző event
            dataTable.SelectionChanged += selected;
            //A dupla kattintást jelző event
            dataTable.DoubleTapped += doubleTapped;
            //ComboBox beállítása
            cbType.ItemsSource = PlaneTypes.CreateComboBox();
            cbType.SelectedIndex = 0;
        }


        //Ha változott a kijelölt sor
        private void selected(object sender, DataGridSelectionChangedEventArgs e)
        {
            //Valami
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Flight param = (Flight)dataTable.SelectedItem;
            this.Frame.Navigate(typeof(PlanePage), param);
        }

        //Új járat felvétele a gomb megnyomásakor
        private void btAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ICommandBase cmd = new AddCommand(dpDate.Date, dpTime.Time, tbDep.Text, tbDes.Text, cbType.SelectedValue.ToString());
            ViewModel.ExecuteCommand(cmd);
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
            //TODO: valami
        }

        private void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Ha van kiválasztott repülő, töröljük
            if (dataTable.SelectedItem != null)
            {
                //DataService.DeleteFlightAsync((Flight)dataTable.SelectedItem);
                ICommandBase cmd = new DeleteCommand((Flight)dataTable.SelectedItem);
                ViewModel.ExecuteCommand(cmd);
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
                    //f.Date = ViewModel.CombineDateAndTime(dialog.Date, dialog.Time);
                    f.Departure = dialog.Departure;
                    f.Destination = dialog.Destination;
                    f.Status = dialog.Status;
                    f.PlaneType = dialog.PlaneType;

                    //Táblázat frissítése
                    DataService.UpdateFlightAsync(f);
                }
            }
        }

        private void btUndo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.UnExecuteCommand();
        }

        private void btRedo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.ReExecuteCommand();
        }
    }
}
