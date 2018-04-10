using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeKeysHouse.Models;

namespace YouTubeKeysHouse.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [EnableCors("MyPolicy")]
    public class YoutubekeysController : Controller
    {
        private readonly TripsEraDbContext _context;

        public YoutubekeysController(TripsEraDbContext context)
        {
            _context = context;
        }

        // GET: api/Youtubekeys
        [HttpGet]
        public IEnumerable<Youtubekeys> GetYoutubekeys()
        {
            return _context.Youtubekeys;
        }

        // GET: api/Youtubekeys/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetYoutubekeys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var youtubekeys = await _context.Youtubekeys.SingleOrDefaultAsync(m => m.Id == id);

            if (youtubekeys == null)
            {
                return NotFound();
            }

            return Ok(youtubekeys);
        }

        [HttpGet("{userName}")]
        public IEnumerable<Youtubekeys> GetYoutubekeysByUserName([FromRoute] string userName)
        {
            List<Youtubekeys> KeysRowList = new List<Youtubekeys>();
            if (string.IsNullOrEmpty(userName))
                return null;
            try
            {
                Usertracker user = _context.Usertracker.SingleOrDefault(m => m.Username == userName);
                KeysRowList = _context.Youtubekeys.AsNoTracking().Where(m => m.Id > user.KeysFrom).Take(user.KeysToFetch).ToList();

                if (KeysRowList.Count()>0) {
                    user.ActualKeysProcessed += KeysRowList.Count();
                    user.KeysFrom = KeysRowList[KeysRowList.Count() - 1].Id;
                    _context.Usertracker.Update(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            return KeysRowList;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutYoutubekeys([FromRoute] int id, [FromBody] Youtubekeys youtubekeys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != youtubekeys.Id)
            {
                return BadRequest();
            }

            _context.Entry(youtubekeys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YoutubekeysExists(id))
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

        //// POST: api/Youtubekeys
        //[HttpPost]
        //public async Task<IActionResult> PostYoutubekeys([FromBody] Youtubekeys youtubekeys)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Youtubekeys.Add(youtubekeys);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetYoutubekeys", new { id = youtubekeys.Id }, youtubekeys);
        //}
        
        [HttpPost]
        public async Task<IActionResult> PostYoutubekeys([FromBody] List<string> youtubekeys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var keyitem in youtubekeys)
            {
                _context.Youtubekeys.Add(new Youtubekeys()
                {
                    TheKey = keyitem,
                    KeyType = "V"
                });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Youtubekeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYoutubekeys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var youtubekeys = await _context.Youtubekeys.SingleOrDefaultAsync(m => m.Id == id);
            if (youtubekeys == null)
            {
                return NotFound();
            }

            _context.Youtubekeys.Remove(youtubekeys);
            await _context.SaveChangesAsync();

            return Ok(youtubekeys);
        }

        private bool YoutubekeysExists(int id)
        {
            return _context.Youtubekeys.Any(e => e.Id == id);
        }
    }
}