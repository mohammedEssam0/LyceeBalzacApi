using System.Security.Claims;
using LyceeBalzacApi.Data;
using LyceeBalzacApi.data_models;
using LyceeBalzacApi.Response;
using LyceeBalzacApi.security;
using LyceeBalzacApi.security.passwordHashing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private JwtService _jwtService;
        private HashService _hashService;
        private LyceeBalzacApiContext _context { get; }

        public UsersController(LyceeBalzacApiContext context, JwtService jwtService,
            HashService hashService)
        {
            _context = context;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Users.Select(x => x.ToUserResponse()));
        }

        [HttpGet("{id}")]
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
        public IActionResult Post(User user)
        {
            if (_context.Users.FirstOrDefault(x => x.Email == user.Email) != null)
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
        public IActionResult Put(int id, [FromBody] GetUserResponse user)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.FirstName = user.firstName;
            userToUpdate.LastName = user.lastName;
            userToUpdate.PhoneNumber = user.phoneNumber;
            userToUpdate.Address = user.address;
            _context.SaveChanges();
            return Ok(userToUpdate.ToUserResponse());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));

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
        public IActionResult Activate(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToActivate = _context.Users.FirstOrDefault(x => x.Id == id);
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == idUser);
            if (userToActivate == null || currentUser == null)
            {
                return NotFound();
            }

            if (currentUser.Role != Role.Admin)
            {
                return Unauthorized("only admins can perform this action");
            }

            userToActivate.UserStatus = UserStatus.Active;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{id}/deactivate")]
        public IActionResult Deactivate(int id)
        {
            var idUser = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (idUser == null)
            {
                return Unauthorized("You are not authorized to perform this action");
            }

            var userToDeactivate = _context.Users.FirstOrDefault(x => x.Id == id);
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == idUser);
            if (userToDeactivate == null || currentUser == null)
            {
                return NotFound();
            }

            if (currentUser.Role != Role.Admin)
            {
                return Unauthorized("only admins can perform this action");
            }

            userToDeactivate.UserStatus = UserStatus.Inactive;
            _context.SaveChanges();
            return Ok();
        }
    }
}