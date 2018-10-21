using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class DesignerViewModel : ViewModelBase
    {
        private List<Seat> seats = new List<Seat>();
        private String name;
        private int price;
        private String[] seatTypes = { "Normal", "Premium" };
        private int selectedSeatTypeIndex = 0;
        private String imageScource = "/Assets/Antonov124white.png";

        public String Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }

        public String Price { get { return price.ToString(); } set { Int32.TryParse(value, out price); RaisePropertyChanged("Price"); } }

        public String[] SeatTypes { get { return seatTypes; } }

        public int SelectedSeatType
        {
            get { return selectedSeatTypeIndex; }
            set { selectedSeatTypeIndex = value; RaisePropertyChanged("SelectedSeatType"); }
        }

        public String NumberOfSeats
        {
            get { return seats.Count.ToString(); }
        }

        public bool Enabled
        {
            get { return (seats.Count > 0); }
        }

        public String ImageScource
        {
            get { return imageScource; }
            private set { imageScource = value; RaisePropertyChanged("ImageScource"); Debug.WriteLine("Picked photo: " + ImageScource); }
        }

        public List<Seat> Seats { get { return seats; } }

        public void AddSeat(double x, double y, int seatType=1)
        {
            Seat s = new Seat(seats.Count);
            s.Coordinates = new Cord((int)x, (int)y);
            s.Price = price;
            s.SeatType = SeatTypes[SelectedSeatType];

            seats.Add(s);
            RaisePropertyChanged("NumberOfSeats");
            RaisePropertyChanged("Enabled");
        }

        internal void Save()
        {
            var request = new PlaneType(Name, seats);
            HttpService.AddPlaneTypeAsync(request);
        }

        internal void RemoveLastSeat()
        {
            seats.RemoveAt(seats.Count-1);
            RaisePropertyChanged("NumberOfSeats");
            RaisePropertyChanged("Enabled");
        }

        //Kép kiválasztása
        internal async Task PickImageAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                ImageScource = "/Assets/" + file.Name ;
            }
        }
    }
}
