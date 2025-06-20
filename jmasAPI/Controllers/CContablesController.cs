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
    public class CContablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CContablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CContable>>> GetCContable()
        {
            return await _context.CContable.ToListAsync();
        }

        // GET: api/CContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CContable>> GetCContable(int id)
        {
            var cContable = await _context.CContable.FindAsync(id);

            if (cContable == null)
            {
                return NotFound();
            }

            return cContable;
        }

        // GET: api/CContables/ByProducto/{productoId}
        [HttpGet("ByProducto/{productoId}")]
        public async Task<ActionResult<IEnumerable<CContable>>> GetCCxProducto(int productoId)
        {
            var ccontable = await _context.CContable
                .Where(cc => cc.idProducto == productoId)
                .ToListAsync();

            if (ccontable == null || ccontable.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron cc con el productoId: {productoId}" });
            }

            return Ok(ccontable);
        }

        // PUT: api/CContables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCContable(int id, CContable cContable)
        {
            if (id != cContable.Id_CConTable)
            {
                return BadRequest();
            }

            _context.Entry(cContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CContableExists(id))
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

        // POST: api/CContables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CContable>> PostCContable(CContable cContable)
        {
            _context.CContable.Add(cContable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCContable", new { id = cContable.Id_CConTable }, cContable);
        }

        // DELETE: api/CContables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCContable(int id)
        {
            var cContable = await _context.CContable.FindAsync(id);
            if (cContable == null)
            {
                return NotFound();
            }

            _context.CContable.Remove(cContable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CContableExists(int id)
        {
            return _context.CContable.Any(e => e.Id_CConTable == id);
        }
    }
}
