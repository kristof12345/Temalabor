using System;
using System.Diagnostics;
using Desktop.Dialogs;
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
            cbType.ItemsSource = PlaneType.CreateComboBox();
            cbType.SelectedIndex = 0;
        }


        //Ha változott a kijelölt sor
        private void selected(object sender, DataGridSelectionChangedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                ViewModel.IsFlightSelected = true;
            }
            else
            {
                ViewModel.IsFlightSelected = false;
            }
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                Flight param = (Flight)dataTable.SelectedItem;
                this.Frame.Navigate(typeof(PlanePage), param);
            }
        }

        //Új járat felvétele a gomb megnyomásakor
        private void btAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CommandBase cmd = new AddCommand(ViewModel.NextId, dpDate.Date, dpTime.Time, tbDep.Text, tbDes.Text, cbType.SelectedValue.ToString(), cbType.SelectedIndex+1);
            Debug.WriteLine("Selected: " + cbType.SelectedIndex);
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
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoUserDialog(this);               
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

        private void btDelete_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Ha van kiválasztott repülő, töröljük
            if (dataTable.SelectedItem != null)
            {
                //DataService.DeleteFlightAsync((Flight)dataTable.SelectedItem);
                CommandBase cmd = new DeleteCommand((Flight)dataTable.SelectedItem);
                ViewModel.ExecuteCommand(cmd);
            }
        }

        private async void btUpdate_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                Flight flight = (Flight)dataTable.SelectedItem;
                Flight old = flight.Copy();

                UpdateDialog dialog = new UpdateDialog(flight);
                ContentDialogResult result = await dialog.ShowAsync();
                //Ha az Apply-re kattintott
                if(result==ContentDialogResult.Secondary)
                { 
                    CommandBase cmd = new UpdateCommand(flight, old);
                    ViewModel.ExecuteCommand(cmd);
                }
                else //Különben CANCEL
                {
                    flight = old;
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
