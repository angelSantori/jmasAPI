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
using jmasAPI.DataTransferObjects;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public SalidasController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaListaDTO>>> GetSalidas()
        {
            try
            {
                return await _context.Salidas
                    .OrderByDescending(s => s.Id_Salida)
                    .Select(s => new SalidaListaDTO
                    {
                        Id_Salida = s.Id_Salida,
                        Salida_CodFolio = s.Salida_CodFolio,
                        Salida_Referencia = s.Salida_Referencia,
                        Salida_Estado = s.Salida_Estado,
                        Salida_Unidades = s.Salida_Unidades,
                        Salida_Costo = s.Salida_Costo,
                        Salida_Fecha = s.Salida_Fecha,
                        Salida_Comentario = s.Salida_Comentario,
                        Salida_TipoTrabajo = s.Salida_TipoTrabajo,
                        Salida_DocumentoFirma = s.Salida_DocumentoFirma,
                        Salida_Pagado = s.Salida_Pagado,
                        idProducto = s.idProducto,
                        Id_User = s.Id_User,
                        Id_Junta = s.Id_Junta,
                        Id_Almacen = s.Id_Almacen,
                        Id_User_Asignado = s.Id_User_Asignado,
                        idPadron = s.idPadron,
                        idCalle = s.idCalle,
                        idColonia = s.idColonia,
                        idOrdenServicio = s.idOrdenServicio,
                        idUserAutoriza = s.idUserAutoriza
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar salidas: {ex.Message}");
            }
        }

        // GET: api/Salidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaDetalleDTO>> GetSalidas(int id)
        {
            var salidas = await _context.Salidas
                .Where(s => s.Id_Salida == id)
                .Select(s => new SalidaDetalleDTO
                {
                    Id_Salida = s.Id_Salida,
                    Salida_CodFolio = s.Salida_CodFolio,
                    Salida_Referencia = s.Salida_Referencia,
                    Salida_Estado = s.Salida_Estado,
                    Salida_Unidades = s.Salida_Unidades,
                    Salida_Costo = s.Salida_Costo,
                    Salida_Fecha = s.Salida_Fecha,
                    Salida_Comentario = s.Salida_Comentario,
                    Salida_TipoTrabajo = s.Salida_TipoTrabajo,
                    Salida_Imag64Orden = s.Salida_Imag64Orden,
                    Salida_DocumentoFirmas = s.Salida_DocumentoFirmas,
                    idProducto = s.idProducto,
                    Id_User = s.Id_User,
                    Id_Junta = s.Id_Junta,
                    Id_Almacen = s.Id_Almacen,
                    Id_User_Asignado = s.Id_User_Asignado,
                    idPadron = s.idPadron,
                    idCalle = s.idCalle,
                    idColonia = s.idColonia,
                    idOrdenServicio = s.idOrdenServicio,
                    idUserAutoriza = s.idUserAutoriza
                })
                .FirstOrDefaultAsync();

            if (salidas == null)
            {
                return NotFound();
            }

            return salidas;
        }

        // GET: api/Salidas/ByFolio/{folio}
        [HttpGet("ByFolio/{folio}")]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidasByFolio(string folio)
        {
            // Filtrar las entradas cuyo folio coincida con el valor proporcionado
            var salidas = await _context.Salidas
                .Where(e => e.Salida_CodFolio == folio)
                .ToListAsync();

            // Verificar si se encontraron registros
            if (salidas == null || salidas.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron entradas con el folio: {folio}" });
            }

            return Ok(salidas);
        }

        //Get api/Salias/next-folio
        [HttpGet("next-salidacodfolio")]
        public async Task<ActionResult<string>> GetNextSalidaCodFolio()
        {
            var lastSalida = await _context.Salidas
                .OrderByDescending(s => s.Id_Salida)
                .FirstOrDefaultAsync();

            int nextNumber = lastSalida != null
                ? int.Parse(lastSalida.Salida_CodFolio.Replace("Sal", "")) + 1
                : 1;

            return Ok($"Sal{nextNumber}");
        }

        // GET: api/Salidas/ByUserAsignado/{userId}
        [HttpGet("ByUserAsignado/{userId}")]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidasByUserAsignado(int userId)
        {
            try
            {
                var salidas = await _context.Salidas
                    .Where(s => s.Id_User_Asignado == userId)
                    .OrderByDescending(s => s.Id_Salida)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return NotFound(new { message = $"No se enxontraron salidas asignadas al usuario con ID: {userId}" });
                }

                return Ok(salidas);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Error al cargar salidas por usuario asignado: {ex.Message}");
            }
        }

        // GET: api/Salidas/ByOT/{userId}
        [HttpGet("ByOT/{otId}")]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidaByOT(int otId)
        {
            var salidas = await _context.Salidas
                .Where(sal => sal.idOrdenServicio == otId)
                .ToListAsync();

            if (salidas == null || salidas.Count == 0)
            {
                return NotFound(new { message = $"No se ecnotraron salidas OT con ID {otId}" });
            }

            return Ok(salidas);
        }

        [HttpGet("ByMonth/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidasByMonth(int month, int year)
        {
            try
            {
                string monthFormatted = month.ToString().PadLeft(2, '0');

                var salidas = await _context.Salidas
                    .Where(s => s.Salida_Fecha != null &&
                                s.Salida_Fecha.Contains($"/{monthFormatted}/{year}") &&
                                s.Salida_Estado == true)
                    .ToListAsync();

                if (salidas == null || salidas.Count == 0)
                {
                    return NotFound(new { message = $"No se encontraron salidas para el mes {month} del año {year}" });
                }

                return Ok(salidas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener salidas por mes: {ex.Message}");
            }
        }

        private async Task<string> GenerateNextFolio()
        {
            var lastFolioSalida = await _context.Salidas
                .OrderByDescending(salida => salida.Id_Salida)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastFolioSalida != null && !string.IsNullOrEmpty(lastFolioSalida.Salida_CodFolio))
            {
                // Extraer el número del folio (ej: "Sal123" → 123)
                var folioParts = lastFolioSalida.Salida_CodFolio.Replace("Sal", "");
                if (int.TryParse(folioParts, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"Sal{nextNumber}";
        }

        // POST: api/Salidas/UploadDocumentoFirmas/{folio}
        [HttpPost("UploadDocumentoFirmas/{folio}")]
        public async Task<IActionResult> UploadDocumentoFirmas(string folio, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No se ha proporcionado un archivo válido");

                // Verificar que existe al menos una salida con este folio
                var salidasConFolio = await _context.Salidas
                    .Where(s => s.Salida_CodFolio == folio)
                    .ToListAsync();

                if (salidasConFolio == null || salidasConFolio.Count == 0)
                    return NotFound($"No se encontraron salidas con el folio: {folio}");

                // Convertir el archivo a base64
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var base64String = Convert.ToBase64String(fileBytes);

                // Actualizar solo la primera salida del folio con el documento
                var primeraSalida = salidasConFolio.First();
                primeraSalida.Salida_DocumentoFirmas = base64String;
                primeraSalida.Salida_DocumentoFirma = true;

                _context.Entry(primeraSalida).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Documento de firmas subido correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir el documento: {ex.Message}");
            }
        }

        // GET: api/Salidas/GetDocumentoFirmas/{folio}
        [HttpGet("GetDocumentoFirmas/{folio}")]
        public async Task<IActionResult> GetDocumentoFirmas(string folio)
        {
            try
            {
                // Buscar la primera salida con este folio que tenga documento de firmas
                var salidaConDocumento = await _context.Salidas
                    .Where(s => s.Salida_CodFolio == folio && !string.IsNullOrEmpty(s.Salida_DocumentoFirmas))
                    .FirstOrDefaultAsync();

                if (salidaConDocumento == null)
                    return NotFound("No se encontró documento de firmas para este folio");

                return Ok(new
                {
                    documentoBase64 = salidaConDocumento.Salida_DocumentoFirmas,
                    folio = salidaConDocumento.Salida_CodFolio
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar el documento: {ex.Message}");
            }
        }

        // PUT: api/Salidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalidas(int id, Salidas salidas)
        {
            if (id != salidas.Id_Salida)
            {
                return BadRequest();
            }

            _context.Entry(salidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicaSalidaNube(salidas, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidasExists(id))
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

        // POST: api/Salidas/MultipleNube
        [HttpPost("MultipleNube")]
        public async Task<ActionResult<IEnumerable<Salidas>>> PostMultipleSalidasNube(List<Salidas> salidasList)
        {
            if (salidasList == null || salidasList.Count == 0)
            {
                return BadRequest("No se proporcionaron salidas");
            }

            try
            {
                // Verificar que todas las salidas tengan el mismo folio
                var folioDesdeLocal = salidasList.First().Salida_CodFolio;

                if (string.IsNullOrEmpty(folioDesdeLocal))
                {
                    return BadRequest("El folio recibido desde local está vacío");
                }

                if (salidasList.Any(s => s.Salida_CodFolio != folioDesdeLocal))
                {
                    return BadRequest("Todas las salidas deben tener el mismo folio");
                }

                // USAR EL FOLIO QUE VIENE DESDE LOCAL, NO GENERAR UNO NUEVO
                foreach (var salida in salidasList)
                {
                    // Mantener el ID_Salida en 0 para que la nube genere sus propios IDs
                    // pero conservar el folio original desde local
                    salida.Id_Salida = 0;
                    _context.Salidas.Add(salida);
                }

                await _context.SaveChangesAsync();

                return Ok(salidasList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar salidas múltiples en nube: {ex.Message}");
            }
        }

        // POST: api/Salidas/Multiple
        // POST: api/Salidas/Multiple
        [HttpPost("Multiple")]
        public async Task<ActionResult<IEnumerable<Salidas>>> PostMultipleSalidas(List<Salidas> salidasList)
        {
            if (salidasList == null || salidasList.Count == 0)
            {
                return BadRequest("No se proporcionaron salidas");
            }

            // Generar un único folio para todas las salidas
            string nextFolio = await GenerateNextFolio();
            Console.WriteLine($"Generando folio: {nextFolio} para {salidasList.Count} salidas");

            foreach (var salida in salidasList)
            {
                salida.Salida_CodFolio = nextFolio;
                _context.Salidas.Add(salida);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"Salidas guardadas en local con folio: {nextFolio}");

            // Replicar TODAS las salidas del mismo folio en la nube
            await ReplicaMultipleSalidasNube(salidasList);

            return Ok(salidasList);
        }

        // Nuevo método para replicar múltiples salidas a la nube
        private async Task ReplicaMultipleSalidasNube(List<Salidas> salidas)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Salidas/MultipleNube";

                // Asegurarse de que todas las salidas tengan los IDs correctos para la nube
                var salidasParaNube = salidas.Select(s => new Salidas
                {
                    // Mantener mismo ID si es necesario, o dejar que la nube genere nuevos
                    Id_Salida = 0, // La nube generará nuevos IDs
                    Salida_CodFolio = s.Salida_CodFolio,
                    Salida_Referencia = s.Salida_Referencia,
                    Salida_Estado = s.Salida_Estado,
                    Salida_Unidades = s.Salida_Unidades,
                    Salida_Costo = s.Salida_Costo,
                    Salida_Fecha = s.Salida_Fecha,
                    Salida_TipoTrabajo = s.Salida_TipoTrabajo,
                    Salida_Comentario = s.Salida_Comentario,
                    Salida_Imag64Orden = s.Salida_Imag64Orden,
                    Salida_DocumentoFirmas = s.Salida_DocumentoFirmas,
                    idProducto = s.idProducto,
                    Id_User = s.Id_User,
                    Id_Junta = s.Id_Junta,
                    Id_Almacen = s.Id_Almacen,
                    Id_User_Asignado = s.Id_User_Asignado,
                    idPadron = s.idPadron,
                    idCalle = s.idCalle,
                    idColonia = s.idColonia,
                    idOrdenServicio = s.idOrdenServicio,
                    idUserAutoriza = s.idUserAutoriza
                }).ToList();

                var jsonContent = JsonSerializer.Serialize(salidasParaNube);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Agregar timeout para evitar bloqueos prolongados
                client.Timeout = TimeSpan.FromSeconds(30);

                var response = await client.PostAsync(apiNubeUrl, httpContent);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar SALIDAS MÚLTIPLES en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");

                    // Opcional: puedes loggear el error pero no fallar la operación principal
                }
                else
                {
                    Console.WriteLine($"Salidas replicadas exitosamente a la nube. Folio: {salidas.First().Salida_CodFolio}");
                }
            }
            catch (Exception ex)
            {
                // Loggear el error pero no fallar la operación principal
                Console.WriteLine($"Excepción al replicar salidas múltiples en la nube: {ex.Message}");
                // Considerar agregar un sistema de reintentos aquí
            }
        }

        // POST: api/Salidas
        [HttpPost]
        public async Task<ActionResult<Salidas>> PostSalidas(Salidas salidas)
        {
            string nextFolio = await GenerateNextFolio();
            salidas.Salida_CodFolio = nextFolio;

            _context.Salidas.Add(salidas);
            await _context.SaveChangesAsync();
            await ReplicaSalidaNube(salidas, "POST");

            return CreatedAtAction("GetSalidas", new { id = salidas.Id_Salida }, salidas);
        }

        // DELETE: api/Salidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);
            if (salidas == null)
            {
                return NotFound();
            }

            _context.Salidas.Remove(salidas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalidasExists(int id)
        {
            return _context.Salidas.Any(e => e.Id_Salida == id);
        }

        private async Task ReplicaSalidaNube(Salidas salidas, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/Salidas";

                var jsonContent = JsonSerializer.Serialize(salidas);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{salidas.Id_Salida}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{salidas.Id_Salida}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar SALIDA en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción SALIDA al replicar en la nube: {ex.Message}");
            }
        }
    }
}
