using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data_Transfer_Objects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi
{
    [Route("api/reserve")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public TodoController(DAL.FlightContext context, ReserveContext context2)
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
                result.Add(new Flight_DTO());
                result[i].Departure = DAL_list[i].departure;
            }
            return result;
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<Flight_DTO> GetById(long id)
        {
            DAL.Flight temp = _context.Flights.Find(id);
            Flight_DTO result = new Flight_DTO();

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
            tempfl.departure = item.Departure;
            _context.Flights.Add(tempfl);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = tempfl.flightID }, item);
        }
    }
}
