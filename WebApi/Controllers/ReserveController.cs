using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;

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

            /* if (_context.Flights.Count() == 0)
             {
                 // Create a new TodoItem if collection is empty,
                 // which means you can't delete all TodoItems.
                 _context.Flights.Add(new ConsoleApp1.Flight { departure = "Item1" });
                 _context.SaveChanges();
             }*/
        }

        [HttpGet]
        public ActionResult<List<Flight_DTO>> GetAll()
        {
            var DAL_list = _context.Flights.ToList();
            List<Flight_DTO> result = new List<Flight_DTO>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                result.Add(new Flight_DTO(DAL_list[i].freeSeats));
                result[i].Departure = DAL_list[i].departure;
                result[i].Date = DAL_list[i].date;
                result[i].Destination = DAL_list[i].destination;
                result[i].FreeSeats = DAL_list[i].freeSeats;
                result[i].PlaneType = DAL_list[i].planeType;
                result[i].Status = DAL_list[i].status;
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetReserve")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            DAL.Flight temp = _context.Flights.Find(id);
            Flight_DTO result = new Flight_DTO(temp.freeSeats);

            if (temp == null)
            {
                return NotFound();
            }
            else
                result.Departure = temp.departure;
            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            DAL.Flight tempfl = new DAL.Flight();
            tempfl.flightID = (int)item.FlightId;
            tempfl.departure = item.Departure;
            tempfl.destination = item.Destination;
            tempfl.date = item.Date;
            tempfl.status = item.Status;
            //TODO: a székeket nem tudom, hogy hogy kezeled az még kell
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
