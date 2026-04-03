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

        [HttpGet]
        public async Task<ActionResult> GetRutas()
        {
            var rutas = await _context.Rutas.ToListAsync();
            return Ok(rutas);
        }

        [HttpGet("{CodeRuta:int}")]
        public async Task<ActionResult<Ruta>> GetRuta(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta == null) return NotFound();
            return Ok(ruta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Ruta ruta)
        {
            _context.Rutas.Add(ruta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRuta), new { id = ruta.Id }, ruta);
        }

    }
}
