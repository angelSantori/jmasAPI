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
    public class ColoniasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ColoniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Colonias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colonia>>> GetColonia()
        {
            return await _context.Colonia.ToListAsync();
        }

        // GET: api/Colonias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colonia>> GetColonia(int id)
        {
            var colonia = await _context.Colonia.FindAsync(id);

            if (colonia == null)
            {
                return NotFound();
            }

            return colonia;
        }

        // GET: api/Colonias/BuscarPorNombre?nombre={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Colonia>>> BuscarPorNombre([FromQuery] string nombreColonia)
        {
            if (string.IsNullOrWhiteSpace(nombreColonia))
            {
                return BadRequest("El párametro 'nombreColonia' es requerido");
            }
            return await _context.Colonia
                .Where(colonia => colonia.nombreColonia != null &&
                            colonia.nombreColonia.ToLower().Contains(nombreColonia.ToLower()))
                .Take(10)
                .ToListAsync();
        }

        // PUT: api/Colonias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColonia(int id, Colonia colonia)
        {
            if (id != colonia.idColonia)
            {
                return BadRequest();
            }

            _context.Entry(colonia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoniaExists(id))
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

        // POST: api/Colonias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colonia>> PostColonia(Colonia colonia)
        {
            _context.Colonia.Add(colonia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColonia", new { id = colonia.idColonia }, colonia);
        }

        // DELETE: api/Colonias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColonia(int id)
        {
            var colonia = await _context.Colonia.FindAsync(id);
            if (colonia == null)
            {
                return NotFound();
            }

            _context.Colonia.Remove(colonia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColoniaExists(int id)
        {
            return _context.Colonia.Any(e => e.idColonia == id);
        }
    }
}
