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

        [HttpGet]
        public ActionResult<List<Flight_DTO>> GetAll()
        {
            var DAL_list = _context.Flights.ToList();
            List<Flight_DTO> result = new List<Flight_DTO>();

            foreach (DAL.Flight flight in DAL_list)
            {
                if (!flight.isDeleted)
                {
                    Flight_DTO current = DataConversion.Flight_DAL_to_DTO(flight, _context);
                    result.Add(current);
                }
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetFlight")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            DAL.Flight temp = _context.Flights.Find(id);
            if (temp == null || temp.isDeleted)
                return NotFound();
            Flight_DTO result = DataConversion.Flight_DAL_to_DTO(temp, _context);

            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            var ifDeleted = _context.Flights.Find(item.FlightId);
            if (ifDeleted != null && ifDeleted.isDeleted)
            {
                ifDeleted.isDeleted = false;
                _context.SaveChanges();
                return CreatedAtRoute("GetFlight", new { id = ifDeleted.flightID }, item);
            }
            else
            {
                DAL.Flight tempfl = DataConversion.Flight_DTO_to_DAL(item, _context);
                _context.Flights.Add(tempfl);
                _context.SaveChanges();
                return CreatedAtRoute("GetFlight", new { id = tempfl.flightID }, item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Flight_DTO dtoFlight)
        {
            var todo = _context.Flights.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.date = dtoFlight.Date;
            todo.departure = dtoFlight.Departure;
            todo.destination = dtoFlight.Destination;
            todo.status = dtoFlight.Status;
            //todo.firstClassPrice = dtoFlight.FirstClassPrice;
            //todo.normalPrice = dtoFlight.NormalPrice;
            todo.planeTypeID = dtoFlight.PlaneTypeID;

            _context.Flights.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Flights.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.isDeleted = true;
            //_context.Flights.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}