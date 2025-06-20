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
    public class EvaluacionOrdenTrabajoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EvaluacionOrdenTrabajoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EvaluacionOrdenTrabajoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenTrabajo>>> GetevaluacionOrdenTrabajo()
        {
            return await _context.evaluacionOrdenTrabajo.ToListAsync();
        }

        // GET: api/EvaluacionOrdenTrabajoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionOrdenTrabajo>> GetEvaluacionOrdenTrabajo(int id)
        {
            var evaluacionOrdenTrabajo = await _context.evaluacionOrdenTrabajo.FindAsync(id);

            if (evaluacionOrdenTrabajo == null)
            {
                return NotFound();
            }

            return evaluacionOrdenTrabajo;
        }

        // GET: api/EvaluacionOrdenTrabajoes/ByOT/{idOT}
        [HttpGet("ByOT/{idOT}")]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenTrabajo>>> GetEOTxidOT(int idOT)
        {
            try
            {
                var evaluaciones = await _context.evaluacionOrdenTrabajo
                    .Where(ev => ev.idOrdenTrabajo == idOT)
                    .OrderBy(ev => ev.idEvaluacionOrdenTrabajo)
                    .ToListAsync();

                if (evaluaciones == null || evaluaciones.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron evaluaciones ligadas con el idOT: {idOT}" });
                }

                return Ok(evaluaciones);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
 
        // PUT: api/EvaluacionOrdenTrabajoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluacionOrdenTrabajo(int id, EvaluacionOrdenTrabajo evaluacionOrdenTrabajo)
        {
            if (id != evaluacionOrdenTrabajo.idEvaluacionOrdenTrabajo)
            {
                return BadRequest();
            }

            _context.Entry(evaluacionOrdenTrabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluacionOrdenTrabajoExists(id))
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

        // POST: api/EvaluacionOrdenTrabajoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EvaluacionOrdenTrabajo>> PostEvaluacionOrdenTrabajo(EvaluacionOrdenTrabajo evaluacionOrdenTrabajo)
        {
            _context.evaluacionOrdenTrabajo.Add(evaluacionOrdenTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluacionOrdenTrabajo", new { id = evaluacionOrdenTrabajo.idEvaluacionOrdenTrabajo }, evaluacionOrdenTrabajo);
        }

        // DELETE: api/EvaluacionOrdenTrabajoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvaluacionOrdenTrabajo(int id)
        {
            var evaluacionOrdenTrabajo = await _context.evaluacionOrdenTrabajo.FindAsync(id);
            if (evaluacionOrdenTrabajo == null)
            {
                return NotFound();
            }

            _context.evaluacionOrdenTrabajo.Remove(evaluacionOrdenTrabajo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvaluacionOrdenTrabajoExists(int id)
        {
            return _context.evaluacionOrdenTrabajo.Any(e => e.idEvaluacionOrdenTrabajo == id);
        }
    }
}
