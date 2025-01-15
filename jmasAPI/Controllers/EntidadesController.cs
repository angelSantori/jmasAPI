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
    public class EntidadesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EntidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Entidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entidades>>> GetEntidades()
        {
            return await _context.Entidades.ToListAsync();
        }

        // GET: api/Entidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entidades>> GetEntidades(int id)
        {
            var entidades = await _context.Entidades.FindAsync(id);

            if (entidades == null)
            {
                return NotFound();
            }

            return entidades;
        }

        // PUT: api/Entidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntidades(int id, Entidades entidades)
        {
            if (id != entidades.Id_Entidad)
            {
                return BadRequest();
            }

            _context.Entry(entidades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntidadesExists(id))
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

        // POST: api/Entidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entidades>> PostEntidades(Entidades entidades)
        {
            _context.Entidades.Add(entidades);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntidades", new { id = entidades.Id_Entidad }, entidades);
        }

        // DELETE: api/Entidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntidades(int id)
        {
            var entidades = await _context.Entidades.FindAsync(id);
            if (entidades == null)
            {
                return NotFound();
            }

            _context.Entidades.Remove(entidades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntidadesExists(int id)
        {
            return _context.Entidades.Any(e => e.Id_Entidad == id);
        }
    }
}
