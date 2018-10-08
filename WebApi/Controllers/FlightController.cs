using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using WebApi.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//MOST VÁLTOZTATTAM VALAMIT
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

        //Ha a DAL.Seat-ben bennmarad a flightID, akkor azt is át kéne írni
        public Flight_DTO Flight_DAL_to_DTO(DAL.Flight dalFlight)
        {
            var plane = _context.PlaneTypes.Single(i => i.planeTypeID == dalFlight.planeTypeID);

            Flight_DTO temp = new Flight_DTO(plane.planeType);

            temp.FlightId = dalFlight.flightID; 
            temp.DatabaseId = dalFlight.businessID;
            temp.Departure = dalFlight.departure;
            temp.Date = dalFlight.date;
            temp.Destination = dalFlight.destination;
            temp.Status = dalFlight.status;
            temp.PlaneTypeName = plane.planeType;

            DTO.PlaneType dtoPlaneType = new PlaneType(plane.planeType);
            temp.PlaneType = dtoPlaneType;

            return temp;
        }

        public DAL.Flight Flight_DTO_to_DAL(DTO.Flight_DTO dtoFlight)
        {
            DAL.Flight temp = new DAL.Flight();
            //temp.flightID = fID;
            temp.businessID = dtoFlight.DatabaseId;
            temp.departure = dtoFlight.Departure;
            temp.date = dtoFlight.Date;
            temp.destination = dtoFlight.Destination;
            temp.status = dtoFlight.Status;

            var plane = _context.PlaneTypes.Single(i => i.planeType.Equals(dtoFlight.PlaneTypeName));

            plane.planeType = dtoFlight.PlaneTypeName;            

            //var seats = _context.Seats.Where(s => s.planeTypeID == plane.planeTypeID);
              //  temp.numberofSeats = seats.ToList().Count;
                //temp.freeSeats = seats.ToList().Count; // egyelőre csak így
                //temp.planeType = plane;
                  
            return temp;
        }

        [HttpGet]
        public ActionResult<List<Flight_DTO>> GetAll()
        {
            var DAL_list = _context.Flights.ToList();
            List<Flight_DTO> result = new List<Flight_DTO>();
            for (int i = 0; i < DAL_list.Count; i++)
            {
                Flight_DTO current = Flight_DAL_to_DTO(DAL_list[i]);
                //current.PlaneTypeName = current.PlaneType.PlaneTypeName;
                result.Add(current);
                //Debug.WriteLine("Server1: " + current.PlaneType.PlaneTypeName);
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetFlight")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            //DAL.Flight temp = _context.Flights.Find(id);
            var temp = _context.Flights.Single(p => p.businessID == id);
            if (temp == null)
                return NotFound();
            Flight_DTO result = Flight_DAL_to_DTO(temp);

            return result;
        }

        [HttpPost]
        public IActionResult Create(Flight_DTO item)
        {
            DAL.Flight tempfl = Flight_DTO_to_DAL(item);
            tempfl.planeType.planeType = item.PlaneTypeName;
            _context.Flights.Add(tempfl);
            //try 
            {
                _context.SaveChanges();
            }
            //catch (Exception) { Debug.WriteLine("HIBA A FLIGHTCONTROLLERBEN 2"); }

            var ret = CreatedAtRoute("GetFlight", new { id = tempfl.flightID }, item);
            return ret;
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Flight_DTO dtoFlight)
        {
            //var todo = _context.Flights.Find(id);
            var todo = _context.Flights.Single(p => p.businessID == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.businessID = dtoFlight.DatabaseId;
            todo.date = dtoFlight.Date;
            todo.departure = dtoFlight.Departure;
            todo.destination = dtoFlight.Destination;
            todo.status = dtoFlight.Status;

            var plane = _context.PlaneTypes.Single(i => i.planeType.Equals(dtoFlight.PlaneTypeName));
            todo.planeTypeID = plane.planeTypeID;

            _context.Flights.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            //try 
            {
                //var todo = _context.Flights.Find(id);
                var todo = _context.Flights.Single(p => p.businessID == id);
                if (todo == null)
                {
                    return NotFound();
                }

                _context.Flights.Remove(todo);
                _context.SaveChanges();
            }
            //catch (Exception) { Debug.WriteLine("HIBA A FLIGHTCONTROLLERBEN 3"); }
        return NoContent();
        }
    }
}