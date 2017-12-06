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
    [Route("api/Prices")]
    public class PricesController : Controller
    {
        private readonly ProjectDBContext _context;

        public PricesController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Prices
        [HttpGet]
        public IEnumerable<Prices> GetPrices()
        {
            return _context.Prices;
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public IActionResult GetPrices([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var prices = _context.Prices.Where(p => p.Id == id);
            

            if (prices == null)
            {
                return NotFound();
            }

            return  Ok(prices);
        }

        // PUT: api/Prices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrices([FromRoute] int id, [FromBody] Prices prices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prices.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(prices).State = EntityState.Modified;
           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PricesExists(id))
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

        // POST: api/Prices
        [HttpPost]
        public async Task<IActionResult> PostPrices([FromBody] Prices prices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Prices.Add(prices);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PricesExists(prices.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrices", new { id = prices.ProductId }, prices);
        }

        // DELETE: api/Prices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrices([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prices = await _context.Prices.SingleOrDefaultAsync(m => m.ProductId == id);
            if (prices == null)
            {
                return NotFound();
            }

            _context.Prices.Remove(prices);
            await _context.SaveChangesAsync();

            return Ok(prices);
        }

        private bool PricesExists(int id)
        {
            return _context.Prices.Any(e => e.ProductId == id);
        }
    }
}