using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PlaneTypeController : Controller
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public PlaneTypeController(DAL.FlightContext context, ReserveContext context2)
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

        public PlaneType PlaneType_DAL_to_DTO(DAL.PlaneType dalPlaneType)
        {
            PlaneType temp = new PlaneType(dalPlaneType.planeType);

            var queriedSeats = _context.Seats.Where(s => s.planeTypeID == dalPlaneType.planeTypeID);

            List<Seat> dtoSeats = new List<Seat>();
            foreach (DAL.Seat seat in queriedSeats)
            {
                Seat current = Seat_DAL_to_DTO(seat);
                dtoSeats.Add(current);
            }

            return temp;
        }

        public DAL.PlaneType PlaneType_DTO_to_DAL(PlaneType dtoPlaneType)
        {
            DAL.PlaneType temp = new DAL.PlaneType();
            temp.planeType = dtoPlaneType.PlaneTypeName;
            return temp;
        }

        [HttpGet]
        public ActionResult<List<PlaneType>> GetAll()
        {
            var DAL_list = _context.PlaneTypes.ToList();
            List<PlaneType> result = new List<PlaneType>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                PlaneType current = PlaneType_DAL_to_DTO(DAL_list[i]);
                result.Add(current);
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetPlaneType")]
        public ActionResult<PlaneType> GetById(long id)
        {
            var temp = _context.PlaneTypes.Single(p => p.planeTypeID == id);
            PlaneType result = PlaneType_DAL_to_DTO(temp);

            if (temp == null)
                return NotFound();
            return result;
        }

        [HttpPost]
        public IActionResult Create(PlaneType item)
        {
            DAL.PlaneType tempfl = PlaneType_DTO_to_DAL(item);

            _context.PlaneTypes.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetSeat", new { id = tempfl.planeTypeID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, PlaneType item)
        {
            var todo = _context.PlaneTypes.Single(p => p.planeTypeID == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.planeType = item.PlaneTypeName;

            _context.PlaneTypes.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.PlaneTypes.Single(p => p.planeTypeID == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.PlaneTypes.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
