﻿using System;
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
    public class PadronsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PadronsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Padrons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Padron>>> GetPadron()
        {
            return await _context.Padron.ToListAsync();
        }

        // GET: api/Padrons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Padron>> GetPadron(int id)
        {
            var padron = await _context.Padron.FindAsync(id);

            if (padron == null)
            {
                return NotFound();
            }

            return padron;
        }

        // PUT: api/Padrons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPadron(int id, Padron padron)
        {
            if (id != padron.idPadron)
            {
                return BadRequest();
            }

            _context.Entry(padron).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PadronExists(id))
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

        // POST: api/Padrons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Padron>> PostPadron(Padron padron)
        {
            _context.Padron.Add(padron);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPadron", new { id = padron.idPadron }, padron);
        }

        // DELETE: api/Padrons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePadron(int id)
        {
            var padron = await _context.Padron.FindAsync(id);
            if (padron == null)
            {
                return NotFound();
            }

            _context.Padron.Remove(padron);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PadronExists(int id)
        {
            return _context.Padron.Any(e => e.idPadron == id);
        }
    }
}
