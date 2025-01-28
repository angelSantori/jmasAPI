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
    public class AlmacenesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlmacenesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Almacenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Almacenes>>> GetAlmacenes()
        {
            return await _context.Almacenes.ToListAsync();
        }

        // GET: api/Almacenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Almacenes>> GetAlmacenes(int id)
        {
            var almacenes = await _context.Almacenes.FindAsync(id);

            if (almacenes == null)
            {
                return NotFound();
            }

            return almacenes;
        }

        // PUT: api/Almacenes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmacenes(int id, Almacenes almacenes)
        {
            if (id != almacenes.Id_Almacen)
            {
                return BadRequest();
            }

            _context.Entry(almacenes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmacenesExists(id))
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

        // POST: api/Almacenes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Almacenes>> PostAlmacenes(Almacenes almacenes)
        {
            _context.Almacenes.Add(almacenes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlmacenes", new { id = almacenes.Id_Almacen }, almacenes);
        }

        // DELETE: api/Almacenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlmacenes(int id)
        {
            var almacenes = await _context.Almacenes.FindAsync(id);
            if (almacenes == null)
            {
                return NotFound();
            }

            _context.Almacenes.Remove(almacenes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlmacenesExists(int id)
        {
            return _context.Almacenes.Any(e => e.Id_Almacen == id);
        }
    }
}
