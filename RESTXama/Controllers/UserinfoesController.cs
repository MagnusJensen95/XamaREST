using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTXama.Models;

namespace RESTXama.Controllers
{
    [Produces("application/json")]
    [Route("api/Userinfoes")]
    public class UserinfoesController : Controller
    {
        private readonly ProjectDBContext _context;

        public UserinfoesController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Userinfoes
        [HttpGet]
        public IEnumerable<Userinfo> GetUserinfo()
        {
            return _context.Userinfo;
        }

        // GET: api/Userinfoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Id == id);

            if (userinfo == null)
            {
                return NotFound();
            }

            return Ok(userinfo);
        }

        // PUT: api/Userinfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserinfo([FromRoute] int id, [FromBody] Userinfo userinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userinfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(userinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserinfoExists(id))
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

        // POST: api/Userinfoes
        [HttpPost]
        public async Task<IActionResult> PostUserinfo([FromBody] Userinfo userinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Userinfo.Add(userinfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserinfoExists(userinfo.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserinfo", new { id = userinfo.Id }, userinfo);
        }

        // DELETE: api/Userinfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userinfo = await _context.Userinfo.SingleOrDefaultAsync(m => m.Id == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            _context.Userinfo.Remove(userinfo);
            await _context.SaveChangesAsync();

            return Ok(userinfo);
        }

        private bool UserinfoExists(int id)
        {
            return _context.Userinfo.Any(e => e.Id == id);
        }
    }
}