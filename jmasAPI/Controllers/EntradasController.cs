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
    public class EntradasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntradasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Entradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entradas>>> GetEntradas()
        {
            return await _context.Entradas.ToListAsync();
        }

        // GET: api/Entradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entradas>> GetEntradas(int id)
        {
            var entradas = await _context.Entradas.FindAsync(id);

            if (entradas == null)
            {
                return NotFound();
            }

            return entradas;
        }

        // PUT: api/Entradas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradas(int id, Entradas entradas)
        {
            if (id != entradas.Id_Entradas)
            {
                return BadRequest();
            }

            _context.Entry(entradas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradasExists(id))
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

        // GET: api/Entradas/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<Entradas>>> GetEntradasByFolio(string folio)
        {
            // Filtrar las entradas cuyo folio coincida con el valor proporcionado
            var entradas = await _context.Entradas
                .Where(e => e.Entrada_Folio == folio)
                .ToListAsync();

            // Verificar si se encontraron registros
            if (entradas == null || entradas.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron entradas con el folio: {folio}" });
            }

            return Ok(entradas);
        }

        // POST: api/Entradas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entradas>> PostEntradas(Entradas entradas)
        {
            _context.Entradas.Add(entradas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradas", new { id = entradas.Id_Entradas }, entradas);
        }

        // DELETE: api/Entradas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradas(int id)
        {
            var entradas = await _context.Entradas.FindAsync(id);
            if (entradas == null)
            {
                return NotFound();
            }

            _context.Entradas.Remove(entradas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntradasExists(int id)
        {
            return _context.Entradas.Any(e => e.Id_Entradas == id);
        }
    }
}
