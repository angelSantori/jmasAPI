using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using System.Text;
using System.Text.Json;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CContablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CContablesController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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

        [HttpGet("ProductosSinCuenta")]
        public async Task<ActionResult<IEnumerable<int>>> GetProductosSinCuenta()
        {
            // Obtener todos los IDs de productos que ya tienen cuenta
            var productosConCuenta = await _context.CContable
                .Select(cc => cc.idProducto)
                .Distinct()
                .ToListAsync();

            // Obtener todos los IDs de productos que NO tienen cuenta
            var productosSinCuenta = await _context.Productos
                .Where(p => !productosConCuenta.Contains(p.Id_Producto))
                .Select(p => p.Id_Producto)
                .ToListAsync();

            return Ok(productosSinCuenta);
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
                await ReplicarCContableNube(cContable, "PUT");
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
        [HttpPost]
        public async Task<ActionResult<CContable>> PostCContable(CContable cContable)
        {
            _context.CContable.Add(cContable);
            await _context.SaveChangesAsync();
            await ReplicarCContableNube(cContable, "POST");

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
            await ReplicarCContableNube(cContable, "DELETE");

            return NoContent();
        }

        private bool CContableExists(int id)
        {
            return _context.CContable.Any(e => e.Id_CConTable == id);
        }

        private async Task ReplicarCContableNube(CContable ccontable, string metodo)
        {
            bool replicacionHabilitada = _configuration.GetValue<bool>("Replicacion:Habilitada");

            if (!replicacionHabilitada)
            {
                return;
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiNubeUrlBase = _configuration.GetValue<string>("Replicacion:UrlApiNube");
                string apiNubeUrl = $"{apiNubeUrlBase}/CContables";

                var jsonContent = JsonSerializer.Serialize(ccontable);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{ccontable.Id_CConTable}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{ccontable.Id_CConTable}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar CCONTABLE en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción CCONTABLE al replicar en la nube: {ex.Message}");
            }
        }
    }
}
