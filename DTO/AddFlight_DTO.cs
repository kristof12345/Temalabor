using System;

namespace DTO
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
