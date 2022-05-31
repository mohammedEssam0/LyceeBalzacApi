using LyceeBalzacApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LyceeBalzacApi.data_models;
using LyceeBalzacApi.security;
using Microsoft.AspNetCore.Authorization;


namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Level1Controller : ControllerBase
    {
        private LyceeBalzacApiContext _context;
        private JwtService _jwtService;
        public Level1Controller(LyceeBalzacApiContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var level1 = _context.Level1.ToList();
            return Ok(level1);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var level1 = _context.Level1.Find(id);
            if (level1 == null)
            {
                return NotFound();
            }
            return Ok(level1);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] Level1 level1)
        {
            var id = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (id == null)
            {
                return Unauthorized();
            }
            level1.Entry_ID = id;
            _context.Level1.Add(level1);
            _context.SaveChanges();
            return Ok(level1);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Level1 level1)
        {
            var Entry_id = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (Entry_id == null)
            {
                return Unauthorized();
            }
            var level1ToUpdate = _context.Level1.Find(id);
            level1ToUpdate.Id = level1.Id;
            level1ToUpdate.ArabicName = level1.ArabicName;
            level1ToUpdate.EnglishName = level1.EnglishName;
            level1ToUpdate.Notes = level1.Notes;
            _context.SaveChanges();
            return Ok(level1ToUpdate);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var level1ToDelete = _context.Level1.Find(id);
            _context.Level1.Remove(level1ToDelete);
            _context.SaveChanges();
            return Ok(level1ToDelete);
        }
        
    }
}
