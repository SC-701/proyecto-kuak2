using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase, ICuentaController
    {
        private ICuentaFlujo _cuentaFlujo;
        private ILogger<CuentaController> _logger;

        public CuentaController(ICuentaFlujo cuentaFlujo, ILogger<CuentaController> logger)
        {
            _cuentaFlujo = cuentaFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuenta([FromBody] Cuenta cuenta)
        {
            Guid resultado = await _cuentaFlujo.CrearCuenta(cuenta);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear la cuenta. Entrada: {@Cuenta}", cuenta);
                return BadRequest("No se pudo crear la cuenta.");
            }

            return CreatedAtAction(nameof(ObtenerCuentaPorId),
                new { idCuenta = resultado },
                null);
        }



        [HttpPut]
        public async Task<IActionResult> ActualizarCuenta([FromQuery] Guid idCuenta, [FromBody] Cuenta cuenta)
        {
            if (idCuenta == Guid.Empty || cuenta == null)
            {
                _logger.LogError("ID de cuenta inválido o cuenta nula.");
                return BadRequest("ID de cuenta inválido o cuenta nula.");
            }
            Guid resultado = await _cuentaFlujo.ActualizarCuenta(idCuenta, cuenta);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al actualizar la cuenta.");
                return BadRequest("Error al actualizar la cuenta.");
            }
            return NoContent();
        }


        [HttpGet("{idCuenta}")]
        public async Task<IActionResult> ObtenerCuentaPorId([FromRoute] Guid idCuenta)
        {
            Cuenta cuenta = await _cuentaFlujo.ObtenerCuentaPorId(idCuenta);

            if (cuenta == null)
            {
                _logger.LogError($"Cuenta con ID {idCuenta} no encontrada.");
                return NotFound("Cuenta no encontrada.");
            }

            return Ok(cuenta);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCuenta([FromQuery] Guid idCuenta)
        {
            Guid resultado = await _cuentaFlujo.EliminarCuenta(idCuenta);

            if (resultado == null)
            {
                _logger.LogError("Error al eliminar cuenta.");
                return BadRequest("No se pudo eliminar cuenta.");
            }

            return Ok(resultado);
        }

        [HttpGet("cuentas")]
        public async Task<IActionResult> ObtenerCuentas()
        {
            IEnumerable<Cuenta> cuentas = await _cuentaFlujo.ObtenerCuentas(); 

            if (cuentas == null || !cuentas.Any())
            {
                _logger.LogError($"No se encontraron cuentas.");
                return NotFound("No se encontraron cuentas.");
            }

            return Ok(cuentas);
        }

        [HttpGet("detalle/{idCuenta}")]
        public async Task<IActionResult> DetalleCuenta([FromRoute] Guid idCuenta)
        {
            var resultado = await _cuentaFlujo.DetalleCuenta(idCuenta);
            if (resultado == null)
            {
                return NotFound("cuenta no existe");
            }
            return Ok(resultado);
        }
    }
}
