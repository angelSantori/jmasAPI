using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CallesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Calles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calle>>> GetCalle()
        {
            return await _context.Calle.ToListAsync();
        }

        // GET: api/Calles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calle>> GetCalle(int id)
        {
            var calle = await _context.Calle.FindAsync(id);

            if (calle == null)
            {
                return NotFound();
            }

            return calle;
        }

        // PUT: api/Calles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalle(int id, Calle calle)
        {
            if (id != calle.idCalle)
            {
                return BadRequest();
            }

            _context.Entry(calle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalleExists(id))
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

        // POST: api/Calles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calle>> PostCalle(Calle calle)
        {
            _context.Calle.Add(calle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalle", new { id = calle.idCalle }, calle);
        }

        // DELETE: api/Calles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalle(int id)
        {
            var calle = await _context.Calle.FindAsync(id);
            if (calle == null)
            {
                return NotFound();
            }

            _context.Calle.Remove(calle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalleExists(int id)
        {
            return _context.Calle.Any(e => e.idCalle == id);
        }
    }
}
