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
        public static DAL.User findUser(DTO.User dtoUser, FlightContext context)
        {
            var user = context.Users.Single(u => u.name.Equals(dtoUser.Name));
            return user;
        }

        public static bool findUserName(DTO.User dtoUser, FlightContext context)
        {
            var user = context.Users.Single(u => u.name.Equals(dtoUser.Name));
            if (user != null)
                return true;
            return false;
        }

        public static bool findUserPassword(DTO.User dtoUser, FlightContext context)
        {
            var user = context.Users.Single(u => u.password.Equals(dtoUser.Password));
            if (user != null)
                return true;
            return false;
        }

        public static bool isThereReservationForFlight(long flightID, FlightContext context)
        {
            var flight =    from rs in context.ReservationSeats
                            join r in context.Reservations on rs.reservationID equals r.reservationID
                            where r.flightID == flightID
                            select r.flightID;
            if (flight.Any())
                return true;
            return false;
        }

        public static bool isThereReservationForPlaneType(long planeTypeID, FlightContext context)
        {
            var planeType = from rs in context.ReservationSeats
                         join r in context.Reservations on rs.reservationID equals r.reservationID
                         join f in context.Flights on r.flightID equals f.flightID
                         where f.planeTypeID == planeTypeID
                         select f.planeTypeID;
            if (planeType.Any())
                return true;
            return false;
        }

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
            var reservations = context.Reservations.Where(s => s.userID == user.userID).ToList();
            List<DAL.Reservation> listReserevations = new List<DAL.Reservation>(reservations);
            return listReserevations;
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
