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

namespace WebApi
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private DAL.FlightContext _context;
        private ReserveContext _context2;

        public ReservationController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;
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
            DAL.Reservation temp = _context.Reservations.Find(id);
            if (temp == null)
                return NotFound();
            DTO.Reservation result = DataConversion.Reservation_DAL_to_DTO(temp, _context);

            return result;
        }

        [HttpGet("userID/{userID}", Name = "GetAllReservationsForUser")]
        public ActionResult<List<DTO.Reservation>> GetAllSeatsForFlight(long userID)
        {
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
            //Debug.WriteLine("userID " + item.User);
            DAL.Reservation tempfl = DataConversion.Reservation_DTO_to_DAL(item);
            _context.Reservations.Add(tempfl);               
            _context.SaveChanges();

            _context = Queries.AddRecordsToReservationSeat(item, _context, tempfl.reservationID);
            _context.SaveChanges();

            return CreatedAtRoute("GetReservation", new { id = tempfl.reservationID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.Reservation dtoReservation)
        {
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