using LyceeBalzacApi.Data;
using LyceeBalzacApi.data_models;
using LyceeBalzacApi.security;
using LyceeBalzacApi.security.passwordHashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private JwtService _jwtService;
        private HashService _hashService;
        private LyceeBalzacApiContext _context { get; }

        public UsersController(LyceeBalzacApiContext context, IConfiguration configuration, JwtService jwtService, HashService hashService)
        {
            _context = context;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id}")]
        //[Authorize]
        public IActionResult Get(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user.ToUserResponse());
        }

        [HttpPost]
        ////[Authorize]
        public IActionResult Post(User user)
        {
            if(_context.Users.First(x=>x.Email == user.Email) != null)
            {
                return BadRequest("Email already exists");
            }
            user.UserStatus = UserStatus.Pending;
            var hashedPassword = _hashService.Hash(user.PasswordHash);
            user.PasswordHash = hashedPassword.password;
            user.Salt = hashedPassword.salt;
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"]);
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            if (idUser != user.Id)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();
            return Ok(userToUpdate);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult Delete(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"]);

            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToDelete = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToDelete == null)
            {
                return NotFound();
            }

            if (userToDelete.Role != Role.Admin)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/activate")]
        //[Authorize]
        public IActionResult Activate(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"]);
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToActivate = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToActivate == null)
            {
                return NotFound();
            }

            if (userToActivate.Role != Role.Admin)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            userToActivate.UserStatus = UserStatus.Active;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/deactivate")]
        //[Authorize]
        public IActionResult Deactivate(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"]);
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToDeactivate = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToDeactivate == null)
            {
                return NotFound();
            }

            if (userToDeactivate.Role != Role.Admin)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            userToDeactivate.UserStatus = UserStatus.Inactive;
            _context.SaveChanges();
            return Ok();
        }
    }
}