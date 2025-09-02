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
    public class ColoniasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ColoniasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Colonias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colonia>>> GetColonia()
        {
            return await _context.Colonia.ToListAsync();
        }

        // GET: api/Colonias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colonia>> GetColonia(int id)
        {
            var colonia = await _context.Colonia.FindAsync(id);

            if (colonia == null)
            {
                return NotFound();
            }

            return colonia;
        }

        // GET: api/Colonias/BuscarPorNombre?nombre={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Colonia>>> BuscarPorNombre([FromQuery] string nombreColonia)
        {
            if (string.IsNullOrWhiteSpace(nombreColonia))
            {
                return BadRequest("El párametro 'nombreColonia' es requerido");
            }
            return await _context.Colonia
                .Where(colonia => colonia.nombreColonia != null &&
                            colonia.nombreColonia.ToLower().Contains(nombreColonia.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // PUT: api/Colonias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColonia(int id, Colonia colonia)
        {
            if (id != colonia.idColonia)
            {
                return BadRequest();
            }

            _context.Entry(colonia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarColoniasNube(colonia, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoniaExists(id))
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

        // POST: api/Colonias
        [HttpPost]
        public async Task<ActionResult<Colonia>> PostColonia(Colonia colonia)
        {
            _context.Colonia.Add(colonia);
            await _context.SaveChangesAsync();
            await ReplicarColoniasNube(colonia, "POST");

            return CreatedAtAction("GetColonia", new { id = colonia.idColonia }, colonia);
        }

        // DELETE: api/Colonias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColonia(int id)
        {
            var colonia = await _context.Colonia.FindAsync(id);
            if (colonia == null)
            {
                return NotFound();
            }

            _context.Colonia.Remove(colonia);
            await _context.SaveChangesAsync();
            await ReplicarColoniasNube(colonia, "DELETE");

            return NoContent();
        }

        private bool ColoniaExists(int id)
        {
            return _context.Colonia.Any(e => e.idColonia == id);
        }

        private async Task ReplicarColoniasNube(Colonia colonia, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Colonias";

                var jsonContent = JsonSerializer.Serialize(colonia);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{colonia.idColonia}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{colonia.idColonia}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar COLONIA en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Excepción COLONIA al replicar en la nube: {ex.Message}");
            }
        }
    }
}
