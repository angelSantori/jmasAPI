using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajoRealizadoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TrabajoRealizadoesController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/TrabajoRealizadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrabajoRealizado>>> GettrabajoRealizado()
        {
            return await _context.trabajoRealizado.ToListAsync();
        }

        // GET: api/TrabajoRealizadoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrabajoRealizado>> GetTrabajoRealizado(int id)
        {
            var trabajoRealizado = await _context.trabajoRealizado.FindAsync(id);

            if (trabajoRealizado == null)
            {
                return NotFound();
            }

            return trabajoRealizado;
        }

        // GET: api/TrabajoRealizadoes/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<TrabajoRealizado>>> GetTRByFolio(string folio)
        {
            var trabajos = await _context.trabajoRealizado
                .Where(tr => tr.folioTR == folio)
                .ToListAsync();

            if (trabajos == null || trabajos.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron tranajos con el folio: {folio}" });
            }

            return Ok(trabajos);
        }

        //Get api/TrabajoRealizadoes/next-folio
        [HttpGet("next-trabajofolio")]
        public async Task<ActionResult<string>> GetNextTRCodFolio()
        {
            var lastTR = await _context.trabajoRealizado
                .OrderByDescending(tr => tr.idTrabajoRealizado)
                .FirstOrDefaultAsync();

            int nextNumber = lastTR != null
                ? int.Parse(lastTR.folioTR.Replace("TR", "")) + 1
                : 1;

            return Ok($"TR{nextNumber}");
        }

        //GET: api/TrabajoRealizadoes/ByUser/{userId}
        [HttpGet("ByUser/{userID}")]
        public async Task<ActionResult<IEnumerable<TrabajoRealizado>>> GetTRByUser(int userID)
        {
            var trabajos = await _context.trabajoRealizado
                .Where(tr => tr.idUserTR == userID)
                .ToListAsync();

            if (trabajos == null || trabajos.Count == 0)
            {
                return NotFound(new { mesaage = $"No se encontraron trabajos realizados para el usuario con ID {userID}" });
            }

            return Ok(trabajos);
        }

        //GET: api/TrabajoRealizadoes/ByUserEmpty/{userId}
        [HttpGet("ByUserEmpty/{userID}")]
        public async Task<ActionResult<IEnumerable<TrabajoRealizado>>> GetTRByUserEmpty(int userID)
        {
            var trabajos = await _context.trabajoRealizado
                .Where(tr => tr.idUserTR == userID && string.IsNullOrEmpty(tr.fotoAntes64TR))
                .ToListAsync();

            if (trabajos == null || trabajos.Count == 0)
            {
                return NotFound(new { mesaage = $"No se encontraron trabajos realizados para el usuario con ID {userID}" });
            }

            return Ok(trabajos);
        }


        //GET: api/TrabajoRealizadoes/ByOT/{otID}
        [HttpGet("ByOT/{otID}")]
        public async Task<ActionResult<IEnumerable<TrabajoRealizado>>> GetTRByOT(int otID)
        {
            try
            {
                var trabajos = await _context.trabajoRealizado
                    .Where(tr => tr.idOrdenServicio == otID)
                    .OrderByDescending(tr => tr.idTrabajoRealizado)
                    .ToListAsync();

                if (trabajos == null || trabajos.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron trabajos con OT: {otID}" });
                }
                return Ok(trabajos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar trabajos para OT: {ex.Message}");
            }
        }


        // PUT: api/TrabajoRealizadoes/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabajoRealizado(int id, TrabajoRealizado trabajoRealizado)
        {
            if (id != trabajoRealizado.idTrabajoRealizado)
            {
                return BadRequest();
            }

            _context.Entry(trabajoRealizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaTrabajoRealizadoNube(trabajoRealizado, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajoRealizadoExists(id))
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

        // POST: api/TrabajoRealizadoes
        [HttpPost]
        public async Task<ActionResult<TrabajoRealizado>> PostTrabajoRealizado(TrabajoRealizado trabajoRealizado)
        {
            _context.trabajoRealizado.Add(trabajoRealizado);
            await _context.SaveChangesAsync();
            await ReplicaTrabajoRealizadoNube(trabajoRealizado, "POST");

            return CreatedAtAction("GetTrabajoRealizado", new { id = trabajoRealizado.idTrabajoRealizado }, trabajoRealizado);
        }

        // DELETE: api/TrabajoRealizadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabajoRealizado(int id)
        {
            var trabajoRealizado = await _context.trabajoRealizado.FindAsync(id);
            if (trabajoRealizado == null)
            {
                return NotFound();
            }

            _context.trabajoRealizado.Remove(trabajoRealizado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrabajoRealizadoExists(int id)
        {
            return _context.trabajoRealizado.Any(e => e.idTrabajoRealizado == id);
        }

        private async Task ReplicaTrabajoRealizadoNube(TrabajoRealizado trabajoRealizado, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/TrabajoRealizadoes";

                var jsonContent = JsonSerializer.Serialize(trabajoRealizado);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{trabajoRealizado.idTrabajoRealizado}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{trabajoRealizado.idTrabajoRealizado}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar TRABAJOREALIZADO en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción TRABAJOREALIZADO al replicar en la nube: {ex.Message}");
            }
        }
    }
}
