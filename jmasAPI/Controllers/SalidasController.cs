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
    public class SalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidas()
        {
            return await _context.Salidas.ToListAsync();
        }

        // GET: api/Salidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salidas>> GetSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);

            if (salidas == null)
            {
                return NotFound();
            }

            return salidas;
        }

        // GET: api/Salidas/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidasByFolio(string folio)
        {
            // Filtrar las entradas cuyo folio coincida con el valor proporcionado
            var salidas = await _context.Salidas
                .Where(e => e.Salida_Folio == folio)
                .ToListAsync();

            // Verificar si se encontraron registros
            if (salidas == null || salidas.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron entradas con el folio: {folio}" });
            }

            return Ok(salidas);
        }

        // PUT: api/Salidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalidas(int id, Salidas salidas)
        {
            if (id != salidas.Id_Salida)
            {
                return BadRequest();
            }

            _context.Entry(salidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidasExists(id))
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

        // POST: api/Salidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salidas>> PostSalidas(Salidas salidas)
        {
            _context.Salidas.Add(salidas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalidas", new { id = salidas.Id_Salida }, salidas);
        }

        // DELETE: api/Salidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);
            if (salidas == null)
            {
                return NotFound();
            }

            _context.Salidas.Remove(salidas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalidasExists(int id)
        {
            return _context.Salidas.Any(e => e.Id_Salida == id);
        }
    }
}
