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

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Mantenimientos.ToListAsync());
        }

        // SCRUM-86: Implementar GET(id)
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento == null) return NotFound();
            return Ok(mantenimiento);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Mantenimiento mantenimiento)
        {
            _context.Add(mantenimiento);
            await _context.SaveChangesAsync();
            return Ok(mantenimiento);
        }

        // SCRUM-86: Implementar PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Id) return BadRequest();
            _context.Update(mantenimiento);
            await _context.SaveChangesAsync();
            return Ok(mantenimiento);
        }

        // SCRUM-86: Implementar DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento == null) return NotFound();

            _context.Remove(mantenimiento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}