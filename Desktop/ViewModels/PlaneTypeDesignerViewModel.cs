using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class PlaneTypeDesignerViewModel : ViewModelBase
    {
        private List<Seat> seats = new List<Seat>();
        private PlaneType planeType;
        private String name;
        private int selectedSeatTypeIndex = 0;
        private String imageScource = "/Assets/Antonov124white.png";

        public String Name { get { return name; } set { name = value; RaisePropertyChanged("Name"); } }

        public String[] SeatTypes { get { return Enum.GetNames(typeof(SeatType)); } }

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
            private set { imageScource = value; RaisePropertyChanged("ImageScource");}
        }

        public List<Seat> Seats { get { return seats; } }

        public void AddSeat(double x, double y, int seatType=1)
        {
            Seat s = new Seat(seats.Count);
            s.Coordinates = new Cord((int)x, (int)y);
            s.SeatType = (SeatType) Enum.GetValues(typeof(SeatType)).GetValue(selectedSeatTypeIndex);

            seats.Add(s);
            RaisePropertyChanged("NumberOfSeats");
            RaisePropertyChanged("Enabled");
        }

        internal async Task SaveAsync()
        {
            var request = new PlaneType(Name, 0);
            request.Seats = seats;
            await PlaneTypeDataService.AddPlaneTypeAsync(request);
            PlaneTypeDataService.ReloadTypesListAsync();
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

        public void SetPlaneType(PlaneType t)
        {
            planeType = t;
            Name = t.PlaneTypeName;
            seats = t.Seats;
        }
    }
}
