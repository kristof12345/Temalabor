using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public static class Queries
    {
        public static List<DAL.Seat> findSeatsForFlight(DAL.Flight flight, FlightContext _context)
        {
            return _context.Seats.Where(s => s.planeTypeID == flight.planeTypeID).ToList();
        }
    }
}
