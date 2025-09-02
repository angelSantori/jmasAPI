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
    public class JuntasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public JuntasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Juntas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juntas>>> GetJuntas()
        {
            return await _context.Juntas.ToListAsync();
        }

        // GET: api/Juntas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Juntas>> GetJuntas(int id)
        {
            var juntas = await _context.Juntas.FindAsync(id);

            if (juntas == null)
            {
                return NotFound();
            }

            return juntas;
        }

        // PUT: api/Juntas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuntas(int id, Juntas juntas)
        {
            if (id != juntas.Id_Junta)
            {
                return BadRequest();
            }

            _context.Entry(juntas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaJuntaNube(juntas, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuntasExists(id))
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

        // POST: api/Juntas
        [HttpPost]
        public async Task<ActionResult<Juntas>> PostJuntas(Juntas juntas)
        {
            _context.Juntas.Add(juntas);
            await _context.SaveChangesAsync();
            await ReplicaJuntaNube(juntas, "POST");

            return CreatedAtAction("GetJuntas", new { id = juntas.Id_Junta }, juntas);
        }

        // DELETE: api/Juntas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuntas(int id)
        {
            var juntas = await _context.Juntas.FindAsync(id);
            if (juntas == null)
            {
                return NotFound();
            }

            _context.Juntas.Remove(juntas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuntasExists(int id)
        {
            return _context.Juntas.Any(e => e.Id_Junta == id);
        }

        private async Task ReplicaJuntaNube(Juntas juntas, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Juntas";

                var jsonContent = JsonSerializer.Serialize(juntas);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{juntas.Id_Junta}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{juntas.Id_Junta}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar JUNTA en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción JUNTA al replicar en la nube: {ex.Message}");
            }
        }
    }
}
