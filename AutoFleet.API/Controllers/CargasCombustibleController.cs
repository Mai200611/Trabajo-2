using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoFleet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargasCombustibleController : ControllerBase
    {
        private readonly DataContext _context;

        public CargasCombustibleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CargasCombustible
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargaCombustible>>> GetCargasCombustible()
        {
            var consumos = await _context.CargasCombustible
                .Include(c => c.Vehiculo)
                .ToListAsync();
            return Ok(consumos);
        }

        // GET: api/CargasCombustible/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CargaCombustible>> GetCargaCombustible(int id)
        {
            var consumo = await _context.CargasCombustible
                .Include(c => c.Vehiculo)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consumo == null)
            {
                return NotFound(new { mensaje = $"La carga de combustible con ID {id} no existe." });
            }

            return Ok(consumo);
        }

        // POST: api/CargasCombustible
        [HttpPost]
        public async Task<ActionResult<CargaCombustible>> PostCargaCombustible(CargaCombustible consumo)
        {
            // Verificar si ya existe un consumo con ese ID
            var existe = await _context.CargasCombustible.AnyAsync(c => c.Id == consumo.Id);
            if (existe)
            {
                return Conflict(new { mensaje = $"La carga de combustible con ID {consumo.Id} ya existe." });
            }

            // Validar que el vehículo exista
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == consumo.VehiculoId);
            if (!vehiculoExiste)
            {
                return BadRequest(new { mensaje = $"El vehículo con ID {consumo.VehiculoId} no existe." });
            }

            _context.CargasCombustible.Add(consumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCargaCombustible), new { id = consumo.Id }, consumo);
        }

        // PUT: api/CargasCombustible/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCargaCombustible(int id, CargaCombustible consumo)
        {
            // Verificar que el ID de la URL coincida con el ID del objeto
            if (id != consumo.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del consumo." });
            }

            // Verificar si el consumo existe
            var existe = await _context.CargasCombustible.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound(new { mensaje = $"La carga de combustible con ID {id} no existe." });
            }

            // Validar que el vehículo exista
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == consumo.VehiculoId);
            if (!vehiculoExiste)
            {
                return BadRequest(new { mensaje = $"El vehículo con ID {consumo.VehiculoId} no existe." });
            }

            // Validar que el kilometraje sea válido
            if (consumo.Kilometraje < 0)
            {
                return BadRequest(new { mensaje = "El kilometraje no puede ser negativo." });
            }

            // Marcar la entidad como modificada
            _context.Entry(consumo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CargasCombustible.AnyAsync(c => c.Id == id))
                {
                    return NotFound(new { mensaje = $"La carga de combustible con ID {id} ya no existe." });
                }
                throw;
            }

            return Ok(consumo);
        }

        // DELETE: api/CargasCombustible/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCargaCombustible(int id)
        {
            // Buscar el consumo
            var consumo = await _context.CargasCombustible.FindAsync(id);

            if (consumo == null)
            {
                return NotFound(new { mensaje = $"La carga de combustible con ID {id} no existe." });
            }

            // Eliminar el registro de tanqueo
            _context.CargasCombustible.Remove(consumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}