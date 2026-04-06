using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoFleet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecorridosController : ControllerBase
    {
        private readonly DataContext _context;

        public RecorridosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Recorridos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recorrido>>> GetRecorridos()
        {
            var recorridos = await _context.Recorridos
                .Include(r => r.Vehiculo)
                .Include(r => r.Conductor)
                .Include(r => r.Ruta)
                .ToListAsync();
            return Ok(recorridos);
        }

        // GET: api/Recorridos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Recorrido>> GetRecorrido(int id)
        {
            var recorrido = await _context.Recorridos
                .Include(r => r.Vehiculo)
                .Include(r => r.Conductor)
                .Include(r => r.Ruta)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recorrido == null)
            {
                return NotFound(new { mensaje = $"El recorrido con ID {id} no existe." });
            }

            return Ok(recorrido);
        }

        // GET: api/Recorridos/vehiculo/{vehiculoId}
        [HttpGet("vehiculo/{vehiculoId:int}")]
        public async Task<ActionResult<IEnumerable<Recorrido>>> GetRecorridosPorVehiculo(int vehiculoId)
        {
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == vehiculoId);
            if (!vehiculoExiste)
            {
                return NotFound(new { mensaje = $"El vehículo con ID {vehiculoId} no existe." });
            }

            var recorridos = await _context.Recorridos
                .Include(r => r.Vehiculo)
                .Include(r => r.Conductor)
                .Include(r => r.Ruta)
                .Where(r => r.VehiculoId == vehiculoId)
                .ToListAsync();

            return Ok(recorridos);
        }

        // GET: api/Recorridos/conductor/{conductorId}
        [HttpGet("conductor/{conductorId:int}")]
        public async Task<ActionResult<IEnumerable<Recorrido>>> GetRecorridosPorConductor(int conductorId)
        {
            var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == conductorId);
            if (!conductorExiste)
            {
                return NotFound(new { mensaje = $"El conductor con ID {conductorId} no existe." });
            }

            var recorridos = await _context.Recorridos
                .Include(r => r.Vehiculo)
                .Include(r => r.Conductor)
                .Include(r => r.Ruta)
                .Where(r => r.ConductorId == conductorId)
                .ToListAsync();

            return Ok(recorridos);
        }

        // POST: api/Recorridos
        [HttpPost]
        public async Task<ActionResult<Recorrido>> PostRecorrido(Recorrido recorrido)
        {
            // Verificar si ya existe un recorrido con ese ID
            var existe = await _context.Recorridos.AnyAsync(r => r.Id == recorrido.Id);
            if (existe)
            {
                return Conflict(new { mensaje = $"El recorrido con ID {recorrido.Id} ya existe." });
            }

            // Validar que el vehículo exista
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == recorrido.VehiculoId);
            if (!vehiculoExiste)
            {
                return BadRequest(new { mensaje = $"El vehículo con ID {recorrido.VehiculoId} no existe." });
            }

            // Validar que el conductor exista
            var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == recorrido.ConductorId);
            if (!conductorExiste)
            {
                return BadRequest(new { mensaje = $"El conductor con ID {recorrido.ConductorId} no existe." });
            }

            // Validar que la ruta exista
            var rutaExiste = await _context.Rutas.AnyAsync(r => r.Id == recorrido.RutaId);
            if (!rutaExiste)
            {
                return BadRequest(new { mensaje = $"La ruta con ID {recorrido.RutaId} no existe." });
            }

            // Validar que la hora de llegada sea después de la hora de salida
            if (recorrido.HoraLlegada <= recorrido.HoraSalida)
            {
                return BadRequest(new { mensaje = "La hora de llegada debe ser posterior a la hora de salida." });
            }

            // Validar que el kilometraje final sea mayor o igual al inicial
            if (recorrido.KmFinal < recorrido.KmInicial)
            {
                return BadRequest(new { mensaje = "El kilometraje final debe ser mayor o igual al kilometraje inicial." });
            }

            _context.Recorridos.Add(recorrido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecorrido), new { id = recorrido.Id }, recorrido);
        }

        // PUT: api/Recorridos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutRecorrido(int id, Recorrido recorrido)
        {
            // Verificar que el ID de la URL coincida con el ID del objeto
            if (id != recorrido.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del recorrido." });
            }

            // Verificar si el recorrido existe
            var existe = await _context.Recorridos.AnyAsync(r => r.Id == id);
            if (!existe)
            {
                return NotFound(new { mensaje = $"El recorrido con ID {id} no existe." });
            }

            // Validar que el vehículo exista
            var vehiculoExiste = await _context.Vehiculos.AnyAsync(v => v.Id == recorrido.VehiculoId);
            if (!vehiculoExiste)
            {
                return BadRequest(new { mensaje = $"El vehículo con ID {recorrido.VehiculoId} no existe." });
            }

            // Validar que el conductor exista
            var conductorExiste = await _context.Conductores.AnyAsync(c => c.Id == recorrido.ConductorId);
            if (!conductorExiste)
            {
                return BadRequest(new { mensaje = $"El conductor con ID {recorrido.ConductorId} no existe." });
            }

            // Validar que la ruta exista
            var rutaExiste = await _context.Rutas.AnyAsync(r => r.Id == recorrido.RutaId);
            if (!rutaExiste)
            {
                return BadRequest(new { mensaje = $"La ruta con ID {recorrido.RutaId} no existe." });
            }

            // Validar que la hora de llegada sea después de la hora de salida
            if (recorrido.HoraLlegada <= recorrido.HoraSalida)
            {
                return BadRequest(new { mensaje = "La hora de llegada debe ser posterior a la hora de salida." });
            }

            // Validar que el kilometraje final sea mayor o igual al inicial
            if (recorrido.KmFinal < recorrido.KmInicial)
            {
                return BadRequest(new { mensaje = "El kilometraje final debe ser mayor o igual al kilometraje inicial." });
            }

            // Marcar la entidad como modificada
            _context.Entry(recorrido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Recorridos.AnyAsync(r => r.Id == id))
                {
                    return NotFound(new { mensaje = $"El recorrido con ID {id} ya no existe." });
                }
                throw;
            }

            return Ok(recorrido);
        }

        // DELETE: api/Recorridos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteRecorrido(int id)
        {
            // Buscar el recorrido
            var recorrido = await _context.Recorridos.FindAsync(id);

            if (recorrido == null)
            {
                return NotFound(new { mensaje = $"El recorrido con ID {id} no existe." });
            }

            // Eliminar el recorrido
            _context.Recorridos.Remove(recorrido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}