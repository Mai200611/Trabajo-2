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

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Conductores.ToListAsync());
        }

        // SCRUM-74: Implementar GET(id)
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null) return NotFound();
            return Ok(conductor);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Conductor conductor)
        {
            _context.Add(conductor);
            await _context.SaveChangesAsync();
            return Ok(conductor);
        }

        // SCRUM-74: Implementar PUT
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Conductor conductor)
        {
            if (id != conductor.Id) return BadRequest();
            _context.Update(conductor);
            await _context.SaveChangesAsync();
            return Ok(conductor);
        }

        // SCRUM-74: Implementar DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var conductor = await _context.Conductores.FindAsync(id);
            if (conductor == null) return NotFound();

            _context.Remove(conductor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}