using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    //Járat hozzáadása
    class AddFlight_DTO
    {
        //A listához adandó repülő
        private Flight_DTO flight;
        AddFlight_DTO()
        {
            flight = new Flight_DTO(10);
            flight.FlightId = 99;
        }
    }
}
