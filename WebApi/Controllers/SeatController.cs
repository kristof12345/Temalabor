using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;

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

        public static Seat Seat_DAL_to_DTO(long sID, bool rsv, String typ, int prc, int x, int y)
        {
            Seat temp = new Seat(sID);

            temp.Reserved = rsv;
            temp.SeatType = typ;
            temp.Price = prc;
            temp.Coordinates.X = x;
            temp.Coordinates.Y = y;

            return temp;
        }

        public static DAL.Seat Seat_DTO_to_DAL(long sID, bool rsv, String typ, int prc, int x, int y)
        {
            DAL.Seat temp = new DAL.Seat();

            temp.IsReserved = rsv;
            temp.seatType = typ;
            temp.price = prc;
            temp.Xcord = x;
            temp.Ycord = y;

            return temp;
        }

        public static DAL.PlaneType PlaneType_DTO_to_DAL(long ID, string ptype)
        {
            DAL.PlaneType temp = new DAL.PlaneType();

            //temp.PlaneTypeID = ID;         
            return temp;
        }

        [HttpGet]
        public ActionResult<List<Seat>> GetAll()
        {
            var DAL_list = _context.Seats.ToList();
            List<Seat> result = new List<Seat>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                Seat current = Seat_DAL_to_DTO(DAL_list[i].seatID, DAL_list[i].IsReserved, DAL_list[i].seatType, DAL_list[i].price, DAL_list[i].Xcord, DAL_list[i].Ycord);
                result.Add(current);
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetSeat")]
        public ActionResult<Seat> GetById(long id)
        {
            DAL.Seat temp = _context.Seats.Find(id);
            Seat result = Seat_DAL_to_DTO(temp.seatID, temp.IsReserved, temp.seatType, temp.price, temp.Xcord, temp.Ycord);

            if (temp == null)
                return NotFound();
            return result;
        }

        [HttpPost]
        public IActionResult Create(Seat item)
        {
            DAL.Seat tempfl = Seat_DTO_to_DAL(item.SeatId, item.Reserved, item.SeatType, item.Price, item.Coordinates.X, item.Coordinates.Y);

            _context.Seats.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetSeat", new { id = tempfl.seatID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Seat item)
        {
            var todo = _context.Seats.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

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
            var todo = _context.Seats.Find(id);
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