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
    public class ProblemasLecturasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProblemasLecturasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProblemasLecturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProblemasLectura>>> GetProblemasLectura()
        {
            return await _context.ProblemasLectura.ToListAsync();
        }

        // GET: api/ProblemasLecturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProblemasLectura>> GetProblemasLectura(int id)
        {
            var problemasLectura = await _context.ProblemasLectura.FindAsync(id);

            if (problemasLectura == null)
            {
                return NotFound();
            }

            return problemasLectura;
        }

        // PUT: api/ProblemasLecturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblemasLectura(int id, ProblemasLectura problemasLectura)
        {
            if (id != problemasLectura.idProblema)
            {
                return BadRequest();
            }

            _context.Entry(problemasLectura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemasLecturaExists(id))
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

        // POST: api/ProblemasLecturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProblemasLectura>> PostProblemasLectura(ProblemasLectura problemasLectura)
        {
            _context.ProblemasLectura.Add(problemasLectura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblemasLectura", new { id = problemasLectura.idProblema }, problemasLectura);
        }

        // DELETE: api/ProblemasLecturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblemasLectura(int id)
        {
            var problemasLectura = await _context.ProblemasLectura.FindAsync(id);
            if (problemasLectura == null)
            {
                return NotFound();
            }

            _context.ProblemasLectura.Remove(problemasLectura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProblemasLecturaExists(int id)
        {
            return _context.ProblemasLectura.Any(e => e.idProblema == id);
        }
    }
}
