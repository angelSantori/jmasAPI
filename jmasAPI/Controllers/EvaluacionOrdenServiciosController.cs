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
    public class EvaluacionOrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionOrdenServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/evaluacionOrdenServicioes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenServicio>>> GetevaluacionOrdenServicio()
        {
            return await _context.evaluacionOrdenServicio.ToListAsync();
        }

        // GET: api/evaluacionOrdenServicioes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionOrdenServicio>> GetevaluacionOrdenServicio(int id)
        {
            var evaluacionOrdenServicio = await _context.evaluacionOrdenServicio.FindAsync(id);

            if (evaluacionOrdenServicio == null)
            {
                return NotFound();
            }

            return evaluacionOrdenServicio;
        }

        // GET: api/evaluacionOrdenServicioes/ByOT/{idOT}
        [HttpGet("ByOS/{idOS}")]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenServicio>>> GetEOSxidOS(int idOS)
        {
            try
            {
                var evaluaciones = await _context.evaluacionOrdenServicio
                    .Where(ev => ev.idOrdenServicio == idOS)
                    .OrderBy(ev => ev.idEvaluacionOrdenServicio)
                    .ToListAsync();

                if (evaluaciones == null || evaluaciones.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron evaluaciones ligadas con el idOS: {idOS}" });
                }

                return Ok(evaluaciones);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
 
        // PUT: api/evaluacionOrdenServicioes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutevaluacionOrdenServicio(int id, EvaluacionOrdenServicio evaluacionOrdenServicio)
        {
            if (id != evaluacionOrdenServicio.idEvaluacionOrdenServicio)
            {
                return BadRequest();
            }

            _context.Entry(evaluacionOrdenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!evaluacionOrdenServicioExists(id))
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

        // POST: api/evaluacionOrdenServicioes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluacionOrdenServicio>> PostevaluacionOrdenServicio(EvaluacionOrdenServicio evaluacionOrdenServicio)
        {
            _context.evaluacionOrdenServicio.Add(evaluacionOrdenServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetevaluacionOrdenServicio", new { id = evaluacionOrdenServicio.idEvaluacionOrdenServicio}, evaluacionOrdenServicio);
        }

        // DELETE: api/evaluacionOrdenServicioes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteevaluacionOrdenServicio(int id)
        {
            var evaluacionOrdenServicio = await _context.evaluacionOrdenServicio.FindAsync(id);
            if (evaluacionOrdenServicio == null)
            {
                return NotFound();
            }

            _context.evaluacionOrdenServicio.Remove(evaluacionOrdenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool evaluacionOrdenServicioExists(int id)
        {
            return _context.evaluacionOrdenServicio.Any(e => e.idEvaluacionOrdenServicio == id);
        }
    }
}
