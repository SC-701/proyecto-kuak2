using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CuentaController : ControllerBase, ICuentaController
    {
        private readonly ICuentaFlujo _cuentaFlujo;
        private readonly ILogger<CuentaController> _logger;

        public CuentaController(ICuentaFlujo cuentaFlujo, ILogger<CuentaController> logger)
        {
            _cuentaFlujo = cuentaFlujo;
            _logger = logger;
        }

    [HttpPost]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaRequest cuenta)
        {
            Guid resultado = await _cuentaFlujo.CrearCuenta(cuenta);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear la cuenta. Entrada: {@Cuenta}", cuenta);
                return BadRequest("No se pudo crear la cuenta.");
            }

            return CreatedAtAction(nameof(ObtenerCuentaPorId), new { IdCuenta = resultado }, null);
        }

    [HttpPut("{IdCuenta}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> EditarCuenta([FromRoute] Guid IdCuenta, [FromBody] CuentaRequest cuenta)
        {
            if (IdCuenta == Guid.Empty || cuenta == null)
            {
                _logger.LogError("ID de cuenta inválido o cuenta nula.");
                return BadRequest("ID de cuenta inválido o cuenta nula.");
            }
            Guid resultado = await _cuentaFlujo.EditarCuenta(IdCuenta, cuenta);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al editar la cuenta.");
                return BadRequest("Error al editar la cuenta.");
            }
            return NoContent();
        }

    [HttpGet("{IdCuenta}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerCuentaPorId([FromRoute] Guid IdCuenta)
        {
            CuentaResponse cuenta = await _cuentaFlujo.ObtenerCuentaPorId(IdCuenta);

            if (cuenta == null)
            {
                _logger.LogError($"Cuenta con ID {IdCuenta} no encontrada.");
                return NotFound("Cuenta no encontrada.");
            }

            return Ok(cuenta);
        }

    [HttpDelete("{IdCuenta}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> EliminarCuenta([FromRoute] Guid IdCuenta)
        {
            try { 

            bool success = await _cuentaFlujo.EliminarCuenta(IdCuenta);

            if (!success)
            {
                _logger.LogError("Error al eliminar la cuenta con ID {IdCuenta}.", IdCuenta);
                return BadRequest("No se pudo eliminar cuenta.");
            }

            return NoContent();
        }
            catch (SqlException ex) when (ex.Number == 547)
            {
                _logger.LogWarning("No se pudo eliminar la cuenta {IdCuenta} porque está registrada en un movimiento.", IdCuenta);
                return BadRequest("No se puede eliminar esta cuenta porque existe un movimiento registrado en ella. Elimine primero los movimientos relacionados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar la cuenta {IdCuenta}.", IdCuenta);
                return StatusCode(500, "Ocurrió un error inesperado al eliminar la cuenta.");
            }
        }

        [HttpGet()]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerTodasLasCuentas()
        {
            IEnumerable<CuentaResponse> cuentas = await _cuentaFlujo.ObtenerTodasLasCuentas();

            if (cuentas == null || !cuentas.Any())
            {
                _logger.LogError("No se encontraron cuentas.");
                return NotFound("No se encontraron cuentas.");
            }

            return Ok(cuentas);
        }
    }
}
