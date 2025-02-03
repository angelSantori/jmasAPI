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
    public class CanceladosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CanceladosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Canceladoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cancelado>>> GetCancelado()
        {
            return await _context.Cancelado.ToListAsync();
        }

        // GET: api/Canceladoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cancelado>> GetCancelado(int id)
        {
            var cancelado = await _context.Cancelado.FindAsync(id);

            if (cancelado == null)
            {
                return NotFound();
            }

            return cancelado;
        }

        // PUT: api/Canceladoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCancelado(int id, Cancelado cancelado)
        {
            if (id != cancelado.idCancelacion)
            {
                return BadRequest();
            }

            _context.Entry(cancelado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanceladoExists(id))
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

        // POST: api/Canceladoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cancelado>> PostCancelado(Cancelado cancelado)
        {
            _context.Cancelado.Add(cancelado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCancelado", new { id = cancelado.idCancelacion }, cancelado);
        }

        // DELETE: api/Canceladoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelado(int id)
        {
            var cancelado = await _context.Cancelado.FindAsync(id);
            if (cancelado == null)
            {
                return NotFound();
            }

            _context.Cancelado.Remove(cancelado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanceladoExists(int id)
        {
            return _context.Cancelado.Any(e => e.idCancelacion == id);
        }
    }
}
