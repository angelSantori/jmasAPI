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
    public class LectEnviarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LectEnviarsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/LectEnviars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectEnviar>>> GetLectEnviar()
        {
            return await _context.LectEnviar.ToListAsync();
        }

        // GET: api/LectEnviars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LectEnviar>> GetLectEnviar(int id)
        {
            var lectEnviar = await _context.LectEnviar.FindAsync(id);

            if (lectEnviar == null)
            {
                return NotFound();
            }

            return lectEnviar;
        }

        // PUT: api/LectEnviars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLectEnviar(int id, LectEnviar lectEnviar)
        {
            if (id != lectEnviar.idLectEnviar)
            {
                return BadRequest();
            }

            _context.Entry(lectEnviar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaLectEnviarNube(lectEnviar, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectEnviarExists(id))
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

        // POST: api/LectEnviars
        [HttpPost]
        public async Task<ActionResult<LectEnviar>> PostLectEnviar(LectEnviar lectEnviar)
        {
            _context.LectEnviar.Add(lectEnviar);
            await _context.SaveChangesAsync();
            await ReplicaLectEnviarNube(lectEnviar, "POST");

            return CreatedAtAction("GetLectEnviar", new { id = lectEnviar.idLectEnviar }, lectEnviar);
        }

        // DELETE: api/LectEnviars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectEnviar(int id)
        {
            var lectEnviar = await _context.LectEnviar.FindAsync(id);
            if (lectEnviar == null)
            {
                return NotFound();
            }

            _context.LectEnviar.Remove(lectEnviar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LectEnviarExists(int id)
        {
            return _context.LectEnviar.Any(e => e.idLectEnviar == id);
        }

        private async Task ReplicaLectEnviarNube(LectEnviar lectEnviar, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/LectEnviars";

                var jsonContent = JsonSerializer.Serialize(lectEnviar);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{lectEnviar.idLectEnviar}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{lectEnviar.idLectEnviar}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar LECTENVIAR en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción LECTENVIAR al replicar en la nube: {ex.Message}");
            }
        }
    }
}
