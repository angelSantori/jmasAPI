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
    public class AjustesMenosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AjustesMenosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AjustesMenos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AjustesMenos>>> GetAjustesMenos()
        {
            return await _context.AjustesMenos.ToListAsync();
        }

        // GET: api/AjustesMenos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AjustesMenos>> GetAjustesMenos(int id)
        {
            var ajustesMenos = await _context.AjustesMenos.FindAsync(id);

            if (ajustesMenos == null)
            {
                return NotFound();
            }

            return ajustesMenos;
        }

        // PUT: api/AjustesMenos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAjustesMenos(int id, AjustesMenos ajustesMenos)
        {
            if (id != ajustesMenos.Id_AjusteMenos)
            {
                return BadRequest();
            }

            _context.Entry(ajustesMenos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AjustesMenosExists(id))
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

        // POST: api/AjustesMenos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AjustesMenos>> PostAjustesMenos(AjustesMenos ajustesMenos)
        {
            _context.AjustesMenos.Add(ajustesMenos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAjustesMenos", new { id = ajustesMenos.Id_AjusteMenos }, ajustesMenos);
        }

        // DELETE: api/AjustesMenos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAjustesMenos(int id)
        {
            var ajustesMenos = await _context.AjustesMenos.FindAsync(id);
            if (ajustesMenos == null)
            {
                return NotFound();
            }

            _context.AjustesMenos.Remove(ajustesMenos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AjustesMenosExists(int id)
        {
            return _context.AjustesMenos.Any(e => e.Id_AjusteMenos == id);
        }
    }
}
