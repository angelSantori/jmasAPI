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
    public class AjustesMasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AjustesMasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AjustesMas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AjustesMas>>> GetAjustesMas()
        {
            return await _context.AjustesMas.ToListAsync();
        }

        // GET: api/AjustesMas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AjustesMas>> GetAjustesMas(int id)
        {
            var ajustesMas = await _context.AjustesMas.FindAsync(id);

            if (ajustesMas == null)
            {
                return NotFound();
            }

            return ajustesMas;
        }

        //Get api/AjustesMas/nextFolioAJM
        [HttpGet("nextFolioAJM")]
        public async Task<ActionResult<string>> GetNextFolioAJM()
        {
            var lastAJM = await _context.AjustesMas
                .OrderByDescending(ajm => ajm.Id_AjusteMas)
                .FirstOrDefaultAsync();
            
            int nextNumber = lastAJM != null
                ? int.Parse(lastAJM.AjusteMas_CodFolio.Replace("AJM", "")) +1
                : 1;

            return Ok($"AJM{nextNumber}");
        }

        // PUT: api/AjustesMas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAjustesMas(int id, AjustesMas ajustesMas)
        {
            if (id != ajustesMas.Id_AjusteMas)
            {
                return BadRequest();
            }

            _context.Entry(ajustesMas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AjustesMasExists(id))
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

        // POST: api/AjustesMas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AjustesMas>> PostAjustesMas(AjustesMas ajustesMas)
        {
            _context.AjustesMas.Add(ajustesMas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAjustesMas", new { id = ajustesMas.Id_AjusteMas }, ajustesMas);
        }

        // DELETE: api/AjustesMas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAjustesMas(int id)
        {
            var ajustesMas = await _context.AjustesMas.FindAsync(id);
            if (ajustesMas == null)
            {
                return NotFound();
            }

            _context.AjustesMas.Remove(ajustesMas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AjustesMasExists(int id)
        {
            return _context.AjustesMas.Any(e => e.Id_AjusteMas == id);
        }
    }
}
