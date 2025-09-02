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
    public class CanceladosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public CanceladosController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        // GET: api/Canceladoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cancelado>>> GetCancelado()
        {
            return await _context.Cancelado.ToListAsync();
        }

        // GET: api/Canceladoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cancelado>> GetCancelado(int id)
        {
            var cancelado = await _context.Cancelado.FindAsync(id);

            if (cancelado == null)
            {
                return NotFound();
            }

            return cancelado;
        }

        // PUT: api/Canceladoes/5        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCancelado(int id, Cancelado cancelado)
        {
            if (id != cancelado.idCancelacion)
            {
                return BadRequest();
            }

            _context.Entry(cancelado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarCanceladoNube(cancelado, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanceladoExists(id))
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

        // POST: api/Canceladoes
        [HttpPost]
        public async Task<ActionResult<Cancelado>> PostCancelado(Cancelado cancelado)
        {
            _context.Cancelado.Add(cancelado);
            await _context.SaveChangesAsync();
            await ReplicarCanceladoNube(cancelado, "POST");
            return CreatedAtAction("GetCancelado", new { id = cancelado.idCancelacion }, cancelado);
        }

        // DELETE: api/Canceladoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancelado(int id)
        {
            var cancelado = await _context.Cancelado.FindAsync(id);
            if (cancelado == null)
            {
                return NotFound();
            }

            _context.Cancelado.Remove(cancelado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanceladoExists(int id)
        {
            return _context.Cancelado.Any(e => e.idCancelacion == id);
        }

        private async Task ReplicarCanceladoNube(Cancelado cancelado, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Canceladoes";

                var jsonContent = JsonSerializer.Serialize(cancelado);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{cancelado.idCancelacion}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{cancelado.idCancelacion}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar CANCELADO en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Excepción CANCELADO al replicar en la nube: {ex.Message}");
            }
        }
    }
}
