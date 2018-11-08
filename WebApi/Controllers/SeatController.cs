using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DAL;
using System.Diagnostics;

namespace WebApi.Controllers
{
    [Route("api/seat")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public SeatController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        [HttpGet]
        public ActionResult<List<DTO.Seat>> GetAll()
        {
            var DAL_list = _context.Seats.ToList();
            List<DTO.Seat> result = new List<DTO.Seat>();

            foreach (DAL.Seat seat in DAL_list)
            {
                if (!seat.isDeleted)
                {
                    DTO.Seat current = DataConversion.Seat_DAL_to_DTO(seat, _context);
                    result.Add(current);
                }
            }
            return result;
        }

        [HttpGet("flightID/{flightID}", Name = "GetAllSeatsForFlight")]
        public ActionResult<List<DTO.Seat>> GetAllSeatsForFlight(long flightID)
        {
            List<DTO.Seat> result = new List<DTO.Seat>();

            DAL.Flight tempFlight = _context.Flights.Find(flightID);

            List<DAL.Seat> queriedSeats = Queries.findSeatsForFlight(tempFlight, _context);

            foreach (DAL.Seat seat in queriedSeats)
            {
                if (!seat.isDeleted)
                {
                    DTO.Seat current = DataConversion.Seat_DAL_to_DTO(seat, _context);
                    result.Add(current);
                }
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetSeat")]
        public ActionResult<DTO.Seat> GetById(long id)
        {
            DAL.Seat temp = _context.Seats.Find(id);

            if (temp == null || temp.isDeleted)
                return NotFound();

            DTO.Seat result = DataConversion.Seat_DAL_to_DTO(temp, _context);
            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.Seat item)
        {
            //Debug.WriteLine("MEGHIVODIK");
            var ifDeleted = _context.Seats.Find(item.SeatId);          
            if (ifDeleted != null && ifDeleted.isDeleted)
            {
                ifDeleted.isDeleted = false;
                _context.SaveChanges();
                return CreatedAtRoute("GetSeat", new { id = ifDeleted.seatID }, item);
            }                      
            else
            {
                DAL.Seat tempfl = DataConversion.Seat_DTO_to_DAL(item);

                _context.Seats.Add(tempfl);
                _context.SaveChanges();

                return CreatedAtRoute("GetSeat", new { id = tempfl.seatID }, item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.Seat item)
        {
            DAL.Seat todo = _context.Seats.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.seatType = item.SeatType;
            todo.Xcord = item.Coordinates.X;
            todo.Ycord = item.Coordinates.Y;

            _context.Seats.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            DAL.Seat todo = _context.Seats.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.isDeleted = true;
            //_context.Seats.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}