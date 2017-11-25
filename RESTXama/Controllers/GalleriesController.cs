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
    [Route("api/Galleries")]
    public class GalleriesController : Controller
    {
        private readonly ProjectDBContext _context;

        public GalleriesController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Galleries
        [HttpGet]
        public IEnumerable<Gallery> GetGallery()
        {
            return _context.Gallery;
        }

        // GET: api/Galleries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.Id == id);

            if (gallery == null)
            {
                return NotFound();
            }

            return Ok(gallery);
        }

        // PUT: api/Galleries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGallery([FromRoute] int id, [FromBody] Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gallery.Id)
            {
                return BadRequest();
            }

            _context.Entry(gallery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(id))
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

        // POST: api/Galleries
        [HttpPost]
        public async Task<IActionResult> PostGallery([FromBody] Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gallery.Add(gallery);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GalleryExists(gallery.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGallery", new { id = gallery.Id }, gallery);
        }

        // DELETE: api/Galleries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gallery = await _context.Gallery.SingleOrDefaultAsync(m => m.Id == id);
            if (gallery == null)
            {
                return NotFound();
            }

            _context.Gallery.Remove(gallery);
            await _context.SaveChangesAsync();

            return Ok(gallery);
        }

        private bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.Id == id);
        }
    }
}