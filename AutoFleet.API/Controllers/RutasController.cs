using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;

namespace AutoFleet.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly DataContext _context;

        public RutasController(DataContext context)
        {
            _context = context;
        }

        //GET retorna TODOS los registros en la tabla Rutas
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var rutas = await _context.Rutas.ToListAsync();
            return Ok(rutas);
        }

        //GET que retorna un registro en la tabla Rutas por id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ruta>> Get(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta == null) return NotFound(new { mensaje = $"La ruta con ID {id} no existe." });
            return Ok(ruta);
        }

        //POST que permite realizar un registro en la tabla Rutas en la bd
        [HttpPost]
        public async Task<ActionResult> Post(Ruta ruta)
        { 
            
            //Comprobar si ya existe el Id
            var existe = await _context.Rutas.AnyAsync(r => r.Id == ruta.Id);
            if (existe)
            {
                return Conflict(new { mensaje = $"La ruta con ID {ruta.Id} ya existe." });
            }

            _context.Rutas.Add(ruta);
            await _context.SaveChangesAsync();
            return Ok(ruta);
        }

        // PUT que permite actualizar un registro de Ruta por id y el cuerpo modificado
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutRuta(int id, Ruta ruta)
        {
            // Verificar que el registro exista
            if (id != ruta.Id)
            {
                return BadRequest(new { mensaje = "El ID a buscar no coincide con el ID del cuerpo JSON" });
            }

            // Verificar si la ruta existe
            var existe = await _context.Rutas.AnyAsync(r => r.Id == id);
            if (!existe)
            {
                return NotFound(new { mensaje = $"La ruta con ID {id} no existe." });
            }

            // Marcar la entidad como modificada
            _context.Entry(ruta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verificar nuevamente si existe (por si el error es por repeticion, y por si algo)
                if (!await _context.Rutas.AnyAsync(r => r.Id == id))
                {
                    return NotFound(new { mensaje = $"La ruta con ID {id} ya no existe." });
                }
                throw;
            }

            return Ok(ruta);
        }

        // DELETE que permite eliminar un registro de la tabla Rutas
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteRuta(int id)
        {
            // Buscar la ruta
            var ruta = await _context.Rutas.FindAsync(id);

            if (ruta == null)
            {
                return NotFound(new { mensaje = $"La ruta con ID {id} no existe." });
            }

            // Verificar si la ruta tiene recorridos asociados
            var tieneRecorridos = await _context.Recorridos.AnyAsync(r => r.RutaId == id);
            if (tieneRecorridos)
            {
                return Conflict(new { mensaje = $"No se puede eliminar la ruta con ID {id} porque tiene recorridos asociados." });
            }

            // Eliminar la ruta
            _context.Rutas.Remove(ruta);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 - Eliminación exitosa sin contenido
        }

    }
}
