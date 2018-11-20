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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Exceptions;

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
                DTO.User current = DataConversion.User_DAL_to_DTO(user);
                result.Add(current);
            }

            return result;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<DTO.User> GetById(long id)
        {
            if (id < 1)
                throw new InvalidIDException("User ID is less than 1");
            DAL.User temp = _context.Users.Find(id);
            if (temp == null)
                return NotFound();

            DTO.User result = DataConversion.User_DAL_to_DTO(temp);
            return result;
        }

        [HttpPost]
        public IActionResult Create(DTO.User item)
        {
            DAL.User tempfl = DataConversion.User_DTO_to_DAL(item);
            _context.Users.Add(tempfl);
            _context.SaveChanges();
            return CreatedAtRoute("GetUser", new { id = tempfl.userID }, item);
        }

        [HttpPut]
        public ActionResult<Session> Update(DTO.User dtoUser)
        {
            Session session = new Session(dtoUser);
            session.Success = false;
            if (Queries.findUserName(dtoUser, _context))
            {               
                if(Queries.findUserPassword(dtoUser, _context))
                {
                    DAL.User user = Queries.findUser(dtoUser, _context);
                    dtoUser.UserId = user.userID;

                    var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, dtoUser.Name),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Email, dtoUser.Password),
                          new Claim("Foo", "Bar")
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nem tudom de kell legalabb 128b"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken("Login header token",
                          "Login header token",
                          claims,
                          expires: DateTime.Now.AddMinutes(30),
                          signingCredentials: creds);                  
                    session.Success = true;
                    session.Token = new JwtSecurityTokenHandler().WriteToken(token);                   
                }
            }
                
            /*
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.name = dtoUser.Name;
            todo.password = dtoUser.Password;
            todo.userType = dtoUser.UserType;

            _context.Users.Update(todo);
            _context.SaveChanges();
            */

            return session;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (id < 1)
                throw new InvalidIDException("User ID is less than 1");
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.Users.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}