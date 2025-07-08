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
    public class MedioOrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedioOrdenServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedioOrdenServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedioOrdenServicio>>> GetmedioOrdenServicio()
        {
            return await _context.medioOrdenServicio.ToListAsync();
        }

        // GET: api/MedioOrdenServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedioOrdenServicio>> GetMedioOrdenServicio(int id)
        {
            var medioOrdenServicio = await _context.medioOrdenServicio.FindAsync(id);

            if (medioOrdenServicio == null)
            {
                return NotFound();
            }

            return medioOrdenServicio;
        }

        // PUT: api/MedioOrdenServicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedioOrdenServicio(int id, MedioOrdenServicio medioOrdenServicio)
        {
            if (id != medioOrdenServicio.idMedio)
            {
                return BadRequest();
            }

            _context.Entry(medioOrdenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedioOrdenServicioExists(id))
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

        // POST: api/MedioOrdenServicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MedioOrdenServicio>> PostMedioOrdenServicio(MedioOrdenServicio medioOrdenServicio)
        {
            _context.medioOrdenServicio.Add(medioOrdenServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedioOrdenServicio", new { id = medioOrdenServicio.idMedio }, medioOrdenServicio);
        }

        // DELETE: api/MedioOrdenServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedioOrdenServicio(int id)
        {
            var medioOrdenServicio = await _context.medioOrdenServicio.FindAsync(id);
            if (medioOrdenServicio == null)
            {
                return NotFound();
            }

            _context.medioOrdenServicio.Remove(medioOrdenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedioOrdenServicioExists(int id)
        {
            return _context.medioOrdenServicio.Any(e => e.idMedio == id);
        }
    }
}
