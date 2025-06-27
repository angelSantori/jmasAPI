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
    public class TipoProblemasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipoProblemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoProblemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProblema>>> GettipoProblema()
        {
            return await _context.tipoProblema.ToListAsync();
        }

        // GET: api/TipoProblemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProblema>> GetTipoProblema(int id)
        {
            var tipoProblema = await _context.tipoProblema.FindAsync(id);

            if (tipoProblema == null)
            {
                return NotFound();
            }

            return tipoProblema;
        }

        // PUT: api/TipoProblemas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProblema(int id, TipoProblema tipoProblema)
        {
            if (id != tipoProblema.idTipoProblema)
            {
                return BadRequest();
            }

            _context.Entry(tipoProblema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProblemaExists(id))
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

        // POST: api/TipoProblemas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoProblema>> PostTipoProblema(TipoProblema tipoProblema)
        {
            _context.tipoProblema.Add(tipoProblema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoProblema", new { id = tipoProblema.idTipoProblema }, tipoProblema);
        }

        // DELETE: api/TipoProblemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProblema(int id)
        {
            var tipoProblema = await _context.tipoProblema.FindAsync(id);
            if (tipoProblema == null)
            {
                return NotFound();
            }

            _context.tipoProblema.Remove(tipoProblema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProblemaExists(int id)
        {
            return _context.tipoProblema.Any(e => e.idTipoProblema == id);
        }
    }
}
