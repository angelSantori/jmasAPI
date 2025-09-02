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
    public class EntrevistaPadronsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EntrevistaPadronsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/EntrevistaPadrons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntrevistaPadron>>> GetentrevistaPadron()
        {
            return await _context.entrevistaPadron.ToListAsync();
        }

        // GET: api/EntrevistaPadrons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntrevistaPadron>> GetEntrevistaPadron(int id)
        {
            var entrevistaPadron = await _context.entrevistaPadron.FindAsync(id);

            if (entrevistaPadron == null)
            {
                return NotFound();
            }

            return entrevistaPadron;
        }

        // GET: api/EntrevistaPadrons/ByOS/{idOS}
        [HttpGet("ByOS/{idOS}")]
        public async Task<ActionResult<IEnumerable<EntrevistaPadron>>> GetEPxOS(int idOS)
        {
            try
            {
                var entrevistas = await _context.entrevistaPadron
                    .Where(ep => ep.idOrdenServicio == idOS)
                    .OrderBy(ep => ep.idEntrevistaPadron)
                    .ToListAsync();

                if (entrevistas == null || entrevistas.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron entrevistas ligadas con el idOS: {idOS}" });
                }

                return Ok(entrevistas);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/EntrevistaPadrons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrevistaPadron(int id, EntrevistaPadron entrevistaPadron)
        {
            if (id != entrevistaPadron.idEntrevistaPadron)
            {
                return BadRequest();
            }

            _context.Entry(entrevistaPadron).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarEntrevistaPadron(entrevistaPadron, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrevistaPadronExists(id))
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

        // POST: api/EntrevistaPadrons
        [HttpPost]
        public async Task<ActionResult<EntrevistaPadron>> PostEntrevistaPadron(EntrevistaPadron entrevistaPadron)
        {
            _context.entrevistaPadron.Add(entrevistaPadron);
            await _context.SaveChangesAsync();
            await ReplicarEntrevistaPadron(entrevistaPadron, "POST");

            return CreatedAtAction("GetEntrevistaPadron", new { id = entrevistaPadron.idEntrevistaPadron }, entrevistaPadron);
        }

        // DELETE: api/EntrevistaPadrons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrevistaPadron(int id)
        {
            var entrevistaPadron = await _context.entrevistaPadron.FindAsync(id);
            if (entrevistaPadron == null)
            {
                return NotFound();
            }

            _context.entrevistaPadron.Remove(entrevistaPadron);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntrevistaPadronExists(int id)
        {
            return _context.entrevistaPadron.Any(e => e.idEntrevistaPadron == id);
        }

        private async Task ReplicarEntrevistaPadron(EntrevistaPadron entrevistaPadron, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/EntrevistaPadrons";

                var jsonContent = JsonSerializer.Serialize(entrevistaPadron);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{entrevistaPadron.idEntrevistaPadron}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{entrevistaPadron.idEntrevistaPadron}");
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
