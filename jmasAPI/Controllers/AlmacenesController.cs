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
    public class AlmacenesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public AlmacenesController(ApplicationDbContext context, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _context = context;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        // GET: api/Almacenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Almacenes>>> GetAlmacenes()
        {
            return await _context.Almacenes.ToListAsync();
        }

        // GET: api/Almacenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Almacenes>> GetAlmacenes(int id)
        {
            var almacenes = await _context.Almacenes.FindAsync(id);

            if (almacenes == null)
            {
                return NotFound();
            }

            return almacenes;
        }

        // PUT: api/Almacenes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmacenes(int id, Almacenes almacenes)
        {
            if (id != almacenes.Id_Almacen)
            {
                return BadRequest();
            }

            _context.Entry(almacenes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                //  Replicar en la nube después de editar
                await ReplicarAlmacenEnNube(almacenes, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmacenesExists(id))
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

        // POST: api/Almacenes
        [HttpPost]
        public async Task<ActionResult<Almacenes>> PostAlmacenes(Almacenes almacenes)
        {
            _context.Almacenes.Add(almacenes);
            await _context.SaveChangesAsync();

            await ReplicarAlmacenEnNube(almacenes, "POST");

            return CreatedAtAction("GetAlmacenes", new { id = almacenes.Id_Almacen }, almacenes);
        }

        // DELETE: api/Almacenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlmacenes(int id)
        {
            var almacenes = await _context.Almacenes.FindAsync(id);
            if (almacenes == null)
            {
                return NotFound();
            }

            _context.Almacenes.Remove(almacenes);
            await _context.SaveChangesAsync();

            await ReplicarAlmacenEnNube(almacenes, "DELETE");

            return NoContent();
        }

        private bool AlmacenesExists(int id)
        {
            return _context.Almacenes.Any(e => e.Id_Almacen == id);
        }

        private async Task ReplicarAlmacenEnNube(Almacenes almacen, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Almacenes";

                var jsonContent = JsonSerializer.Serialize(almacen);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{almacen.Id_Almacen}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{almacen.Id_Almacen}");
                        break;
                    default:
                        return;
                }

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error al replicar ALMACEN en la nube: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción ALMACEN al replicar en la nube: {ex.Message}");
            }
        }
    }
}