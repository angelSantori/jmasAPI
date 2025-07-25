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
    public class EntrevistaPadronsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntrevistaPadronsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EntrevistaPadrons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntrevistaPadron>>> GetentrevistaPadron()
        {
            return await _context.entrevistaPadron.ToListAsync();
        }

        // GET: api/EntrevistaPadrons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntrevistaPadron>> GetEntrevistaPadron(int id)
        {
            var entrevistaPadron = await _context.entrevistaPadron.FindAsync(id);

            if (entrevistaPadron == null)
            {
                return NotFound();
            }

            return entrevistaPadron;
        }

        // GET: api/EntrevistaPadrons/ByOS/{idOS}
        [HttpGet("ByOS/{idOS}")]
        public async Task<ActionResult<IEnumerable<EntrevistaPadron>>> GetEPxOS(int idOS)
        {
            try
            {
                var entrevistas = await _context.entrevistaPadron
                    .Where(ep => ep.idOrdenServicio == idOS)
                    .OrderBy(ep => ep.idEntrevistaPadron)
                    .ToListAsync();

                if (entrevistas == null || entrevistas.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron entrevistas ligadas con el idOS: {idOS}" });
                }

                return Ok(entrevistas);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/EntrevistaPadrons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrevistaPadron(int id, EntrevistaPadron entrevistaPadron)
        {
            if (id != entrevistaPadron.idEntrevistaPadron)
            {
                return BadRequest();
            }

            _context.Entry(entrevistaPadron).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrevistaPadronExists(id))
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

        // POST: api/EntrevistaPadrons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EntrevistaPadron>> PostEntrevistaPadron(EntrevistaPadron entrevistaPadron)
        {
            _context.entrevistaPadron.Add(entrevistaPadron);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrevistaPadron", new { id = entrevistaPadron.idEntrevistaPadron }, entrevistaPadron);
        }

        // DELETE: api/EntrevistaPadrons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrevistaPadron(int id)
        {
            var entrevistaPadron = await _context.entrevistaPadron.FindAsync(id);
            if (entrevistaPadron == null)
            {
                return NotFound();
            }

            _context.entrevistaPadron.Remove(entrevistaPadron);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntrevistaPadronExists(int id)
        {
            return _context.entrevistaPadron.Any(e => e.idEntrevistaPadron == id);
        }
    }
}
