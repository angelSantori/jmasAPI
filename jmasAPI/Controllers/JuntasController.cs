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
    public class JuntasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JuntasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Juntas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juntas>>> GetJuntas()
        {
            return await _context.Juntas.ToListAsync();
        }

        // GET: api/Juntas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Juntas>> GetJuntas(int id)
        {
            var juntas = await _context.Juntas.FindAsync(id);

            if (juntas == null)
            {
                return NotFound();
            }

            return juntas;
        }

        // PUT: api/Juntas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuntas(int id, Juntas juntas)
        {
            if (id != juntas.Id_Junta)
            {
                return BadRequest();
            }

            _context.Entry(juntas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuntasExists(id))
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

        // POST: api/Juntas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Juntas>> PostJuntas(Juntas juntas)
        {
            _context.Juntas.Add(juntas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJuntas", new { id = juntas.Id_Junta }, juntas);
        }

        // DELETE: api/Juntas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuntas(int id)
        {
            var juntas = await _context.Juntas.FindAsync(id);
            if (juntas == null)
            {
                return NotFound();
            }

            _context.Juntas.Remove(juntas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuntasExists(int id)
        {
            return _context.Juntas.Any(e => e.Id_Junta == id);
        }
    }
}
