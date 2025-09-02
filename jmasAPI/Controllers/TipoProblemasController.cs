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
    public class TipoProblemasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TipoProblemasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/TipoProblemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProblema>>> GettipoProblema()
        {
            return await _context.tipoProblema.ToListAsync();
        }

        // GET: api/TipoProblemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProblema>> GetTipoProblema(int id)
        {
            var tipoProblema = await _context.tipoProblema.FindAsync(id);

            if (tipoProblema == null)
            {
                return NotFound();
            }

            return tipoProblema;
        }

        // PUT: api/TipoProblemas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProblema(int id, TipoProblema tipoProblema)
        {
            if (id != tipoProblema.idTipoProblema)
            {
                return BadRequest();
            }

            _context.Entry(tipoProblema).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaTipoProblemaNube(tipoProblema, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProblemaExists(id))
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

        // POST: api/TipoProblemas
        [HttpPost]
        public async Task<ActionResult<TipoProblema>> PostTipoProblema(TipoProblema tipoProblema)
        {
            _context.tipoProblema.Add(tipoProblema);
            await _context.SaveChangesAsync();
            await ReplicaTipoProblemaNube(tipoProblema, "POST");

            return CreatedAtAction("GetTipoProblema", new { id = tipoProblema.idTipoProblema }, tipoProblema);
        }

        // DELETE: api/TipoProblemas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProblema(int id)
        {
            var tipoProblema = await _context.tipoProblema.FindAsync(id);
            if (tipoProblema == null)
            {
                return NotFound();
            }

            _context.tipoProblema.Remove(tipoProblema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProblemaExists(int id)
        {
            return _context.tipoProblema.Any(e => e.idTipoProblema == id);
        }

        private async Task ReplicaTipoProblemaNube(TipoProblema tipoProblema, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/TipoProblemas";

                var jsonContent = JsonSerializer.Serialize(tipoProblema);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{tipoProblema.idTipoProblema}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{tipoProblema.idTipoProblema}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar TIPORPOBLEMA en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción TIPORPOBLEMA al replicar en la nube: {ex.Message}");
            }
        }
    }
}
