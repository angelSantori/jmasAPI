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
    public class htaPrestamosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public htaPrestamosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/htaPrestamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<htaPrestamo>>> GethtaPrestamo()
        {
            return await _context.htaPrestamo.ToListAsync();
        }

        // GET: api/htaPrestamos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<htaPrestamo>> GethtaPrestamo(int id)
        {
            var htaPrestamo = await _context.htaPrestamo.FindAsync(id);

            if (htaPrestamo == null)
            {
                return NotFound();
            }

            return htaPrestamo;
        }

        // Get: api/htaPrest/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<htaPrestamo>>> GetHPByFolio(string folio)
        {
            // Filtrar las entradas cuyo folio coincida con el valor proporcionado
            var htaprest = await _context.htaPrestamo
                .Where(hp => hp.prestCodFolio == folio)
                .ToListAsync();

            // Verificar si se encontraron registros
            if (htaprest == null || htaprest.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron registros con el folio: ${folio}"});
            }

            return Ok(htaprest);
        }

        // Get: api/htaPrest/nextPrestCodFolio
        [HttpGet("nextPrestCodFolio")]
        public async Task<ActionResult<string>> GetNextPrestCodFolio()
        {
            // Get all unique CodFolio
            var codFolios = await _context.htaPrestamo
                .Where(hp => !string.IsNullOrEmpty(hp.prestCodFolio))
                .Select(hp => hp.prestCodFolio)
                .Distinct()
                .ToListAsync();

            // Parse the codfolios to integeres
            var codFolioNumbers = codFolios
                .Select(cf => int.Parse(cf.Replace("HP", "")))
                .OrderByDescending(n => n)
                .ToList();

            // Get the last numbre o start from 0 if there are no entries
            int lastNumber = codFolioNumbers.FirstOrDefault();
            int nextNumber = lastNumber + 1;

            return Ok($"HP{nextNumber}");

        }



        // PUT: api/htaPrestamos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PuthtaPrestamo(int id, htaPrestamo htaPrestamo)
        {
            if (id != htaPrestamo.idHtaPrestamo)
            {
                return BadRequest();
            }

            _context.Entry(htaPrestamo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaHtaPrestamoNube(htaPrestamo, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!htaPrestamoExists(id))
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

        // POST: api/htaPrestamos
        [HttpPost]
        public async Task<ActionResult<htaPrestamo>> PosthtaPrestamo(htaPrestamo htaPrestamo)
        {
            _context.htaPrestamo.Add(htaPrestamo);
            await _context.SaveChangesAsync();
            await ReplicaHtaPrestamoNube(htaPrestamo, "POST");

            return CreatedAtAction("GethtaPrestamo", new { id = htaPrestamo.idHtaPrestamo }, htaPrestamo);
        }

        // DELETE: api/htaPrestamos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletehtaPrestamo(int id)
        {
            var htaPrestamo = await _context.htaPrestamo.FindAsync(id);
            if (htaPrestamo == null)
            {
                return NotFound();
            }

            _context.htaPrestamo.Remove(htaPrestamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool htaPrestamoExists(int id)
        {
            return _context.htaPrestamo.Any(e => e.idHtaPrestamo == id);
        }

        private async Task ReplicaHtaPrestamoNube(htaPrestamo htaPrestamo, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/htaPrestamos";

                var jsonContent = JsonSerializer.Serialize(htaPrestamo);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{htaPrestamo.idHtaPrestamo}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{htaPrestamo.idHtaPrestamo}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar HTAPRESTAMO en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción HTAPRESTAMO al replicar en la nube: {ex.Message}");
            }
        }
    }
}
