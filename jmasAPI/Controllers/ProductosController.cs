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
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProductos(int id)
        {
            var productos = await _context.Productos.FindAsync(id);

            if (productos == null)
            {
                return NotFound();
            }

            return productos;
        }

        //Ger api/ConrultaUniversal/5
        [HttpGet("ConsultaUniversal/{idProducto}")]
        public async Task<IActionResult> ConsultaUniversal(int idProducto)
        {
            var entradas = await _context.Entradas.Where(e => e.idProducto == idProducto).ToListAsync();
            var salidas = await _context.Salidas.Where(s => s.idProducto == idProducto).ToListAsync();

            return Ok(new { entradas, salidas });
        }

        // GET: api/Productos/BuscarPorNombre?nombre={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Productos>>> BuscarProductosPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El parámetro 'nombre' es requerido");
            }

            try
            {
                var productos = await _context.Productos
                    .Where(p => p.prodDescripcion != null &&
                                p.prodDescripcion.ToLower().Contains(nombre.ToLower()))
                    .Take(30)
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Productos/ConDeficit
        [HttpGet("ConDeficit")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductoConDeficit()
        {
            return await _context.Productos
                .Where(p => p.prodExistencia < p.prodMin)
                .ToListAsync();
        }

        // GET: api/Productos/PorRango?idInicial=1&idFinal=100
        [HttpGet("PorRango")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProductosPorRango(
            [FromQuery] int idInicial,
            [FromQuery] int idFinal)
        {
            if (idInicial > idFinal)
            {
                return BadRequest("El ID inicial no puede ser mayor que el ID final");
            }
            return await _context.Productos
                .Where(p => p.Id_Producto >= idInicial && p.Id_Producto<= idFinal)
                .OrderBy(p => p.Id_Producto)
                .ToListAsync();
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductos(int id, Productos productos)
        {
            if (id != productos.Id_Producto)
            {
                return BadRequest();
            }

            _context.Entry(productos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
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

        // PATCH
        // PATCH: api/Productos/5/ActualizarInventario
        [HttpPatch("{id}/ActualizarInventario")]
        public async Task<IActionResult> UpdateInventario(
            int id,
            [FromBody] UpdateInventarioDto request)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Actualiza existencia y almacén
            producto.prodExistencia = request.Existencia;
            producto.Id_Almacen = request.IdAlmacen;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProductos(Productos productos)
        {
            _context.Productos.Add(productos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductos", new { id = productos.Id_Producto}, productos);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductos(int id)
        {
            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.Id_Producto == id);
        }
    }
}
