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
    public class htaPrestamosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public htaPrestamosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/htaPrestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<htaPrestamo>>> GethtaPrestamo()
        {
            return await _context.htaPrestamo.ToListAsync();
        }

        // GET: api/htaPrestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<htaPrestamo>> GethtaPrestamo(int id)
        {
            var htaPrestamo = await _context.htaPrestamo.FindAsync(id);

            if (htaPrestamo == null)
            {
                return NotFound();
            }

            return htaPrestamo;
        }



        // PUT: api/htaPrestamos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PuthtaPrestamo(int id, htaPrestamo htaPrestamo)
        {
            if (id != htaPrestamo.idHtaPrestamo)
            {
                return BadRequest();
            }

            _context.Entry(htaPrestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!htaPrestamoExists(id))
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

        // POST: api/htaPrestamos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<htaPrestamo>> PosthtaPrestamo(htaPrestamo htaPrestamo)
        {
            _context.htaPrestamo.Add(htaPrestamo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GethtaPrestamo", new { id = htaPrestamo.idHtaPrestamo }, htaPrestamo);
        }

        // DELETE: api/htaPrestamos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletehtaPrestamo(int id)
        {
            var htaPrestamo = await _context.htaPrestamo.FindAsync(id);
            if (htaPrestamo == null)
            {
                return NotFound();
            }

            _context.htaPrestamo.Remove(htaPrestamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool htaPrestamoExists(int id)
        {
            return _context.htaPrestamo.Any(e => e.idHtaPrestamo == id);
        }
    }
}
