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
    public class PadronsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PadronsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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

        // GET: api/Padrons/BuscarPorNombre?nombre={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Padron>>> BuscarPorNombre([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El parámetro 'nombre' es requerido");
            }

            return await _context.Padron
                .Where(p => p.padronNombre != null &&
                            p.padronNombre.ToLower().Contains(nombre.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // GET: api/Padrons/BuscarPorDireccion?direccion={direccion}
        [HttpGet("BuscarPorDireccion")]
        public async Task<ActionResult<IEnumerable<Padron>>> BuscarPorDireccion([FromQuery] string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
            {
                return BadRequest("El parámetro 'direccion' es requerido");
            }
            return await _context.Padron
                .Where(p => p.padronDireccion != null &&
                            p.padronDireccion.ToLower().Contains(direccion.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // GET: api/Padrons/Buscar?termino={termino}
        [HttpGet("Buscar")]
        public async Task<ActionResult<IEnumerable<Padron>>> Buscar([FromQuery] string termino)
        {
            if (string.IsNullOrWhiteSpace(termino))
            {
                return BadRequest("El parámetro 'termino' es requerido");
            }

            var terminoLower = termino.ToLower();

            return await _context.Padron
                .Where(p => (p.padronNombre != null && p.padronNombre.ToLower().Contains(terminoLower)) ||
                           (p.padronDireccion != null && p.padronDireccion.ToLower().Contains(terminoLower)))
                .Take(30)
                .ToListAsync();
        }

        // PUT: api/Padrons/5        
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
                await ReplicaPadronNube(padron, "PUT");
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
        [HttpPost]
        public async Task<ActionResult<Padron>> PostPadron(Padron padron)
        {
            _context.Padron.Add(padron);
            await _context.SaveChangesAsync();
            await ReplicaPadronNube(padron, "POST");

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

        private async Task ReplicaPadronNube(Padron padron, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Padrons";

                var jsonContent = JsonSerializer.Serialize(padron);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{padron.idPadron}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{padron.idPadron}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar PADRON en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción PADRON al replicar en la nube: {ex.Message}");
            }
        }
    }
}
