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
    public class ContratistasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ContratistasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Contratistas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contratistas>>> GetContratistas()
        {
            return await _context.Contratistas.ToListAsync();
        }

        // GET: api/Contratistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contratistas>> GetContratistas(int id)
        {
            var contratistas = await _context.Contratistas.FindAsync(id);

            if (contratistas == null)
            {
                return NotFound();
            }

            return contratistas;
        }

        // PUT: api/Contratistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContratistas(int id, Contratistas contratistas)
        {
            if (id != contratistas.idContratista)
            {
                return BadRequest();
            }

            _context.Entry(contratistas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaContratistaNube(contratistas, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratistasExists(id))
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

        // POST: api/Contratistas
        [HttpPost]
        public async Task<ActionResult<Contratistas>> PostContratistas(Contratistas contratistas)
        {
            _context.Contratistas.Add(contratistas);
            await _context.SaveChangesAsync();
            await ReplicaContratistaNube(contratistas, "POST");

            return CreatedAtAction("GetContratistas", new { id = contratistas.idContratista }, contratistas);
        }

        // DELETE: api/Contratistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContratistas(int id)
        {
            var contratistas = await _context.Contratistas.FindAsync(id);
            if (contratistas == null)
            {
                return NotFound();
            }

            _context.Contratistas.Remove(contratistas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContratistasExists(int id)
        {
            return _context.Contratistas.Any(e => e.idContratista == id);
        }

        private async Task ReplicaContratistaNube(Contratistas contratista, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Contratistas";

                var jsonContent = JsonSerializer.Serialize(contratista);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{contratista.idContratista}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{contratista.idContratista}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar CONTRATISTA en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción CONTRATISTA al replicar en la nube: {ex.Message}");
            }
        }
    }
}
