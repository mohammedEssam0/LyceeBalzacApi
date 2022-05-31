using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LyceeBalzacApi.Data;
using LyceeBalzacApi.data_models;
using LyceeBalzacApi.request;
using LyceeBalzacApi.security;
using LyceeBalzacApi.security.passwordHashing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private LyceeBalzacApiContext _context;
        private JwtService _jwtService;
        private HashService _hashService;
        
        public AuthController(LyceeBalzacApiContext context, JwtService jwtService, HashService hashService)
        {
            _context = context;
            _jwtService = jwtService;
            _hashService = hashService;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginModel.email);
            if (user == null)
            {
                return BadRequest("Email not found");
            }

            var passwordHash = new PasswordHash(user.PasswordHash, user.Salt);
            if (!_hashService.Verify(loginModel.password, passwordHash))
            {
                return BadRequest("password incorrect");
            }
            var claims = new[]
            {
                new Claim("userId", user.Id.ToString())
            };
            var token = _jwtService.CreateToken(claims);
            return Ok(new { token });
        }
        
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if(_context.Users.FirstOrDefault(x=>x.Email == user.Email) != null)
            {
                return BadRequest("Email already exists");
            }
            user.UserStatus = UserStatus.Pending;
            var hashedPassword = _hashService.Hash(user.PasswordHash);
            user.PasswordHash = hashedPassword.password;
            user.Salt = hashedPassword.salt;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user.ToUserResponse());
        }
        
    }
}
