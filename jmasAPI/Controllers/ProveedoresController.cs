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
    public class ProveedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProveedoresController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedores>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedores>> GetProveedores(int id)
        {
            var proveedores = await _context.Proveedores.FindAsync(id);

            if (proveedores == null)
            {
                return NotFound();
            }

            return proveedores;
        }

        // GET: api/Proveedores/BuscarProveedorXNombre?nombreProveedor={nombre}
        [HttpGet("ProveedorPorNombre")]
        public async Task<ActionResult<IEnumerable<Proveedores>>> ProveedorPorNombre([FromQuery] string nombreProveedor)
        {
            if (string.IsNullOrWhiteSpace(nombreProveedor))
            {
                return BadRequest("El parámetro 'nombreProveedor' es requerido");
            }

            return await _context.Proveedores
                .Where(pN => pN.Proveedor_Name != null &&
                             pN.Proveedor_Name.ToLower().Contains(nombreProveedor.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // PUT: api/Proveedores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedores(int id, Proveedores proveedores)
        {
            if (id != proveedores.Id_Proveedor)
            {
                return BadRequest();
            }

            _context.Entry(proveedores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaProveedorNube(proveedores, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedoresExists(id))
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

        // POST: api/Proveedores
        [HttpPost]
        public async Task<ActionResult<Proveedores>> PostProveedores(Proveedores proveedores)
        {
            _context.Proveedores.Add(proveedores);
            await _context.SaveChangesAsync();
            await ReplicaProveedorNube(proveedores, "POST");

            return CreatedAtAction("GetProveedores", new { id = proveedores.Id_Proveedor }, proveedores);
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedores(int id)
        {
            var proveedores = await _context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedoresExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id_Proveedor == id);
        }

        private async Task ReplicaProveedorNube(Proveedores proveedores, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Proveedores";

                var jsonContent = JsonSerializer.Serialize(proveedores);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{proveedores.Id_Proveedor}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{proveedores.Id_Proveedor}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar PROVEEDOR en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción PROVEEDOR al replicar en la nube: {ex.Message}");
            }
        }
    }
}
