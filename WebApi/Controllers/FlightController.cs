using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using WebApi.Controllers;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public FlightController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;  
        }

        public Flight_DTO Flight_DAL_to_DTO(long fID, DateTime d, string dep, string dest,  int frSeats, DAL.PlaneType ptype, string st)
        {
            Flight_DTO temp = new Flight_DTO(frSeats);
            temp.FlightId = fID;
            temp.Departure = dep;
            temp.Date = d;
            temp.Destination = dest;
            temp.FreeSeats = frSeats;
            //temp.PlaneType = SeatController.PlaneType_DAL_to_DTO(ptype.planeTypeID, ptype.planeType, ptype.seats);
            temp.Status = st;

            return temp;
        }

        public DAL.Flight Flight_DTO_to_DAL(long fID, DateTime d, string dep, string dest, int frSeats, PlaneType_DTO ptype, string st)
        {
            DAL.Flight temp = new DAL.Flight();
            //temp.flightID = fID;
            temp.departure = dep;
            temp.date = d;
            temp.destination = dest;
            temp.freeSeats = frSeats;
            //temp.planeType = SeatController.PlaneType_DTO_to_DAL(ptype.PlaneTypeID, ptype.PlaneType, ptype.Seats);
            temp.status = st;
            temp.freeSeats = frSeats;
           
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
                    DAL_list[i].planeType, DAL_list[i].status);
                result.Add(current);             
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetFlight")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            DAL.Flight temp = _context.Flights.Find(id);
            if (temp == null)
                return NotFound();
            Flight_DTO result = Flight_DAL_to_DTO(temp.flightID, temp.date, temp.departure, temp.destination, temp.freeSeats, temp.planeType, temp.status);

            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            DAL.Flight tempfl = Flight_DTO_to_DAL(item.FlightId , item.Date, item.Departure, item.Destination, item.FreeSeats, item.PlaneType, item.Status);        
            _context.Flights.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetFlight", new { id = tempfl.flightID }, item);
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
            //todo.planeType = item.PlaneType;
          
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
