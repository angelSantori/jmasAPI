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
    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HerramientasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Herramientas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Herramienta>>> GetHerramienta()
        {
            return await _context.Herramienta.ToListAsync();
        }

        // GET: api/Herramientas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Herramienta>> GetHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);

            if (herramienta == null)
            {
                return NotFound();
            }

            return herramienta;
        }

        // GET: api/Herramientas/HtaPorNombre?nombreHta={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Herramienta>>> HtaPorNombre([FromQuery] string nombreHta)
        {
            if (string.IsNullOrWhiteSpace(nombreHta))
            {
                return BadRequest("El parámetro 'nombreHta' es requerido");
            }

            return await _context.Herramienta
                .Where(hta => hta.htaNombre != null &&
                              hta.htaNombre.ToLower().Contains(nombreHta.ToLower()))
                .Take(30)
                .ToListAsync();
        }


        // PUT: api/Herramientas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHerramienta(int id, Herramienta herramienta)
        {
            if (id != herramienta.idHerramienta)
            {
                return BadRequest();
            }

            _context.Entry(herramienta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerramientaExists(id))
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

        // POST: api/Herramientas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Herramienta>> PostHerramienta(Herramienta herramienta)
        {
            _context.Herramienta.Add(herramienta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHerramienta", new { id = herramienta.idHerramienta }, herramienta);
        }

        // DELETE: api/Herramientas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHerramienta(int id)
        {
            var herramienta = await _context.Herramienta.FindAsync(id);
            if (herramienta == null)
            {
                return NotFound();
            }

            _context.Herramienta.Remove(herramienta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HerramientaExists(int id)
        {
            return _context.Herramienta.Any(e => e.idHerramienta == id);
        }
    }
}
