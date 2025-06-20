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
    public class OrdenTrabajosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenTrabajosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdenTrabajos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenTrabajo>>> GetordenTrabajo()
        {
            return await _context.ordenTrabajo.ToListAsync();
        }

        // GET: api/OrdenTrabajos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenTrabajo>> GetOrdenTrabajo(int id)
        {
            var ordenTrabajo = await _context.ordenTrabajo.FindAsync(id);

            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            return ordenTrabajo;
        }

        // GET: api/OrdenTrabajos/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<OrdenTrabajo>>> GetOrdenTrabajoXFolio(string folio)
        {
            var ordenTrabajo = await _context.ordenTrabajo
                .Where(ot => ot.folioOT == folio)
                .ToListAsync();

            //Verificar si se encontraron registros
            if (ordenTrabajo == null || ordenTrabajo.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron ordenTrabajo con el folio: {folio}" });
            }

            return Ok(ordenTrabajo);
        }

        //Get api/OrdenTrabajos/next-folio
        [HttpGet("nextOTFolio")]
        public async Task<ActionResult<string>> GetNextOrdenTrabajoFolio()
        {
            var lastOT = await _context.ordenTrabajo
                .OrderByDescending(ot => ot.idOrdenTrabajo)
                .FirstOrDefaultAsync();

            int nextNumber = lastOT != null
                ? int.Parse(lastOT.folioOT.Replace("OT", "")) + 1
                : 1;

            return Ok($"OT{nextNumber}");
        }

        // PUT: api/OrdenTrabajoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenTrabajo(int id, OrdenTrabajo ordenTrabajo)
        {
            if (id != ordenTrabajo.idOrdenTrabajo)
            {
                return BadRequest();
            }

            _context.Entry(ordenTrabajo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenTrabajoExists(id))
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



        // POST: api/OrdenTrabajoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenTrabajo>> PostOrdenTrabajo(OrdenTrabajo ordenTrabajo)
        {
            _context.ordenTrabajo.Add(ordenTrabajo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenTrabajo", new { id = ordenTrabajo.idOrdenTrabajo }, ordenTrabajo);
        }

        // DELETE: api/OrdenTrabajoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenTrabajo(int id)
        {
            var ordenTrabajo = await _context.ordenTrabajo.FindAsync(id);
            if (ordenTrabajo == null)
            {
                return NotFound();
            }

            _context.ordenTrabajo.Remove(ordenTrabajo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenTrabajoExists(int id)
        {
            return _context.ordenTrabajo.Any(e => e.idOrdenTrabajo == id);
        }
    }
}
