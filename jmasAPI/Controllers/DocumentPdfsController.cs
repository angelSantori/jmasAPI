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

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentPdfsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentPdfsController(ApplicationDbContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentPdf>> PostDocumentPdf(DocumentPdf documentPdf)
        {
            _context.documentPdf.Add(documentPdf);
            await _context.SaveChangesAsync();

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
    }
}
