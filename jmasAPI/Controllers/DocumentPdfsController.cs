using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;
using NuGet.Protocol.Core.Types;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentPdfsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public DocumentPdfsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // GET: api/DocumentPdfs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentPdf>>> GetdocumentPdf()
        {
            return await _context.documentPdf.ToListAsync();
        }

        // GET: api/DocumentPdfs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentPdf>> GetDocumentPdf(int id)
        {
            var documentPdf = await _context.documentPdf.FindAsync(id);

            if (documentPdf == null)
            {
                return NotFound();
            }

            return documentPdf;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<DocumentPdf>>> SearchDocuments(
            [FromQuery] string? name,
            [FromQuery] string? docType,
            [FromQuery] string? startDate,
            [FromQuery] string? endDate)
        {
            try
            {
                // Primero obtener todos los documentos que coincidan con los otros filtros
                var query = _context.documentPdf.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(d => d.nombreDocPdf.Contains(name));
                }

                if (!string.IsNullOrEmpty(docType))
                {
                    query = query.Where(d => d.nombreDocPdf.StartsWith(docType));
                }

                // Aplicar filtros de fecha después de materializar la consulta
                var results = await query.ToListAsync();

                // Filtrar por fechas en memoria
                if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out var startDateParsed))
                {
                    results = results.Where(d =>
                    {
                        var datePart = d.fechaDocPdf.Split(' ')[0];
                        return DateTime.ParseExact(datePart, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= startDateParsed;
                    }).ToList();
                }

                if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out var endDateParsed))
                {
                    results = results.Where(d =>
                    {
                        var datePart = d.fechaDocPdf.Split(' ')[0];
                        return DateTime.ParseExact(datePart, "dd/MM/yyyy", CultureInfo.InvariantCulture) <= endDateParsed;
                    }).ToList();
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar documentos: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var document = await _context.documentPdf.FindAsync(id);
            if (document == null || string.IsNullOrEmpty(document.dataDocPdf))
            {
                return NotFound();   
            }

            // Decodificar el base64 a bytes
            var pdfBytes = Convert.FromBase64String(document.dataDocPdf);

            //Devolver como archivo descargable
            return File(pdfBytes, "application/pdf", document.nombreDocPdf);
        }

        // PUT: api/DocumentPdfs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentPdf(int id, DocumentPdf documentPdf)
        {
            if (id != documentPdf.idDocumentPdf)
            {
                return BadRequest();
            }

            _context.Entry(documentPdf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await ReplicarDocumentPdfNube(documentPdf, "PUT");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentPdfExists(id))
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

        // POST: api/DocumentPdfs
        [HttpPost]
        public async Task<ActionResult<DocumentPdf>> PostDocumentPdf(DocumentPdf documentPdf)
        {
            _context.documentPdf.Add(documentPdf);
            await _context.SaveChangesAsync();
            await ReplicarDocumentPdfNube(documentPdf, "POST");

            return CreatedAtAction("GetDocumentPdf", new { id = documentPdf.idDocumentPdf }, documentPdf);
        }

        [HttpPost("save-pdf")]
        public async Task<IActionResult> SavePdf([FromBody] DocumentPdf documentPdf)
        {
            try
            {
                if(documentPdf == null || documentPdf.dataDocPdf == null)
                    return BadRequest("Datos del PDF no válidos");

                _context.documentPdf.Add(documentPdf);
                await _context.SaveChangesAsync();

                return Ok(new {id = documentPdf.idDocumentPdf});
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar PDF: {ex.Message}");
            }
        }

        // DELETE: api/DocumentPdfs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentPdf(int id)
        {
            var documentPdf = await _context.documentPdf.FindAsync(id);
            if (documentPdf == null)
            {
                return NotFound();
            }

            _context.documentPdf.Remove(documentPdf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentPdfExists(int id)
        {
            return _context.documentPdf.Any(e => e.idDocumentPdf == id);
        }

        private async Task ReplicarDocumentPdfNube(DocumentPdf documentPdf, string metodo)
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
                string apiNubeUrl = $"{apiNubeUrlBase}/DocumentPdfs";

                var jsonContent = JsonSerializer.Serialize(documentPdf);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                switch (metodo)
                {
                    case "POST":
                        response = await client.PostAsync(apiNubeUrl, httpContent);
                        break;
                    case "PUT":
                        response = await client.PutAsync($"{apiNubeUrl}/{documentPdf.idDocumentPdf}", httpContent);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync($"{apiNubeUrl}/{documentPdf.idDocumentPdf}");
                        break;
                    default:
                        return;
                }
                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al replicar DOCUMENTPDF en la nube: {response.StatusCode}");
                    Console.WriteLine($"Respuesta del servidor: {responseContent}");
                    Console.WriteLine($"JSON enviado: {jsonContent}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Excepción DOCUMENTPDF al replicar en la nube: {ex.Message}");
            }
        }
    }
}
