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
    public class SalidasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidas()
        {
            try
            {
                // Establece timeout de 5 minutos (300 segundos) solo para esta consulta
                _context.Database.SetCommandTimeout(300);

                return await _context.Salidas
                    .OrderByDescending(s => s.Id_Salida)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar salidas: {ex.Message}");
            }
        }

        // GET: api/Salidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salidas>> GetSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);

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

        // PUT: api/Salidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Salidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salidas>> PostSalidas(Salidas salidas)
        {
            _context.Salidas.Add(salidas);
            await _context.SaveChangesAsync();

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
    }
}
