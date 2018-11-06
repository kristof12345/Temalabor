using Desktop.Dialogs;
using Desktop.Services;
using Desktop.UserControls;
using Desktop.ViewModels;
using DTO;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Desktop.Views
{
    public sealed partial class PlaneTypeDesignerPage : Page
    {
        private PlaneTypeDesignerViewModel ViewModel
        {
            get { return DataContext as PlaneTypeDesignerViewModel; }
        }

        public PlaneTypeDesignerPage()
        {
            InitializeComponent();
            canvas.PointerPressed += clicked;
        }

        private void clicked(object sender, PointerRoutedEventArgs e)
        {
            //Egér pozíciójának lekérdezése
            var mousePos = e.GetCurrentPoint(canvas).Position;
            var seatPos = new Point(mousePos.X - 10, mousePos.Y - 8); //Hogy az egér a UserControl középpontjában legyen
            NormalSeatUserControl newSeat = new NormalSeatUserControl(new Seat());
            //Left=X, Top=Y, Right=0, Bottom=0
            newSeat.Margin = new Thickness(seatPos.X, seatPos.Y, 0, 0);
            canvas.Children.Add(newSeat);
            ViewModel.AddSeat(seatPos.X, seatPos.Y);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                //Módosítás
                var type = (PlaneType)e.Parameter;
                ViewModel.SetPlaneType(type);
                tbTitle.Text = "Modify " + type.PlaneTypeName;
            }
            else
            {
                //Új hozzáadása
                ViewModel.SetPlaneType(new PlaneType());
                tbTitle.Text = "New plane type";
            }
            //A korábbi székeket visszarajzoljuk
            foreach (Seat s in ViewModel.Seats)
            {
                UserControl newSeat;
                if (s.SeatType == SeatType.Normal)
                {
                    newSeat = new NormalSeatUserControl(s);
                }
                else
                {
                    newSeat = new FirstClassSeatUserControl(s);
                }
                newSeat.Margin = new Thickness(s.Coordinates.X, s.Coordinates.Y, 0, 0);
                canvas.Children.Add(newSeat);
            }
        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveAsync();
            //this.Frame.Navigate(typeof(PlaneTypeManagerPage));
        }

        private void btundo_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.RemoveAt(ViewModel.Seats.Count-1); //Töröljük az utolsót a vászonról
            ViewModel.RemoveLastSeat(); //Töröljük a listából is
        }

        private async void btFilePicker_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.PickImageAsync();
        }
    }
}
