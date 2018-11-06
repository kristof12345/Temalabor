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
                if (!reservation.isDeleted)
                {
                    DTO.Reservation current = DataConversion.Reservation_DAL_to_DTO(reservation, _context);
                    result.Add(current);
                }
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetReservation")]
        public ActionResult<DTO.Reservation> GetById(long id)
        {
            DAL.Reservation temp = _context.Reservations.Find(id);
            if (temp == null || temp.isDeleted)
                return NotFound();
            DTO.Reservation result = DataConversion.Reservation_DAL_to_DTO(temp, _context);

            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.Reservation item)
        {
            var ifDeleted = _context.Reservations.Find(item.ReservationId);

            if (ifDeleted != null && ifDeleted.isDeleted)
            {             
                ifDeleted.isDeleted = false;               
                _context.SaveChanges();
                _context = Queries.reserveSeatsOnFlight(ifDeleted, _context);
                

                return CreatedAtRoute("GetReservation", new { id = ifDeleted.reservationID }, item);
            }
            else
            {
                DAL.Reservation tempfl = DataConversion.Reservation_DTO_to_DAL(item);
                _context.Reservations.Add(tempfl);
                _context = Queries.reserveSeatsOnFlight(tempfl, _context);
                _context.SaveChanges();
                

                return CreatedAtRoute("GetReservation", new { id = tempfl.reservationID }, item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.Reservation dtoReservation)
        {
            var todo = _context.Reservations.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            _context = Queries.unReserveSeatsOnFlight(todo, _context);

            todo.user = dtoReservation.User;
            todo.userID = dtoReservation.UserID;
            todo.flightID = dtoReservation.FlightId;
            todo.date = dtoReservation.Date;

            _context = Queries.reserveSeatsOnFlight(todo, _context);

            _context.Reservations.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Reservations.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            _context = Queries.unReserveSeatsOnFlight(todo, _context);
          
            todo.isDeleted = true;
            //_context.Reservations.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}