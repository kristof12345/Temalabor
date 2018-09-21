using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
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
