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
    public class OrdenComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdenCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenCompra>>> GetordenCompra()
        {
            return await _context.ordenCompra.ToListAsync();
        }

        // GET: api/OrdenCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenCompra>> GetOrdenCompra(int id)
        {
            var ordenCompra = await _context.ordenCompra.FindAsync(id);

            if (ordenCompra == null)
            {
                return NotFound();
            }

            return ordenCompra;
        }

        // GET: api/OrdenCompras/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<OrdenCompra>>> GetOrdenCompraXFolio(string folio)
        {
            var ordenCompra = await _context.ordenCompra
                .Where(oc => oc.folioOC ==  folio)
                .ToListAsync();

            if (ordenCompra == null || ordenCompra.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron compras con el folio: {folio}" });
            }

            return Ok(ordenCompra);
        }

        //Get api/OrdenCompras/nextFolio
        [HttpGet("nextOCFolio")]
        public async Task<ActionResult<string>> GetNextOrdenComprajoFolio()
        {
            var lasOC = await _context.ordenCompra
                .OrderByDescending(oc => oc.idOrdenCompra)
                .FirstOrDefaultAsync();

            int nextNumber = lasOC != null
                ? int.Parse(lasOC.folioOC.Replace("OC", "")) + 1
                : 1;

            return Ok($"OC{nextNumber}");
        }

        // PUT: api/OrdenCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdenCompra(int id, OrdenCompra ordenCompra)
        {
            if (id != ordenCompra.idOrdenCompra)
            {
                return BadRequest();
            }

            _context.Entry(ordenCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenCompraExists(id))
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

        // POST: api/OrdenCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenCompra>> PostOrdenCompra(OrdenCompra ordenCompra)
        {
            _context.ordenCompra.Add(ordenCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdenCompra", new { id = ordenCompra.idOrdenCompra }, ordenCompra);
        }

        // DELETE: api/OrdenCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdenCompra(int id)
        {
            var ordenCompra = await _context.ordenCompra.FindAsync(id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            _context.ordenCompra.Remove(ordenCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdenCompraExists(int id)
        {
            return _context.ordenCompra.Any(e => e.idOrdenCompra == id);
        }
    }
}
