using Desktop.Models;
using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
