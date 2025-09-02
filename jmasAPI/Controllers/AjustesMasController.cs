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
    public class AjustesMasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public AjustesMasController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        // GET: api/AjustesMas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AjustesMas>>> GetAjustesMas()
        {
            return await _context.AjustesMas.ToListAsync();
        }

        // GET: api/AjustesMas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AjustesMas>> GetAjustesMas(int id)
        {
            var ajustesMas = await _context.AjustesMas.FindAsync(id);

            if (ajustesMas == null)
            {
                return NotFound();
            }

            return ajustesMas;
        }

        //Get api/AjustesMas/nextFolioAJM
        [HttpGet("nextFolioAJM")]
        public async Task<ActionResult<string>> GetNextFolioAJM()
        {
            var lastAJM = await _context.AjustesMas
                .OrderByDescending(ajm => ajm.Id_AjusteMas)
                .FirstOrDefaultAsync();
            
            int nextNumber = lastAJM != null
                ? int.Parse(lastAJM.AjusteMas_CodFolio.Replace("AJM", "")) +1
                : 1;

            return Ok($"AJM{nextNumber}");
        }

        // PUT: api/AjustesMas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAjustesMas(int id, AjustesMas ajustesMas)
        {
            if (id != ajustesMas.Id_AjusteMas)
            {
                return BadRequest();
            }

            _context.Entry(ajustesMas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarAjusteMasNube(ajustesMas, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AjustesMasExists(id))
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

        // POST: api/AjustesMas
        [HttpPost]
        public async Task<ActionResult<AjustesMas>> PostAjustesMas(AjustesMas ajustesMas)
        {
            _context.AjustesMas.Add(ajustesMas);
            await _context.SaveChangesAsync();
            await ReplicarAjusteMasNube(ajustesMas, "POST");

            return CreatedAtAction("GetAjustesMas", new { id = ajustesMas.Id_AjusteMas }, ajustesMas);
        }

        // DELETE: api/AjustesMas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAjustesMas(int id)
        {
            var ajustesMas = await _context.AjustesMas.FindAsync(id);
            if (ajustesMas == null)
            {
                return NotFound();
            }

            _context.AjustesMas.Remove(ajustesMas);
            await _context.SaveChangesAsync();
            await ReplicarAjusteMasNube(ajustesMas, "DELETE");

            return NoContent();
        }

        private bool AjustesMasExists(int id)
        {
            return _context.AjustesMas.Any(e => e.Id_AjusteMas == id);
        }


        private async Task ReplicarAjusteMasNube(AjustesMas ajustesMas, string metodo) 
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
                string apiNubeUrl = $"{apiNubeUrlBase}/AjustesMas";

                var jsonContent = JsonSerializer.Serialize(ajustesMas);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo) 
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{ajustesMas.Id_AjusteMas}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{ajustesMas.Id_AjusteMas}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar AJUSTEMAS en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Excepción AJUSTEMAS al replicar en la nube: {ex.Message}");
            }
        }
    }
}
