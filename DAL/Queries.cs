using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;

namespace DAL
{
    public static class Queries
    {
        public static List<DAL.Seat> findSeatsForFlight(DAL.Flight flight, FlightContext _context)
        {
            return _context.Seats.Where(s => s.planeTypeID == flight.planeTypeID).ToList();
        }

        public static List<DAL.SeatsOnFlight> findSeatsForReservation(DAL.Reservation reservation, FlightContext _context)
        {
            return _context.SeatsOnFlights.Where(s => s.flightID == reservation.flightID).ToList();
        }

        public static FlightContext fillSeatsOnFlight(DAL.Flight flight, FlightContext context)
        {
            var seats = context.Seats.Where(s => s.planeTypeID == flight.planeTypeID);
            foreach(DAL.Seat seat in seats)
            {
                SeatsOnFlight temp = new SeatsOnFlight();
                temp.flightID = flight.flightID;
                temp.seatID = seat.seatID;
                temp.isReserved = false;
                context.SeatsOnFlights.Add(temp);
            }

            context.SaveChanges();
            return context;
        }

        public static FlightContext deleteSeatsOnFlight(DAL.Flight flight, FlightContext context)
        {
            var seats = context.SeatsOnFlights.Where(s => s.flightID == flight.flightID);
            foreach (DAL.SeatsOnFlight seat in seats)
            {
                context.Remove(seat);
            }
            context.SaveChanges();
            return context;
        }

        public static FlightContext reserveSeatsOnFlight(DAL.Reservation reservation, FlightContext context)
        {
            var seats = context.SeatsOnFlights.Where(s => s.flightID == reservation.flightID);
            foreach (DAL.SeatsOnFlight seat in seats)
            {                
                seat.isReserved = true;
            }
            context.SaveChanges();
            return context;
        }

        public static FlightContext unReserveSeatsOnFlight(DAL.Reservation reservation, FlightContext context)
        {
            var seats = context.SeatsOnFlights.Where(s => s.flightID == reservation.flightID);
            foreach (DAL.SeatsOnFlight seat in seats)
            {               
                seat.isReserved = false;
            }
            context.SaveChanges();
            return context;
        }

    }
}
