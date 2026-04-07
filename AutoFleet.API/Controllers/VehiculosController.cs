using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoFleet.API.Data;
using AutoFleet.Shared.Entities;

namespace AutoFleet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class VehiculosController : ControllerBase
    {
        private readonly DataContext _context; //Llamado a la base de datos solo lectura 
        public VehiculosController(DataContext context)
        {
            _context = context;
        }
        // Read: Listar todos los vehículos 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        {
            return await _context.Vehiculos.ToListAsync();
        }

        // Read: Obtener un solo vehículo por su ID 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);

            if (vehiculo == null)
            {
                return NotFound("El vehículo no fue encontrado.");
            }

            return vehiculo;
        }

        //Guardar un nuevo vehículo
        [HttpPost]
        public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //error: por la restricción de placa
                return BadRequest("Ya existe un vehículo con esa misma placa.");
            }

            return CreatedAtAction(nameof(GetVehiculo), new { id = vehiculo.Id }, vehiculo); // Modificado con Exito, devuelve el nuevo vehículo creado con su ID asignado.
        }

        //Modificar un vehículo existente
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest("El ID no coincide con el vehículo.");
            }

            _context.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) //en caso de concurrencia, verifica si el vehículo existe o no
            {
                if (!VehiculoExists(id)) // Llamando Metodo auxiliar para verificar si el vehículo existe
                {
                    return NotFound("El vehículo no existe.");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // Borrar un vehículo
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound("El vehículo no existe.");
            }
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Metodo auxiliar para saber si el vehículo existe
        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.Id == id);
        }
    }
}