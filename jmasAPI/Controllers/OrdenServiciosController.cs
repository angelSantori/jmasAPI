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
    public class OrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OrdenServiciosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/ordenServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenServicio>>> GetordenServicio()
        {
            return await _context.ordenServicio.ToListAsync();
        }

        // GET: api/ordenServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenServicio>> GetordenServicio(int id)
        {
            var ordenServicio = await _context.ordenServicio.FindAsync(id);

            if (ordenServicio == null)
            {
                return NotFound();
            }

            return ordenServicio;
        }

        // GET: api/ordenServicios/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<OrdenServicio>>> GetordenServicioXFolio(string folio)
        {
            var ordenServicio = await _context.ordenServicio
                .Where(os => os.folioOS == folio)
                .ToListAsync();

            //Verificar si se encontraron registros
            if (ordenServicio == null || ordenServicio.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron ordenServicio con el folio: {folio}" });
            }

            return Ok(ordenServicio);
        }

        //Get api/ordenServicios/next-folio
        [HttpGet("nextOTFolio")]
        public async Task<ActionResult<string>> GetNextordenServicioFolio()
        {
            var lastOT = await _context.ordenServicio
                .OrderByDescending(os => os.idOrdenServicio)
                .FirstOrDefaultAsync();

            int nextNumber = lastOT != null
                ? int.Parse(lastOT.folioOS.Replace("OS", "")) + 1
                : 1;

            return Ok($"OS{nextNumber}");
        }

        // PUT: api/ordenServicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutordenServicio(int id, OrdenServicio ordenServicio)
        {
            if (id != ordenServicio.idOrdenServicio)
            {
                return BadRequest();
            }

            _context.Entry(ordenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaOrdenServicioNube(ordenServicio, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ordenServicioExists(id))
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



        // POST: api/ordenServicioes
        [HttpPost]
        public async Task<ActionResult<OrdenServicio>> PostordenServicio(OrdenServicio ordenServicio)
        {
            _context.ordenServicio.Add(ordenServicio);
            await _context.SaveChangesAsync();
            await ReplicaOrdenServicioNube(ordenServicio, "POST");

            return CreatedAtAction("GetordenServicio", new { id = ordenServicio.idOrdenServicio}, ordenServicio);
        }

        // DELETE: api/ordenServicioes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteordenServicio(int id)
        {
            var ordenServicio = await _context.ordenServicio.FindAsync(id);
            if (ordenServicio == null)
            {
                return NotFound();
            }

            _context.ordenServicio.Remove(ordenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ordenServicioExists(int id)
        {
            return _context.ordenServicio.Any(e => e.idOrdenServicio == id);
        }

        private async Task ReplicaOrdenServicioNube(OrdenServicio ordenServicio, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/ordenServicios";

                var jsonContent = JsonSerializer.Serialize(ordenServicio);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{ordenServicio.idOrdenServicio}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{ordenServicio.idOrdenServicio}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar ORDENSERVICIO en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción ORDENSERVICIO al replicar en la nube: {ex.Message}");
            }
        }
    }
}
