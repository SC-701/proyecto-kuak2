﻿using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaRequest cuenta)
        {
            Guid resultado = await _cuentaFlujo.CrearCuenta(cuenta);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear la cuenta. Entrada: {@Cuenta}", cuenta);
                return BadRequest("No se pudo crear la cuenta.");
            }

            return CreatedAtAction(nameof(ObtenerCuentaPorId),
                new { id = resultado },
                null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarCuenta([FromQuery] Guid id, [FromBody] CuentaRequest cuenta)
        {
            if (id == Guid.Empty || cuenta == null)
            {
                _logger.LogError("ID de cuenta inválido o cuenta nula.");
                return BadRequest("ID de cuenta inválido o cuenta nula.");
            }
            Guid resultado = await _cuentaFlujo.EditarCuenta(id, cuenta);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al editar la cuenta.");
                return BadRequest("Error al editar la cuenta.");
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCuentaPorId([FromRoute] Guid id)
        {
            CuentaResponse cuenta = await _cuentaFlujo.ObtenerCuentaPorId(id);

            if (cuenta == null)
            {
                _logger.LogError($"Cuenta con ID {id} no encontrada.");
                return NotFound("Cuenta no encontrada.");
            }

            return Ok(cuenta);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarCuenta([FromQuery] Guid id)
        {
            Guid resultado = await _cuentaFlujo.EliminarCuenta(id);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar cuenta.");
                return BadRequest("No se pudo eliminar cuenta.");
            }

            return Ok(resultado);
        }

        [HttpGet()]
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
