using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UpdateFlight_DTO
    {
        public UpdateFlight_DTO(Flight_DTO f)
        {
            Flight = f;
        }
        //A módosított adatokat tartalmazza
        public Flight_DTO Flight;
    }
}
