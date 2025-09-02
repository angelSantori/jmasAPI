using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using System.Text.Json;
using System.Text;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public CallesController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        // GET: api/Calles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calle>>> GetCalle()
        {
            return await _context.Calle.ToListAsync();
        }

        // GET: api/Calles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calle>> GetCalle(int id)
        {
            var calle = await _context.Calle.FindAsync(id);

            if (calle == null)
            {
                return NotFound();
            }

            return calle;
        }

        // GET: api/Calles/BuscarPorNombre?nombre={nombre}
        [HttpGet("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Calle>>> BuscarPorNombre([FromQuery] string nombreCalle)
        {
            if (string.IsNullOrWhiteSpace(nombreCalle))
            {
                return BadRequest("El párametro 'nombreCalle' es requerido");
            }
            return await _context.Calle
                .Where(calle => calle.calleNombre != null &&
                                calle.calleNombre.ToLower().Contains(nombreCalle.ToLower()))
                .Take(30)
                .ToListAsync();
        }

        // PUT: api/Calles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalle(int id, Calle calle)
        {
            if (id != calle.idCalle)
            {
                return BadRequest();
            }

            _context.Entry(calle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarCalleEnNube(calle, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalleExists(id))
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

        // POST: api/Calles
        [HttpPost]
        public async Task<ActionResult<Calle>> PostCalle(Calle calle)
        {
            _context.Calle.Add(calle);
            await _context.SaveChangesAsync();
            await ReplicarCalleEnNube(calle, "POST");

            return CreatedAtAction("GetCalle", new { id = calle.idCalle }, calle);
        }

        // DELETE: api/Calles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalle(int id)
        {
            var calle = await _context.Calle.FindAsync(id);
            if (calle == null)
            {
                return NotFound();
            }

            _context.Calle.Remove(calle);
            await _context.SaveChangesAsync();
            await ReplicarCalleEnNube(calle, "DELETE");

            return NoContent();
        }

        private bool CalleExists(int id)
        {
            return _context.Calle.Any(e => e.idCalle == id);
        }

        private async Task ReplicarCalleEnNube(Calle calle, string metodo)
        {
            bool replicacionHabilitada = _configuration.GetValue<bool>("Replicacion:Habilitada");

            if (!replicacionHabilitada)
            {
                return;
            }

            try
            {
                var client = _clientFactory.CreateClient();
                string apiNubeUrlBase = _configuration.GetValue<string>("Replicacion:UrlApiNube");
                string apiNubeUrl = $"{apiNubeUrlBase}/Calles";

                var jsonContent = JsonSerializer.Serialize(calle);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo) 
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{calle.idCalle}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{calle.idCalle}");
                        break;
                    default:
                        return;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar CALLE en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción CALLE al replicar en la nube: {ex.Message}");
            }
        }
    }
}
