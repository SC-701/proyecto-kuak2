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
    public class CuentaCategoriaController : ControllerBase, ICuentaCategoriaController
    {
        private readonly ICuentaCategoriaFlujo _cuentaCategoriaFlujo;

        public CuentaCategoriaController(ICuentaCategoriaFlujo cuentaCategoriaFlujo)
        {
            _cuentaCategoriaFlujo = cuentaCategoriaFlujo;
        }

    [HttpGet]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerTodasLasCuentasCategorias()
        {
            var resultado = await _cuentaCategoriaFlujo.ObtenerTodas();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

    [HttpGet("{idCuentaCategoria}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerCuentaCategoriaPorId(Guid idCuentaCategoria)
        {
            var resultado = await _cuentaCategoriaFlujo.ObtenerPorId(idCuentaCategoria);
            if (resultado == null)
                return NotFound();
            return Ok(resultado);
        }

    [HttpPost]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> CrearCuentaCategoria([FromBody] CuentaCategoriaRequest cuentaCategoria)
        {
            var idCuentaCategoria = await _cuentaCategoriaFlujo.Crear(cuentaCategoria);
            return CreatedAtAction(nameof(ObtenerCuentaCategoriaPorId), new { idCuentaCategoria }, null);
        }

    [HttpPut("{idCuentaCategoria}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> EditarCuentaCategoria(Guid idCuentaCategoria, [FromBody] CuentaCategoriaRequest cuentaCategoria)
        {
            var actualizado = await _cuentaCategoriaFlujo.Editar(idCuentaCategoria, cuentaCategoria);
            if (actualizado == Guid.Empty)
                return NotFound();
            return Ok();
        }

    [HttpDelete("{idCuentaCategoria}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> EliminarCuentaCategoria(Guid idCuentaCategoria)
        {
            var eliminado = await _cuentaCategoriaFlujo.Eliminar(idCuentaCategoria);
            if (eliminado == Guid.Empty)
                return NotFound();
            return NoContent();
        }
    }
}
