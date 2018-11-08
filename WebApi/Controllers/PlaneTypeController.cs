﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DAL;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/planetype")]
    [ApiController]
    public class PlaneTypeController : Controller
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public PlaneTypeController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        //Ez direkt ad string listát!
        [HttpGet]
        public ActionResult<List<String>> GetAll()
        {
            var DAL_list = _context.PlaneTypes.ToList();
            List<String> result = new List<String>();

            foreach (DAL.PlaneType planeType in DAL_list)
            {               
                DTO.PlaneType current = DataConversion.PlaneType_DAL_to_DTO(planeType, _context);
                result.Add(current.PlaneTypeName);
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetPlaneTypeName")]
        public ActionResult<DTO.PlaneType> GetById(long id)
        {
            DAL.PlaneType temp = _context.PlaneTypes.Find(id);

            if (temp == null)
                return NotFound();

            DTO.PlaneType result = DataConversion.PlaneType_DAL_to_DTO(temp, _context);

            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.PlaneType item)
        {          
            DAL.PlaneType tempfl = DataConversion.PlaneType_DTO_to_DAL(item);
            _context.PlaneTypes.Add(tempfl);
            _context.SaveChanges();
            return CreatedAtRoute("GetPlaneType", new { id = tempfl.planeTypeID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.PlaneType item)
        {
            var todo = _context.PlaneTypes.Find(id);
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
            var todo = _context.PlaneTypes.Find(id);
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
