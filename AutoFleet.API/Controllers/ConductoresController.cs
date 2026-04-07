using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoFleet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        private readonly DataContext _context;

        public ConductoresController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Conductores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductor>>> GetConductores()
        {
            var conductores = await _context.Conductores.ToListAsync();
            return Ok(conductores);
        }

        // GET: api/Conductores/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Conductor>> GetConductor(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null)
            {
                return NotFound(new { mensaje = $"El conductor con ID {id} no existe." });
            }

            return Ok(conductor);
        }

        // POST: api/Conductores
        [HttpPost]
        public async Task<ActionResult<Conductor>> PostConductor(Conductor conductor)
        {
            // Verificar si ya existe un conductor con ese ID
            var existe = await _context.Conductores.AnyAsync(c => c.Id == conductor.Id);
            if (existe)
            {
                return Conflict(new { mensaje = $"El conductor con ID {conductor.Id} ya existe." });
            }

            // Verificar si ya existe un conductor con el mismo documento de identidad
            var existeDocumento = await _context.Conductores.AnyAsync(c => c.Documento == conductor.Documento);
            if (existeDocumento)
            {
                return Conflict(new { mensaje = $"Ya existe un conductor con el documento {conductor.Documento}." });
            }

            // Verificar si ya existe un conductor con la misma licencia
            var existeLicencia = await _context.Conductores.AnyAsync(c => c.LicenciaNumero == conductor.LicenciaNumero);
            if (existeLicencia)
            {
                return Conflict(new { mensaje = $"Ya existe un conductor con la licencia {conductor.LicenciaNumero}." });
            }

            _context.Conductores.Add(conductor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetConductor), new { id = conductor.Id }, conductor);
        }

        // PUT: api/Conductores/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutConductor(int id, Conductor conductor)
        {
            // Verificar que el ID de la URL coincida con el ID del objeto
            if (id != conductor.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del conductor." });
            }

            // Verificar si el conductor existe
            var existe = await _context.Conductores.AnyAsync(c => c.Id == id);
            if (!existe)
            {
                return NotFound(new { mensaje = $"El conductor con ID {id} no existe." });
            }

            // Verificar si otro conductor tiene el mismo documento (excluyendo el actual)
            var existeDocumento = await _context.Conductores.AnyAsync(c => c.Documento == conductor.Documento && c.Id != id);
            if (existeDocumento)
            {
                return Conflict(new { mensaje = $"Ya existe otro conductor con el documento {conductor.Documento}." });
            }

            // Verificar si otro conductor tiene la misma licencia (excluyendo el actual)
            var existeLicencia = await _context.Conductores.AnyAsync(c => c.LicenciaNumero == conductor.LicenciaNumero && c.Id != id);
            if (existeLicencia)
            {
                return Conflict(new { mensaje = $"Ya existe otro conductor con la licencia {conductor.LicenciaNumero}." });
            }

            // Marcar la entidad como modificada
            _context.Entry(conductor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Conductores.AnyAsync(c => c.Id == id))
                {
                    return NotFound(new { mensaje = $"El conductor con ID {id} ya no existe." });
                }
                throw;
            }

            return Ok(conductor);
        }

        // DELETE: api/Conductores/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteConductor(int id)
        {
            // Buscar el conductor
            var conductor = await _context.Conductores.FindAsync(id);

            if (conductor == null)
            {
                return NotFound(new { mensaje = $"El conductor con ID {id} no existe." });
            }

            // Verificar si el conductor tiene recorridos asociados
            var tieneRecorridos = await _context.Recorridos.AnyAsync(r => r.ConductorId == id);
            if (tieneRecorridos)
            {
                return Conflict(new { mensaje = $"No se puede eliminar el conductor con ID {id} porque tiene recorridos asociados." });
            }

            // Eliminar el conductor
            _context.Conductores.Remove(conductor);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - Eliminación exitosa sin contenido
        }
    }
}