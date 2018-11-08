using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Diagnostics;

namespace DAL
{
    public static class Queries
    {
        public static List<DAL.Seat> findSeatsForFlight(DAL.Flight flight, FlightContext context)
        {
            return context.Seats.Where(s => s.planeTypeID == flight.planeTypeID).ToList();
        }

        public static List<DAL.ReservationSeat> findSeatsForReservation(DAL.Reservation reservation, FlightContext context)
        {
            return context.ReservationSeats.Where(s => s.reservationID == reservation.reservationID).ToList();
        }

        public static List<DAL.Reservation> findReservationsForUser(DAL.User user, FlightContext context)
        {
            return context.Reservations.Where(s => s.userID == user.userID).ToList();
        }

        public static FlightContext AddRecordsToReservationSeat(DTO.Reservation reservation, FlightContext context, long reservationID)
        {
            foreach (long seatID in reservation.SeatList)
            {
                ReservationSeat reservSeat = new ReservationSeat();
                reservSeat.reservationID = reservationID;

                var querySeat = context.Seats.Single(s => s.seatID == seatID);
                reservSeat.seatID = querySeat.seatID;
                context.ReservationSeats.Add(reservSeat);
            }

            context.SaveChanges();
            return context;
        }

        public static FlightContext DeleteRecordsOfReservationSeat(FlightContext context, long reservationID)
        {
            foreach (var reserveSeat in context.ReservationSeats)
            {
                if (reserveSeat.reservationID == reservationID)
                    context.ReservationSeats.Remove(reserveSeat);
            }

            context.SaveChanges();
            return context;
        }
    }
}
