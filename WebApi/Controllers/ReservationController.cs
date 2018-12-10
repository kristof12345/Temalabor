using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DAL;
using WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Exceptions;

namespace WebApi
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private DAL.FlightContext _context;

        public ReservationController(DAL.FlightContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<DTO.Reservation>> GetAll()
        {
            var DAL_list = _context.Reservations.ToList();
            List<DTO.Reservation> result = new List<DTO.Reservation>();

            foreach (DAL.Reservation reservation in DAL_list)
            {
                DTO.Reservation current = DataConversion.Reservation_DAL_to_DTO(reservation, _context);
                result.Add(current);    
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public ActionResult<DTO.Reservation> GetById(long id)
        {
            if (id < 1)
                throw new InvalidIDException("Reservation ID is less than 1");
            DAL.Reservation temp = _context.Reservations.Find(id);
            if (temp == null)
                return NotFound();
            DTO.Reservation result = DataConversion.Reservation_DAL_to_DTO(temp, _context);

            return result;
        }

        [HttpGet("userID/{userID}", Name = "GetAllReservationsForUser")]
        public ActionResult<List<DTO.Reservation>> GetAllSeatsForFlight(long userID)
        {
            if (userID < 1)
                throw new InvalidIDException("User ID is less than 1");
            List<DTO.Reservation> result = new List<DTO.Reservation>();

            DAL.User tempUser = _context.Users.Find(userID);           

            List<DAL.Reservation> queriedReservations = Queries.findReservationsForUser(tempUser, _context);

            foreach (DAL.Reservation reservation in queriedReservations)
            {               
                DTO.Reservation current = DataConversion.Reservation_DAL_to_DTO(reservation, _context);
                result.Add(current);
            }

            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.Reservation item)
        {
            DAL.Reservation reservation = DataConversion.Reservation_DTO_to_DAL(item);
            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            DAL.Flight flight = Queries.findFlightforReservation(item, _context);

            DAL.ReservationDetails reservationDetails = new DAL.ReservationDetails();
            reservationDetails.departure = flight.departure;
            reservationDetails.destination = flight.destination;
            reservationDetails.reservationID = reservation.reservationID;
            _context.ReservationDetails.Add(reservationDetails);
            _context.SaveChanges();

            _context = Queries.AddRecordsToReservationSeat(item, _context, reservation.reservationID);
            _context.SaveChanges();

            return CreatedAtRoute("GetReservation", new { id = reservation.reservationID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.Reservation dtoReservation)
        {
            if (id < 1)
                throw new InvalidIDException("Reservation ID is less than 1");
            var todo = _context.Reservations.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.user = dtoReservation.UserName;
            todo.userID = dtoReservation.UserID;
            todo.flightID = dtoReservation.FlightId;
            todo.date = dtoReservation.Date;

            _context.Reservations.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id < 1)
                throw new InvalidIDException("Reservation ID is less than 1");
            var todo = _context.Reservations.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context = Queries.DeleteRecordsOfReservationSeat(_context, id);
            _context.Reservations.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}