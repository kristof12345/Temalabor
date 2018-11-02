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
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DAL.FlightContext _context;
        private readonly ReserveContext _context2;

        public UserController(DAL.FlightContext context, ReserveContext context2)
        {
            _context = context;
            _context2 = context2;
        }

        [HttpGet]
        public ActionResult<List<DTO.User>> GetAll()
        {
            var DAL_list = _context.Users.ToList();
            List<DTO.User> result = new List<DTO.User>();

            foreach (DAL.User user in DAL_list)
            {
                if (!user.isDeleted)
                {
                    DTO.User current = DataConversion.User_DAL_to_DTO(user);
                    result.Add(current);
                }
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<DTO.User> GetById(long id)
        {
            DAL.User temp = _context.Users.Find(id);
            if (temp == null || temp.isDeleted)
                return NotFound();

            DTO.User result = DataConversion.User_DAL_to_DTO(temp);
            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.User item)
        {
            var ifDeleted = _context.Users.Find(item.UserId);
            if (ifDeleted != null && ifDeleted.isDeleted)
            {
                ifDeleted.isDeleted = false;
                _context.SaveChanges();
                return CreatedAtRoute("GetFlight", new { id = ifDeleted.userID }, item);
            }
            else
            {
                DAL.User tempfl = DataConversion.User_DTO_to_DAL(item);
                _context.Users.Add(tempfl);
                _context.SaveChanges();
                return CreatedAtRoute("GetUser", new { id = tempfl.userID }, item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, DTO.User dtoUser)
        {
            var todo = _context.Users.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.name = dtoUser.Name;
            todo.password = dtoUser.Password;
            todo.userType = dtoUser.UserType;

            _context.Users.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Users.Find(id);
            if (todo == null || todo.isDeleted)
            {
                return NotFound();
            }

            todo.isDeleted = true;
            //_context.Users.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}