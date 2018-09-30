using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DeleteFlight_DTO
    {
        public DeleteFlight_DTO(long id)
        {
            FlightId = id;
        }

        //A törlendő járat ID-ja
        public long FlightId { get; set; }
    }
}
