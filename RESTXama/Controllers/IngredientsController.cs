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
    [Route("api/Ingredients")]
    public class IngredientsController : Controller
    {
        private readonly ProjectDBContext _context;

        public IngredientsController(ProjectDBContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public IEnumerable<Ingredients> GetIngredients()
        {
            return _context.Ingredients;
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredients([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredients = await _context.Ingredients.SingleOrDefaultAsync(m => m.ProductId == id);

            if (ingredients == null)
            {
                return NotFound();
            }

            return Ok(ingredients);
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredients([FromRoute] int id, [FromBody] Ingredients ingredients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredients.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(ingredients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientsExists(id))
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

        // POST: api/Ingredients
        [HttpPost]
        public async Task<IActionResult> PostIngredients([FromBody] Ingredients ingredients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ingredients.Add(ingredients);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngredientsExists(ingredients.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIngredients", new { id = ingredients.ProductId }, ingredients);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredients([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredients = await _context.Ingredients.SingleOrDefaultAsync(m => m.ProductId == id);
            if (ingredients == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredients);
            await _context.SaveChangesAsync();

            return Ok(ingredients);
        }

        private bool IngredientsExists(int id)
        {
            return _context.Ingredients.Any(e => e.ProductId == id);
        }
    }
}