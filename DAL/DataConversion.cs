using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Diagnostics;

namespace DAL
{
    public static class DataConversion
    {
        public static Flight_DTO Flight_DAL_to_DTO(DAL.Flight dalFlight, FlightContext _context)
        {
            var planeType = _context.PlaneTypes.Single(i => i.planeTypeID == dalFlight.planeTypeID);

            Flight_DTO temp = new Flight_DTO(planeType.planeType, planeType.planeTypeID);
            temp.FlightId = dalFlight.flightID;
            temp.Departure = dalFlight.departure;
            temp.Date = dalFlight.date;
            temp.Destination = dalFlight.destination;
            temp.Status = dalFlight.status;
            temp.PlaneTypeName = planeType.planeType;
            temp.NormalPrice = dalFlight.normalPrice;
            temp.FirstClassPrice = dalFlight.firstClassPrice;

            return temp;
        }

        public static DAL.Flight Flight_DTO_to_DAL(DTO.Flight_DTO dtoFlight)
        {
            DAL.Flight temp = new DAL.Flight();
            temp.departure = dtoFlight.Departure;
            temp.date = dtoFlight.Date;
            temp.destination = dtoFlight.Destination;
            temp.status = dtoFlight.Status;
            temp.planeTypeID = dtoFlight.PlaneTypeID;
            temp.normalPrice = dtoFlight.NormalPrice;
            temp.firstClassPrice = dtoFlight.FirstClassPrice;
            temp.isDeleted = false;

            return temp;
        }

        public static DTO.Seat Seat_DAL_to_DTO(DAL.Seat dalSeat)
        {
            DTO.Seat temp = new DTO.Seat(dalSeat.seatID);

            temp.SeatId = dalSeat.seatID;
            temp.Reserved = dalSeat.IsReserved;
            temp.SeatType = dalSeat.seatType;
            temp.PlaneTypeId = dalSeat.planeTypeID;

            Cord coord = new Cord(dalSeat.Xcord, dalSeat.Ycord);
            temp.Coordinates = coord;

            return temp;
        }

        public static DAL.Seat Seat_DTO_to_DAL(DTO.Seat dtoSeat)
        {
            DAL.Seat temp = new DAL.Seat();

            temp.planeTypeID = dtoSeat.PlaneTypeId;
            temp.IsReserved = dtoSeat.Reserved;
            temp.seatType = dtoSeat.SeatType;
            temp.Xcord = dtoSeat.Coordinates.X;
            temp.Ycord = dtoSeat.Coordinates.Y;

            return temp;
        }

        public static DTO.PlaneType PlaneType_DAL_to_DTO(DAL.PlaneType dalPlaneType, FlightContext _context)
        {
            DTO.PlaneType temp = new DTO.PlaneType(dalPlaneType.planeType, dalPlaneType.planeTypeID);

            var queriedSeats = _context.Seats.Where(s => s.planeTypeID == dalPlaneType.planeTypeID);
            List<DTO.Seat> dtoSeats = new List<DTO.Seat>();
            foreach (DAL.Seat seat in queriedSeats)
            {
                dtoSeats.Add(DataConversion.Seat_DAL_to_DTO(seat));
            }
            temp.Seats = dtoSeats;

            return temp;
        }

        public static DAL.PlaneType PlaneType_DTO_to_DAL(DTO.PlaneType dtoPlaneType)
        {
            DAL.PlaneType temp = new DAL.PlaneType();
            temp.planeType = dtoPlaneType.PlaneTypeName;
            return temp;
        }

        public static DTO.Reservation Reservation_DAL_to_DTO(DAL.Reservation dalReservation, FlightContext _context)
        {
            DTO.Reservation temp = new DTO.Reservation(dalReservation.flightID);

            var queriedSeats = _context.Seats.Where(s => s.reservationID == dalReservation.reservationID);
            List<long> seatsIDs = new List<long>();
            foreach (DAL.Seat seat in queriedSeats)
            {                
                seatsIDs.Add(seat.seatID);               
            }
            temp.SeatList = seatsIDs;

            temp.ReservationID = dalReservation.reservationID;
            temp.User = dalReservation.user;
            temp.UserID = dalReservation.userID;
            temp.FlightId = dalReservation.flightID;
            temp.Date = dalReservation.date;
            temp.Cost = dalReservation.cost;

            return temp;
        }

        public static DAL.Reservation Reservation_DTO_to_DAL(DTO.Reservation dtoReservation)
        {
            DAL.Reservation temp = new DAL.Reservation();
          
            temp.user = dtoReservation.User;
            temp.userID = dtoReservation.UserID;
            temp.flightID = dtoReservation.FlightId;
            temp.date = dtoReservation.Date;
            temp.cost = dtoReservation.Cost;

            return temp;
        }

        public static DTO.User User_DAL_to_DTO(DAL.User dalUser)
        {
            DTO.User temp = new DTO.User(dalUser.name, dalUser.password);
            temp.UserType = dalUser.userType;
            return temp;
        }

        public static DAL.User User_DTO_to_DAL(DTO.User dtoUser)
        {
            DAL.User temp = new DAL.User();
            temp.name = dtoUser.Name;
            temp.password = dtoUser.Password;
            temp.userType = dtoUser.UserType;
            return temp;
        }
    }
}
