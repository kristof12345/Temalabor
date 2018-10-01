using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using WebApi.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi
{
    [Route("api/reserve")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public ReserveController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;  
        }

        public Flight_DTO Flight_DAL_to_DTO(long fID, DateTime d, string dep, string dest,  int frSeats, string ptype, string st, List<DAL.Seat> s)
        {
            Flight_DTO temp = new Flight_DTO(frSeats);
            temp.FlightId = fID;
            temp.Departure = dep;
            temp.Date = d;
            temp.Destination = dest;
            temp.FreeSeats = frSeats;
            temp.PlaneType = ptype;
            temp.Status = st;

            if (s != null)
            {
                for (int j = 0; j < s.Count; j++)
                {
                    Seat tempS = SeatController.Seat_DAL_to_DTO(s[j].seatID, s[j].IsReserved, s[j].seatType, s[j].price, s[j].Xcord, s[j].Ycord);
                    temp.Seats.Add(tempS);
                }
            }
            return temp;
        }

        public DAL.Flight Flight_DTO_to_DAL(long fID, DateTime d, string dep, string dest, int frSeats, string ptype, string st, List<Seat> s)
        {
            DAL.Flight temp = new DAL.Flight();
            //temp.flightID = fID;
            temp.departure = dep;
            temp.date = d;
            temp.destination = dest;
            temp.freeSeats = frSeats;
            temp.planeType = ptype;
            temp.status = st;
            temp.freeSeats = frSeats;

            if (s != null)
            {
                for (int j = 0; j < s.Count; j++)
                {
                    DAL.Seat tempS = SeatController.Seat_DTO_to_DAL(s[j].SeatId, s[j].Reserved, s[j].SeatType, s[j].Price, s[j].Coordinates.X, s[j].Coordinates.Y);
                    temp.seats.Add(tempS);
                }
            }

            return temp;
        }

        [HttpGet]
        public ActionResult<List<Flight_DTO>> GetAll()
        {
            var DAL_list = _context.Flights.ToList();
            List<Flight_DTO> result = new List<Flight_DTO>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                Flight_DTO current = Flight_DAL_to_DTO(DAL_list[i].flightID, DAL_list[i].date, DAL_list[i].departure, DAL_list[i].destination, DAL_list[i].freeSeats, 
                    DAL_list[i].planeType, DAL_list[i].status, DAL_list[i].seats);
                result.Add(current);             
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetReserve")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            DAL.Flight temp = _context.Flights.Find(id);
            if (temp == null)
                return NotFound();
            Flight_DTO result = Flight_DAL_to_DTO(temp.flightID, temp.date, temp.departure, temp.destination, temp.freeSeats, temp.planeType, temp.status, temp.seats);

            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            DAL.Flight tempfl = Flight_DTO_to_DAL(item.FlightId , item.Date, item.Departure, item.Destination, item.FreeSeats, item.PlaneType, item.Status, item.Seats);        
            _context.Flights.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetReserve", new { id = tempfl.flightID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Flight_DTO item)
        {
            var todo = _context.Flights.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.date = item.Date;
            todo.departure = item.Departure;
            todo.destination = item.Destination;
            todo.status = item.Status;
            todo.freeSeats = item.FreeSeats;
            todo.planeType = item.PlaneType;

            if (item.Seats != null) //Különben nem működne. Kristóf
            {
                for (int j = 0; j < item.Seats.Count; j++)
                {
                    DAL.Seat tempS = SeatController.Seat_DTO_to_DAL(item.Seats[j].SeatId, item.Seats[j].Reserved, item.Seats[j].SeatType, item.Seats[j].Price,
                        item.Seats[j].Coordinates.X, item.Seats[j].Coordinates.Y);
                    todo.seats.Add(tempS);
                }
            }
            _context.Flights.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Flights.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
