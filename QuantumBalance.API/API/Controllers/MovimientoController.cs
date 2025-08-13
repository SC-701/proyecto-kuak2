using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MovimientoController : ControllerBase, IMovimientoController
    {
        private IMovimientoFlujo _movimientoFlujo;
        private ILogger<MovimientoController> _logger;

        public MovimientoController(IMovimientoFlujo movimientoFlujo, ILogger<MovimientoController> logger)
        {
            _movimientoFlujo = movimientoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoRequest movimiento)
        {
            try
            {
                var res = await _movimientoFlujo.CrearMovimiento(movimiento);
                return CreatedAtAction(nameof(ObtenerMovimientoPorId), new { IdMovimiento = res }, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el movimiento.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosMovimientos()
        {
            try
            {
                var res = await _movimientoFlujo.ObtenerTodosLosMovimientos();
                if (!res.Any())
                    return NoContent();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los movimientos.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpGet("{IdMovimiento}")]
        public async Task<IActionResult> ObtenerMovimientoPorId([FromRoute] Guid IdMovimiento)
        {
            try
            {
                var res = await _movimientoFlujo.ObtenerMovimientoPorId(IdMovimiento);
                if (res == null)
                    return NotFound("Movimiento no encontrado.");
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el movimiento por ID.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut("{IdMovimiento}")]
        public async Task<IActionResult> EditarMovimiento([FromRoute] Guid IdMovimiento, [FromBody] MovimientoRequest movimiento)
        {
            try
            {
                var res = await _movimientoFlujo.EditarMovimiento(IdMovimiento, movimiento);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el movimiento.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpDelete("{IdMovimiento}")]
        public async Task<IActionResult> EliminarMovimiento([FromRoute] Guid IdMovimiento)
        {
            try
            {
                var res = await _movimientoFlujo.EliminarMovimiento(IdMovimiento);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el movimiento.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }




    }
}
