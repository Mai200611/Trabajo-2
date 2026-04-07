using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoFleet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly DataContext _context;

        public MantenimientosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Mantenimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> GetMantenimientos()
        {
            var mantenimientos = await _context.Mantenimientos.ToListAsync();
            return Ok(mantenimientos);
        }

        // GET: api/Mantenimientos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Mantenimiento>> GetMantenimiento(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (mantenimiento == null)
            {
                return NotFound(new { mensaje = $"El mantenimiento con ID {id} no existe." });
            }

            return Ok(mantenimiento);
        }

        // POST: api/Mantenimientos
        [HttpPost]
        public async Task<ActionResult<Mantenimiento>> PostMantenimiento(Mantenimiento mantenimiento)
        {
            // Verificar si ya existe un mantenimiento con ese ID
            var existe = await _context.Mantenimientos.AnyAsync(m => m.Id == mantenimiento.Id);
            if (existe)
            {
                return Conflict(new { mensaje = $"El mantenimiento con ID {mantenimiento.Id} ya existe." });
            }

            // Verificar que el vehículo asociado existe (si aplica)
            if (mantenimiento.VehiculoId > 0)
            {
                var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == mantenimiento.VehiculoId);
                if (!vehiculoExiste)
                {
                    return BadRequest(new { mensaje = $"El vehículo con ID {mantenimiento.VehiculoId} no existe." });
                }
            }

            _context.Mantenimientos.Add(mantenimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMantenimiento), new { id = mantenimiento.Id }, mantenimiento);
        }

        // PUT: api/Mantenimientos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutMantenimiento(int id, Mantenimiento mantenimiento)
        {
            // Verificar que el ID de la URL coincida con el ID del objeto
            if (id != mantenimiento.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del mantenimiento." });
            }

            // Verificar si el mantenimiento existe
            var existe = await _context.Mantenimientos.AnyAsync(m => m.Id == id);
            if (!existe)
            {
                return NotFound(new { mensaje = $"El mantenimiento con ID {id} no existe." });
            }

            // Verificar que el vehículo asociado existe (si aplica)
            if (mantenimiento.VehiculoId > 0)
            {
                var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == mantenimiento.VehiculoId);
                if (!vehiculoExiste)
                {
                    return BadRequest(new { mensaje = $"El vehículo con ID {mantenimiento.VehiculoId} no existe." });
                }
            }

            // Marcar la entidad como modificada
            _context.Entry(mantenimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Mantenimientos.AnyAsync(m => m.Id == id))
                {
                    return NotFound(new { mensaje = $"El mantenimiento con ID {id} ya no existe." });
                }
                throw;
            }

            return Ok(mantenimiento);
        }

        // DELETE: api/Mantenimientos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMantenimiento(int id)
        {
            // Buscar el mantenimiento
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (mantenimiento == null)
            {
                return NotFound(new { mensaje = $"El mantenimiento con ID {id} no existe." });
            }

            // Eliminar el mantenimiento
            _context.Mantenimientos.Remove(mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - Eliminación exitosa sin contenido
        }
    }
}