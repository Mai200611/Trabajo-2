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

        [HttpPost]
        public async Task<ActionResult> Post(Conductor conductor)
        {
            _context.Add(conductor);
            await _context.SaveChangesAsync();
            return Ok(conductor);
        }
    }
}