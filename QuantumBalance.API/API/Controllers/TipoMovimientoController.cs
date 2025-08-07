using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using DA;
using Flujo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TipoMovimientoController : ControllerBase, ITipoMovimientoController
    {
        private readonly ITipoMovimientoFlujo _tipoMovimientoFlujo;


        public TipoMovimientoController(ITipoMovimientoFlujo tipoMovimientoFlujo)
        {
            _tipoMovimientoFlujo = tipoMovimientoFlujo;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposMovimientos() 
        {
            var res = await _tipoMovimientoFlujo.ObtenerTiposMovimiento(); 
            if (!res.Any())
                return NoContent();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTipoMovimientoPorId([FromRoute] Guid idTipoMovimiento)
        {
            var res = await _tipoMovimientoFlujo.ObtenerTipoMovimientoPorId(idTipoMovimiento);
            if (res == null)
                return NotFound("Tipo de movimiento no encontrado.");
            return Ok(res);
        }




    }
}
