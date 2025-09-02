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
    public class EvaluacionOrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EvaluacionOrdenServiciosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/evaluacionOrdenServicioes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenServicio>>> GetevaluacionOrdenServicio()
        {
            return await _context.evaluacionOrdenServicio.ToListAsync();
        }

        // GET: api/evaluacionOrdenServicioes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EvaluacionOrdenServicio>> GetevaluacionOrdenServicio(int id)
        {
            var evaluacionOrdenServicio = await _context.evaluacionOrdenServicio.FindAsync(id);

            if (evaluacionOrdenServicio == null)
            {
                return NotFound();
            }

            return evaluacionOrdenServicio;
        }

        // GET: api/evaluacionOrdenServicioes/ByOT/{idOT}
        [HttpGet("ByOS/{idOS}")]
        public async Task<ActionResult<IEnumerable<EvaluacionOrdenServicio>>> GetEOSxidOS(int idOS)
        {
            try
            {
                var evaluaciones = await _context.evaluacionOrdenServicio
                    .Where(ev => ev.idOrdenServicio == idOS)
                    .OrderBy(ev => ev.idEvaluacionOrdenServicio)
                    .ToListAsync();

                if (evaluaciones == null || evaluaciones.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron evaluaciones ligadas con el idOS: {idOS}" });
                }

                return Ok(evaluaciones);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
 
        // PUT: api/evaluacionOrdenServicioes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutevaluacionOrdenServicio(int id, EvaluacionOrdenServicio evaluacionOrdenServicio)
        {
            if (id != evaluacionOrdenServicio.idEvaluacionOrdenServicio)
            {
                return BadRequest();
            }

            _context.Entry(evaluacionOrdenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarEvaluacionOSNube(evaluacionOrdenServicio, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!evaluacionOrdenServicioExists(id))
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

        // POST: api/evaluacionOrdenServicioes
        [HttpPost]
        public async Task<ActionResult<EvaluacionOrdenServicio>> PostevaluacionOrdenServicio(EvaluacionOrdenServicio evaluacionOrdenServicio)
        {
            _context.evaluacionOrdenServicio.Add(evaluacionOrdenServicio);
            await _context.SaveChangesAsync();
            await ReplicarEvaluacionOSNube(evaluacionOrdenServicio, "POST");

            return CreatedAtAction("GetevaluacionOrdenServicio", new { id = evaluacionOrdenServicio.idEvaluacionOrdenServicio}, evaluacionOrdenServicio);
        }

        // DELETE: api/evaluacionOrdenServicioes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteevaluacionOrdenServicio(int id)
        {
            var evaluacionOrdenServicio = await _context.evaluacionOrdenServicio.FindAsync(id);
            if (evaluacionOrdenServicio == null)
            {
                return NotFound();
            }

            _context.evaluacionOrdenServicio.Remove(evaluacionOrdenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool evaluacionOrdenServicioExists(int id)
        {
            return _context.evaluacionOrdenServicio.Any(e => e.idEvaluacionOrdenServicio == id);
        }

        private async Task ReplicarEvaluacionOSNube(EvaluacionOrdenServicio evaluacionOrdenServicio, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/EvaluacionOrdenServicios";

                var jsonContent = JsonSerializer.Serialize(evaluacionOrdenServicio);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{evaluacionOrdenServicio.idOrdenServicio}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{evaluacionOrdenServicio.idOrdenServicio}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar EVALUACIONOS en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Excepción EVALUACIONOS al replicar en la nube: {ex.Message}");
            }
        }
    }
}
