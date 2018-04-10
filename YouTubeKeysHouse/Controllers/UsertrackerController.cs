using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouTubeKeysHouse.Models;

namespace YouTubeKeysHouse.Controllers
{
    [Produces("application/json")]
    [Route("api/Usertracker")]
    [EnableCors("MyPolicy")]
    public class UsertrackerController : Controller
    {
        private readonly TripsEraDbContext _context;

        public UsertrackerController(TripsEraDbContext context)
        {
            _context = context;
        }

        // GET: api/Usertracker
        [HttpGet]
        public IEnumerable<Usertracker> GetUsertracker()
        {
            return _context.Usertracker;
        }

        // GET: api/Usertracker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsertracker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usertracker = await _context.Usertracker.SingleOrDefaultAsync(m => m.Id == id);

            if (usertracker == null)
            {
                return NotFound();
            }

            return Ok(usertracker);
        }

        // PUT: api/Usertracker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsertracker([FromRoute] int id, [FromBody] Usertracker usertracker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usertracker.Id)
            {
                return BadRequest();
            }

            _context.Entry(usertracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsertrackerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usertracker
        [HttpPost]
        public async Task<IActionResult> PostUsertracker([FromBody] Usertracker usertracker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Usertracker.Add(usertracker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsertracker", new { id = usertracker.Id }, usertracker);
        }

        // DELETE: api/Usertracker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsertracker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usertracker = await _context.Usertracker.SingleOrDefaultAsync(m => m.Id == id);
            if (usertracker == null)
            {
                return NotFound();
            }

            _context.Usertracker.Remove(usertracker);
            await _context.SaveChangesAsync();

            return Ok(usertracker);
        }

        private bool UsertrackerExists(int id)
        {
            return _context.Usertracker.Any(e => e.Id == id);
        }
    }
}