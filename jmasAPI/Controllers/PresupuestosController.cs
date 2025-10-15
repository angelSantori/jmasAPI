using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PresupuestosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Presupuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> Getpresupuestos()
        {
            return await _context.presupuestos.ToListAsync();
        }

        // GET: api/Presupuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuestos>> GetPresupuestos(int id)
        {
            var presupuestos = await _context.presupuestos.FindAsync(id);

            if (presupuestos == null)
            {
                return NotFound();
            }

            return presupuestos;
        }

        // GET: api/Presupuestos/ByFolio/{presupuestoFolio}
        [HttpGet("ByFolio/{presupuestoFolio}")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> GetPresupuestoByFolio(string presupuestoFolio)
        {
            //  Filtrar presupuestos cuyo folio coincida con el valor proporcionado
            var presupuestos = await _context.presupuestos
                .Where(pre => pre.presupuestoFolio == presupuestoFolio)
                .ToListAsync();

            //  Verificar si se encontraron registros
            if (presupuestos == null || presupuestos.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron presupuestos con el folio: {presupuestoFolio}" });
            }

            return Ok(presupuestos);
        }

        // Método privado para obtener el folio como string
        private async Task<string> GetNextPresupuestoFolioString()
        {
            var lastPresupuesto = await _context.presupuestos
                .OrderByDescending(pre => pre.idPresupuesto)
                .FirstOrDefaultAsync();

            int nextNumber = lastPresupuesto != null
                ? int.Parse(lastPresupuesto.presupuestoFolio.Replace("PRE", "")) + 1
                : 1;

            return $"PRE{nextNumber}";
        }

        // PUT: api/Presupuestos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresupuestos(int id, Presupuestos presupuestos)
        {
            if (id != presupuestos.idPresupuesto)
            {
                return BadRequest();
            }

            _context.Entry(presupuestos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaPresupuestoNube(presupuestos, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresupuestosExists(id))
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

        // PUT: api/Presupuestos/UpdateMultiple
        [HttpPut("UpdateMultiple")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> UpdateMultiplePresupuestos(List<Presupuestos> presupuestosList)
        {
            if (presupuestosList == null || presupuestosList.Count == 0)
            {
                return BadRequest("No se proporcionaron presupuestos para actualizar");
            }

            try
            {
                foreach (var presupuesto in presupuestosList)
                {
                    var existingPresupuesto = await _context.presupuestos
                        .FirstOrDefaultAsync(p => p.idPresupuesto == presupuesto.idPresupuesto);

                    if (existingPresupuesto == null)
                    {
                        return NotFound($"Presupuesto con ID {presupuesto.idPresupuesto} no encontrado");
                    }

                    // Actualizar solo los campos necesarios
                    existingPresupuesto.presupuestoEstado = presupuesto.presupuestoEstado;
                    // Agregar otros campos que quieras actualizar si es necesario
                    // existingPresupuesto.presupuestoUnidades = presupuesto.presupuestoUnidades;
                    // existingPresupuesto.presupuestoTotal = presupuesto.presupuestoTotal;
                }

                await _context.SaveChangesAsync();

                // Opcional: Replicar en la nube si es necesario
                await ReplicaMultiplePresupuestoNube(presupuestosList);

                return Ok(presupuestosList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar presupuestos: {ex.Message}");
            }
        }

        private async Task<string> GenerateNextPresupuestoFolio()
        {
            var lastPresupuestoFolio = await _context.presupuestos
                .OrderByDescending(pre => pre.idPresupuesto)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastPresupuestoFolio != null && !string.IsNullOrEmpty(lastPresupuestoFolio.presupuestoFolio))
            {
                //  Extraer el número del folio (ej: "PRE123" → 123)
                var folioParts = lastPresupuestoFolio.presupuestoFolio.Replace("PRE", "");
                if (int.TryParse(folioParts, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }

            }

            return $"PRE{nextNumber}";
        }

        // POST: api/Presupuestos/Multiple
        [HttpPost("Multiple")]
        public async Task<ActionResult<IEnumerable<Presupuestos>>> PostMultiplePresupuestos(List<Presupuestos> presupuestosList)
        {
            if (presupuestosList == null || presupuestosList.Count == 0)
            {
                return BadRequest("No se proporcionaron presupuestos");
            }

            //  Generar un único folio para todos los presupuestos
            string nextFolio = await GenerateNextPresupuestoFolio();

            foreach (var presupuesto in presupuestosList)
            {
                presupuesto.presupuestoFolio = nextFolio;
                _context.presupuestos.Add(presupuesto);
            }

            await _context.SaveChangesAsync();
            await ReplicaMultiplePresupuestoNube(presupuestosList);

            return Ok(presupuestosList);
        }

        private async Task ReplicaMultiplePresupuestoNube(List<Presupuestos> presupuestosList)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Presupuestos/MultipleNube";

                var presupuestosNube = presupuestosList.Select(pre => new Presupuestos
                {
                    idPresupuesto = pre.idPresupuesto,
                    presupuestoFolio = pre.presupuestoFolio,
                    presupuestoFecha = pre.presupuestoFecha,
                    presupuestoEstado = pre.presupuestoEstado,
                    idUser = pre.idUser,
                    idPadron = pre.idPadron,
                    idProducto = pre.idProducto,
                }).ToList();

                var jsonContent = JsonSerializer.Serialize(presupuestosNube);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                client.Timeout = TimeSpan.FromSeconds(30);

                var response = await client.PostAsync(apiNubeUrl, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar PRESUPUESTOS MÚLTIPLES en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                }
                else
                {
                    Console.WriteLine($"Presupuesto replicadas exitosamente a la nube. Folio: {presupuestosList.First().presupuestoFolio}");
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Excepción al replicar presupuestos múltiples en la nube: {ex.Message}");
            }
        }

        // POST: api/Presupuestos
        [HttpPost]
        public async Task<ActionResult<Presupuestos>> PostPresupuestos(Presupuestos presupuestos)
        {
            string nextFolio = await GenerateNextPresupuestoFolio();
            presupuestos.presupuestoFolio = nextFolio;

            _context.presupuestos.Add(presupuestos);
            await _context.SaveChangesAsync();
            await ReplicaPresupuestoNube(presupuestos, "POST");

            return CreatedAtAction("GetPresupuestos", new { id = presupuestos.idPresupuesto }, presupuestos);
        }

        // DELETE: api/Presupuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresupuestos(int id)
        {
            var presupuestos = await _context.presupuestos.FindAsync(id);
            if (presupuestos == null)
            {
                return NotFound();
            }

            _context.presupuestos.Remove(presupuestos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PresupuestosExists(int id)
        {
            return _context.presupuestos.Any(e => e.idPresupuesto == id);
        }

        //  Replicar en la nube
        private async Task ReplicaPresupuestoNube(Presupuestos presupuestos, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Presupuestos";

                var jsonContent = JsonSerializer.Serialize(presupuestos);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{presupuestos.idPresupuesto}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{presupuestos.idPresupuesto}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar PRESUPUESTO en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción PRESUPUESTO al replicar en la nube: {ex.Message}");
            }
        }
    }
}
