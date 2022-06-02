using LyceeBalzacApi.Data;
using LyceeBalzacApi.data_models;
using LyceeBalzacApi.security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace LyceeBalzacApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Level2Controller : ControllerBase
    {
        private LyceeBalzacApiContext _context;
        private  JwtService _jwtService;
        
        public Level2Controller(LyceeBalzacApiContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var level2 = _context.Level2.ToList();
            return Ok(level2);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var level2 = _context.Level2.Find(id);
            if (level2 == null)
            {
                return NotFound();
            }
            return Ok(level2);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Level2 level2)
        {
            var id = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (id == null)
            {
                return Unauthorized();
            }
            var Level1id = _context.Level1.Find(level2.Level1Id);
            var LastId = _context.Level2.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Level1Id == Level1id.Id);
            if (Level1id == null)
            {
                return NotFound();
            }
            else if(LastId == null)
            {

                 level2.Id = Convert.ToInt16(Level1id.Id + $"1");

            }
            else
            {
                level2.Id = LastId.Id + 1;
            }
            _context.Level2.Add(level2);
            _context.SaveChanges();
            return Ok(level2);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Level2 level2)
        {
            var Entry_id = _jwtService.GetUserIdFromToken(Request.Headers["Authorization"].ToString().Substring(7));
            if (Entry_id == null)
            {
                return Unauthorized();
            }
            var level2ToUpdate = _context.Level2.Find(id);
            level2ToUpdate.Id = level2.Id;
            level2ToUpdate.Level2_Name_A = level2.Level2_Name_A;
            level2ToUpdate.Level2_Name_E = level2.Level2_Name_E;
            level2ToUpdate.Notes = level2.Notes;
            _context.SaveChanges();
            return Ok(level2ToUpdate);
        }
        [HttpDelete("{id}")]
        
        public IActionResult Delete(int id)
        {
            var level2ToDelete = _context.Level2.Find(id);
            _context.Level2.Remove(level2ToDelete);
            _context.SaveChanges();
            return Ok(level2ToDelete);
        }     
    }
}
