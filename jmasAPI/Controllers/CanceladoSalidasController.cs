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
    public class CanceladoSalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CanceladoSalidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CanceladoSalidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CanceladoSalida>>> GetCanceladoSalida()
        {
            return await _context.CanceladoSalida.ToListAsync();
        }

        // GET: api/CanceladoSalidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CanceladoSalida>> GetCanceladoSalida(int id)
        {
            var canceladoSalida = await _context.CanceladoSalida.FindAsync(id);

            if (canceladoSalida == null)
            {
                return NotFound();
            }

            return canceladoSalida;
        }

        // PUT: api/CanceladoSalidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanceladoSalida(int id, CanceladoSalida canceladoSalida)
        {
            if (id != canceladoSalida.idCanceladoSalida)
            {
                return BadRequest();
            }

            _context.Entry(canceladoSalida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanceladoSalidaExists(id))
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

        // POST: api/CanceladoSalidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CanceladoSalida>> PostCanceladoSalida(CanceladoSalida canceladoSalida)
        {
            _context.CanceladoSalida.Add(canceladoSalida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanceladoSalida", new { id = canceladoSalida.idCanceladoSalida }, canceladoSalida);
        }

        // DELETE: api/CanceladoSalidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanceladoSalida(int id)
        {
            var canceladoSalida = await _context.CanceladoSalida.FindAsync(id);
            if (canceladoSalida == null)
            {
                return NotFound();
            }

            _context.CanceladoSalida.Remove(canceladoSalida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanceladoSalidaExists(int id)
        {
            return _context.CanceladoSalida.Any(e => e.idCanceladoSalida == id);
        }
    }
}
