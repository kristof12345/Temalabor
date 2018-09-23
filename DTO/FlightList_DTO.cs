using System;
using System.Collections.ObjectModel;

namespace DTO
{
    public class FlightList_DTO
    {
        private ObservableCollection<Flight_DTO> data = new ObservableCollection<Flight_DTO>();

        public ObservableCollection<Flight_DTO> GetGridData()
        {
            return data;
        }
    }
}
