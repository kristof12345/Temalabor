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
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

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

                foreach (long seatID in item.SeatList)
                {
                    var seat = _context.Seats.Find(seatID);
                    if (seat != null)
                    {
                        seat.reservationID = item.ReservationId;
                        seat.IsReserved = true;
                        _context.Seats.Update(seat);
                        _context.SaveChanges();
                    }
                }

                return CreatedAtRoute("GetReservation", new { id = ifDeleted.reservationID }, item);
            }
            else
            {
                DAL.Reservation tempfl = DataConversion.Reservation_DTO_to_DAL(item);
                _context.Reservations.Add(tempfl);
                _context.SaveChanges();

                foreach (long seatID in item.SeatList)
                {
                    var seat = _context.Seats.Find(seatID);
                    if (seat != null)
                    {
                        seat.reservationID = tempfl.reservationID;
                        seat.IsReserved = true;
                        _context.Seats.Update(seat);
                        _context.SaveChanges();
                    }
                }                       
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

            todo.user = dtoReservation.User;
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
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            var seats = Queries.findSeatsForReservation(todo, _context);
            foreach (DAL.Seat seat in seats)
            {
                seat.reservationID = 0;
                seat.IsReserved = false;
            }
          
            todo.isDeleted = true;
            //_context.Reservations.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}