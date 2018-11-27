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
        private PlaneType planeType = new PlaneType("",0);
        private int selectedSeatTypeIndex = 0;
        private String imageScource = "/Assets/Antonov124white.png";

        public String Name { get { return planeType.PlaneTypeName; } set { planeType.PlaneTypeName = value;  } }

        public String Id { get { return "Plane Type Id: " + planeType.PlaneTypeID.ToString(); } }

        public String[] SeatTypes { get { return Enum.GetNames(typeof(SeatType)); } }

        public int SelectedSeatType
        {
            get { return selectedSeatTypeIndex; }
            set { selectedSeatTypeIndex = value; RaisePropertyChanged("SelectedSeatType"); }
        }

        public String NumberOfSeats
        {
            get { return planeType.Seats.Count.ToString(); }
        }

        public bool Enabled
        {
            get { return (planeType.Seats.Count > 0); }
        }

        public String ImageScource
        {
            get { return imageScource; }
            private set { imageScource = value; RaisePropertyChanged("ImageScource");}
        }

        public List<Seat> Seats { get { return planeType.Seats; } }

        public void AddSeat(double x, double y, int seatType)
        {
            Seat s = new Seat(planeType.Seats.Count);
            s.Coordinates = new Cord((int)x, (int)y);
            s.SeatType = (SeatType) Enum.GetValues(typeof(SeatType)).GetValue(seatType);

            planeType.Seats.Add(s);
            RaisePropertyChanged("NumberOfSeats");
            RaisePropertyChanged("Enabled");
        }

        internal async Task SaveAsync()
        {
            var request = planeType;
            await PlaneTypeDataService.AddPlaneTypeAsync(request);
            PlaneTypeDataService.ReloadTypesListAsync();
        }

        internal void RemoveLastSeat()
        {
            planeType.Seats.RemoveAt(planeType.Seats.Count-1);
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
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Id");
        }
    }
}
