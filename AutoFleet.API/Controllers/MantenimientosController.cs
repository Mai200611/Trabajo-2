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

        [HttpPost]
        public async Task<ActionResult> Post(Mantenimiento mantenimiento)
        {
            _context.Add(mantenimiento);
            await _context.SaveChangesAsync();
            return Ok(mantenimiento);
        }
    }
}