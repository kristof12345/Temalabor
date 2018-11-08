using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    public class PlaneTypeManagerViewModel : ViewModelBase
    {
        public ObservableCollection<PlaneType> Source
        {
            get
            {
                return PlaneTypeDataService.PlaneTypeList;
            }
        }

        public PlaneType PlaneType;

        private Uri image;

        public Uri Image
        {
            get { return image; }
            private set { image = value; RaisePropertyChanged("Image"); }
        }

        public void LoadImage()
        {
            if (PlaneType != null)
            {
                Image = PlaneTypeDataService.LoadImageUri(PlaneType.PlaneTypeID);
            }
        }

        public bool IsTypeSelected
        {
            get { return PlaneType!=null; }
        }

        internal void SelectedAt(int index)
        {
            if (index >= 0)
            {
                PlaneType = PlaneTypeDataService.PlaneTypeList[index];
            }
            else PlaneType = null;
            RaisePropertyChanged("Flight");
            RaisePropertyChanged("IsTypeSelected");
        }
    }
}
