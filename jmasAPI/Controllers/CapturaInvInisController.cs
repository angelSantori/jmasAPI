﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using jmasAPI;
using jmasAPI.Models;

namespace jmasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapturaInvInisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CapturaInvInisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CapturaInvInis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CapturaInvIni>>> GetCapturaInvIni()
        {
            return await _context.CapturaInvIni.ToListAsync();
        }

        // GET: api/CapturaInvInis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CapturaInvIni>> GetCapturaInvIni(int id)
        {
            var capturaInvIni = await _context.CapturaInvIni.FindAsync(id);

            if (capturaInvIni == null)
            {
                return NotFound();
            }

            return capturaInvIni;
        }

        // GET: api/CapturaInvInis/ByProducto/{productoId}
        [HttpGet("ByProducto/{productoId}")]
        public async Task<ActionResult<IEnumerable<CapturaInvIni>>> GetCIIxProducto(int productoId)
        {
            var capturaii = await _context.CapturaInvIni
                .Where(cii => cii.Id_Producto == productoId)
                .ToListAsync();

            if (capturaii == null || capturaii.Count == 0)
            {
                return NotFound(new { message = $"No se encotraron capturas con productoId: {productoId}" });
            }

            return Ok(capturaii);
        }

        // GET: api/CapturaInvInis/ByMonth/{month}/{year}
        [HttpGet("ByMonth/{month}/{year}")]
        public async Task<ActionResult<IEnumerable<CapturaInvIni>>> GetConteoInicialByMonth(int month, int year)
        {
            // Formateamos el mes para que tenga 2 dígitos
            string monthFormatted = month.ToString().PadLeft(2, '0');

            // Convertimos el año completo a formato de 2 dígitos (ej: 2025 -> 25)
            string yearFormatted = (year % 100).ToString().PadLeft(2, '0');

            // Buscamos registros donde la fecha contenga el mes/año especificado
            // Usando Contains para manejar el formato dd/MM/yy y los espacios al final
            var conteoInicial = await _context.CapturaInvIni
                .Where(c => c.invIniFecha != null &&
                           c.invIniFecha.Trim().Contains($"/{monthFormatted}/{yearFormatted}"))
                .ToListAsync();

            if (conteoInicial == null || conteoInicial.Count == 0)
            {
                return NotFound(new { message = $"No se encontraron registros de conteo inicial para el mes {month} del año {year}" });
            }

            return Ok(conteoInicial);
        }

        // PUT: api/CapturaInvInis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCapturaInvIni(int id, CapturaInvIni capturaInvIni)
        {
            if (id != capturaInvIni.idInvIni)
            {
                return BadRequest();
            }

            _context.Entry(capturaInvIni).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CapturaInvIniExists(id))
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

        // POST: api/CapturaInvInis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CapturaInvIni>> PostCapturaInvIni(CapturaInvIni capturaInvIni)
        {
            _context.CapturaInvIni.Add(capturaInvIni);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCapturaInvIni", new { id = capturaInvIni.idInvIni }, capturaInvIni);
        }

        // DELETE: api/CapturaInvInis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapturaInvIni(int id)
        {
            var capturaInvIni = await _context.CapturaInvIni.FindAsync(id);
            if (capturaInvIni == null)
            {
                return NotFound();
            }

            _context.CapturaInvIni.Remove(capturaInvIni);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CapturaInvIniExists(int id)
        {
            return _context.CapturaInvIni.Any(e => e.idInvIni == id);
        }
    }
}
