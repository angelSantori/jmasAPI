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
    public class CanceladoSalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CanceladoSalidasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/CanceladoSalidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CanceladoSalida>>> GetCanceladoSalida()
        {
            return await _context.CanceladoSalida.ToListAsync();
        }

        // GET: api/CanceladoSalidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CanceladoSalida>> GetCanceladoSalida(int id)
        {
            var canceladoSalida = await _context.CanceladoSalida.FindAsync(id);

            if (canceladoSalida == null)
            {
                return NotFound();
            }

            return canceladoSalida;
        }

        // PUT: api/CanceladoSalidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanceladoSalida(int id, CanceladoSalida canceladoSalida)
        {
            if (id != canceladoSalida.idCanceladoSalida)
            {
                return BadRequest();
            }

            _context.Entry(canceladoSalida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarCanceladoSalidaNube(canceladoSalida, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanceladoSalidaExists(id))
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

        // POST: api/CanceladoSalidas
        [HttpPost]
        public async Task<ActionResult<CanceladoSalida>> PostCanceladoSalida(CanceladoSalida canceladoSalida)
        {
            _context.CanceladoSalida.Add(canceladoSalida);
            await _context.SaveChangesAsync();
            await ReplicarCanceladoSalidaNube(canceladoSalida, "POST");

            return CreatedAtAction("GetCanceladoSalida", new { id = canceladoSalida.idCanceladoSalida }, canceladoSalida);
        }

        // DELETE: api/CanceladoSalidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanceladoSalida(int id)
        {
            var canceladoSalida = await _context.CanceladoSalida.FindAsync(id);
            if (canceladoSalida == null)
            {
                return NotFound();
            }

            _context.CanceladoSalida.Remove(canceladoSalida);
            await _context.SaveChangesAsync();
            await ReplicarCanceladoSalidaNube(canceladoSalida, "DELETE");

            return NoContent();
        }

        private bool CanceladoSalidaExists(int id)
        {
            return _context.CanceladoSalida.Any(e => e.idCanceladoSalida == id);
        }

        private async Task ReplicarCanceladoSalidaNube(CanceladoSalida canceladoSalida, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/CanceladoSalidas";

                var jsonContent = JsonSerializer.Serialize(canceladoSalida);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{canceladoSalida.idCanceladoSalida}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{canceladoSalida.idCanceladoSalida}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar CanceladoSalida en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción CanceladoSalida al replicar en la nube: {ex.Message}");
            }
        }

        
    }
}
