using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
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

        public Seat Seat_DAL_to_DTO(DAL.Seat dalSeat)
        {
            Seat temp = new Seat(dalSeat.businessID);

            temp.Reserved = dalSeat.IsReserved;
            temp.SeatType = dalSeat.seatType;
            temp.Price = dalSeat.price;

            Cord coord = new Cord(dalSeat.Xcord, dalSeat.Ycord);
            temp.Coordinates = coord;

            return temp;
        }

        public DAL.Seat Seat_DTO_to_DAL(Seat dtoSeat)
        {
            DAL.Seat temp = new DAL.Seat();

            var plane = _context.PlaneTypes.Single(i => i.planeTypeID == dtoSeat.SeatId);

            temp.planeTypeID = plane.planeTypeID;
            temp.businessID = dtoSeat.SeatId;
            temp.IsReserved = dtoSeat.Reserved;
            temp.seatType = dtoSeat.SeatType;
            temp.price = dtoSeat.Price;
            temp.Xcord = dtoSeat.Coordinates.X;
            temp.Ycord = dtoSeat.Coordinates.Y;

            return temp;
        }

        [HttpGet]
        public ActionResult<List<Seat>> GetAll()
        {
            var DAL_list = _context.Seats.ToList();
            List<Seat> result = new List<Seat>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                Seat current = Seat_DAL_to_DTO(DAL_list[i]);
                result.Add(current);
            }
            return result;
        }

        [HttpGet("flightID/{flightID}", Name = "GetAllSeatsForFlight")]
        public ActionResult<List<Seat>> GetAllSeatsForFlight(long flightID)
        {
            List<Seat> result = new List<Seat>();
            var queriedSeats = _context.Seats.Where(s => s.flightID == flightID);
                      
            foreach (DAL.Seat seat in queriedSeats)
            {
                Seat current = Seat_DAL_to_DTO(seat);
                
                result.Add(current);
            }
            return result;
        }

        /*
        [HttpGet("{id}", Name = "GetSeat")]
        public ActionResult<Seat> GetById(long id)
        {
            try { 
            var temp = _context.Seats.Single(p => p.businessID == id);
            Seat result = Seat_DAL_to_DTO(temp);

            if (temp == null)
                return NotFound();

            return result;
        }
        */

        [HttpPost]
        public IActionResult Create(Seat item)
        {
            DAL.Seat tempfl = Seat_DTO_to_DAL(item);

            _context.Seats.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetSeat", new { id = tempfl.seatID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Seat item)
        {
            var todo = _context.Seats.Single(p => p.businessID == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.businessID = item.SeatId;
            todo.seatType = item.SeatType;
            todo.IsReserved = item.Reserved;
            todo.price = item.Price;
            todo.Xcord = item.Coordinates.X;
            todo.Ycord = item.Coordinates.Y;

            _context.Seats.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Seats.Single(p => p.businessID == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Seats.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}