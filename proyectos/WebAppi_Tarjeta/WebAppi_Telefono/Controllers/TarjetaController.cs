using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppi_Telefono.Models;

namespace WebAppi_Telefono.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly BdCineContext _context;

        public TarjetaController(BdCineContext context)
        {
            _context = context;
        }

        // GET: api/Tarjeta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TcTarjetum>>> GetTarjetas()
        {
            var tarjetas = await _context.TcTarjeta.ToListAsync();
            return Ok(tarjetas);
        }

        // POST: api/Tarjeta/CreditValidation
        [HttpPost("CreditValidation")]
        public async Task<IActionResult> CreditValidation([FromBody] TcTarjetum tarjeta)
        {
            // Validar tarjeta en la base de datos
            var existingTarjeta = await _context.TcTarjeta
                .FirstOrDefaultAsync(t =>
                    t.Nombre == tarjeta.Nombre &&
                    t.Numero == tarjeta.Numero &&
                    t.MesEx == tarjeta.MesEx &&
                    t.AñoEx == tarjeta.AñoEx &&
                    t.Cv == tarjeta.Cv);

            if (existingTarjeta == null)
            {
                // Si no existe, retornar respuesta No Valid y el token original
                return Ok(new { Result = "No Valid"});
            }
            else
            {
                // Si existe, retornar la respuesta sin incluir el token original
                return Ok(new { Result = "Yes" });
            }
        }
    }
}
