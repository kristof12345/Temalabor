using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    public class PlaneTypeManagerViewModel : ViewModelBase
    {
        private bool isTypeSelected = false;

        public ObservableCollection<PlaneType> Source
        {
            get
            {
                return PlaneTypeDataService.PlaneTypeList;
            }
        }

        public bool IsTypeSelected
        {
            get { return isTypeSelected; }
            set { isTypeSelected = value; RaisePropertyChanged("IsTypeSelected"); }
        }

        internal void SelectedAt(int index)
        {
            Flight = PlaneTypeDataService.PlaneTypeList[index];
            RaisePropertyChanged("Flight");
            isTypeSelected = true;
        }

        public PlaneType Flight = new PlaneType("abc", 1);

        private Uri image;

        public Uri Image
        {
            get { return image; }
            private set { image = value; RaisePropertyChanged("Image"); }
        }

        public void LoadImage()
        {
            Image = PlaneTypeDataService.LoadImageUri(Flight.PlaneTypeID);
        }
    }
}
