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
    public class OrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenServiciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ordenServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenServicio>>> GetordenServicio()
        {
            return await _context.ordenServicio.ToListAsync();
        }

        // GET: api/ordenServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenServicio>> GetordenServicio(int id)
        {
            var ordenServicio = await _context.ordenServicio.FindAsync(id);

            if (ordenServicio == null)
            {
                return NotFound();
            }

            return ordenServicio;
        }

        // GET: api/ordenServicios/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<OrdenServicio>>> GetordenServicioXFolio(string folio)
        {
            var ordenServicio = await _context.ordenServicio
                .Where(os => os.folioOS == folio)
                .ToListAsync();

            //Verificar si se encontraron registros
            if (ordenServicio == null || ordenServicio.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron ordenServicio con el folio: {folio}" });
            }

            return Ok(ordenServicio);
        }

        //Get api/ordenServicios/next-folio
        [HttpGet("nextOTFolio")]
        public async Task<ActionResult<string>> GetNextordenServicioFolio()
        {
            var lastOT = await _context.ordenServicio
                .OrderByDescending(os => os.idOrdenServicio)
                .FirstOrDefaultAsync();

            int nextNumber = lastOT != null
                ? int.Parse(lastOT.folioOS.Replace("OS", "")) + 1
                : 1;

            return Ok($"OS{nextNumber}");
        }

        // PUT: api/ordenServicioes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutordenServicio(int id, OrdenServicio ordenServicio)
        {
            if (id != ordenServicio.idOrdenServicio)
            {
                return BadRequest();
            }

            _context.Entry(ordenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ordenServicioExists(id))
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



        // POST: api/ordenServicioes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenServicio>> PostordenServicio(OrdenServicio ordenServicio)
        {
            _context.ordenServicio.Add(ordenServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetordenServicio", new { id = ordenServicio.idOrdenServicio}, ordenServicio);
        }

        // DELETE: api/ordenServicioes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteordenServicio(int id)
        {
            var ordenServicio = await _context.ordenServicio.FindAsync(id);
            if (ordenServicio == null)
            {
                return NotFound();
            }

            _context.ordenServicio.Remove(ordenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ordenServicioExists(int id)
        {
            return _context.ordenServicio.Any(e => e.idOrdenServicio == id);
        }
    }
}
