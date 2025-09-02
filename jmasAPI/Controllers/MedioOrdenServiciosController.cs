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
    public class MedioOrdenServiciosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public MedioOrdenServiciosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/MedioOrdenServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedioOrdenServicio>>> GetmedioOrdenServicio()
        {
            return await _context.medioOrdenServicio.ToListAsync();
        }

        // GET: api/MedioOrdenServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedioOrdenServicio>> GetMedioOrdenServicio(int id)
        {
            var medioOrdenServicio = await _context.medioOrdenServicio.FindAsync(id);

            if (medioOrdenServicio == null)
            {
                return NotFound();
            }

            return medioOrdenServicio;
        }

        // PUT: api/MedioOrdenServicios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedioOrdenServicio(int id, MedioOrdenServicio medioOrdenServicio)
        {
            if (id != medioOrdenServicio.idMedio)
            {
                return BadRequest();
            }

            _context.Entry(medioOrdenServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaMedioOSNube(medioOrdenServicio, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedioOrdenServicioExists(id))
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

        // POST: api/MedioOrdenServicios
        [HttpPost]
        public async Task<ActionResult<MedioOrdenServicio>> PostMedioOrdenServicio(MedioOrdenServicio medioOrdenServicio)
        {
            _context.medioOrdenServicio.Add(medioOrdenServicio);
            await _context.SaveChangesAsync();
            await ReplicaMedioOSNube(medioOrdenServicio, "POST");

            return CreatedAtAction("GetMedioOrdenServicio", new { id = medioOrdenServicio.idMedio }, medioOrdenServicio);
        }

        // DELETE: api/MedioOrdenServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedioOrdenServicio(int id)
        {
            var medioOrdenServicio = await _context.medioOrdenServicio.FindAsync(id);
            if (medioOrdenServicio == null)
            {
                return NotFound();
            }

            _context.medioOrdenServicio.Remove(medioOrdenServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedioOrdenServicioExists(int id)
        {
            return _context.medioOrdenServicio.Any(e => e.idMedio == id);
        }

        private async Task ReplicaMedioOSNube(MedioOrdenServicio medioOrdenServicio, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/MedioOrdenServicios";

                var jsonContent = JsonSerializer.Serialize(medioOrdenServicio);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{medioOrdenServicio.idMedio}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{medioOrdenServicio.idMedio}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar MDIOOS en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción MDIOOS al replicar en la nube: {ex.Message}");
            }
        }
    }
}
