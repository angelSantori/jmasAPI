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
    public class LectEnviarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LectEnviarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LectEnviars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectEnviar>>> GetLectEnviar()
        {
            return await _context.LectEnviar.ToListAsync();
        }

        // GET: api/LectEnviars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LectEnviar>> GetLectEnviar(int id)
        {
            var lectEnviar = await _context.LectEnviar.FindAsync(id);

            if (lectEnviar == null)
            {
                return NotFound();
            }

            return lectEnviar;
        }

        // PUT: api/LectEnviars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectEnviar(int id, LectEnviar lectEnviar)
        {
            if (id != lectEnviar.idLectEnviar)
            {
                return BadRequest();
            }

            _context.Entry(lectEnviar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectEnviarExists(id))
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

        // POST: api/LectEnviars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LectEnviar>> PostLectEnviar(LectEnviar lectEnviar)
        {
            _context.LectEnviar.Add(lectEnviar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLectEnviar", new { id = lectEnviar.idLectEnviar }, lectEnviar);
        }

        // DELETE: api/LectEnviars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectEnviar(int id)
        {
            var lectEnviar = await _context.LectEnviar.FindAsync(id);
            if (lectEnviar == null)
            {
                return NotFound();
            }

            _context.LectEnviar.Remove(lectEnviar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LectEnviarExists(int id)
        {
            return _context.LectEnviar.Any(e => e.idLectEnviar == id);
        }
    }
}
