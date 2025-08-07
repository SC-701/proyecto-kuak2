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
        public async Task<IActionResult> ObtenerTodasLasCuentasCategorias()
        {
            var resultado = await _cuentaCategoriaFlujo.ObtenerTodas();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCuentaCategoriaPorId(Guid id)
        {
            var resultado = await _cuentaCategoriaFlujo.ObtenerPorId(id);
            if (resultado == null)
                return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuentaCategoria([FromBody] CuentaCategoriaRequest cuentaCategoria)
        {
            var id = await _cuentaCategoriaFlujo.Crear(cuentaCategoria);
            return CreatedAtAction(nameof(ObtenerCuentaCategoriaPorId), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCuentaCategoria(Guid id, [FromBody] CuentaCategoriaRequest cuentaCategoria)
        {
            var actualizado = await _cuentaCategoriaFlujo.Editar(id, cuentaCategoria);
            if (actualizado == Guid.Empty)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCuentaCategoria(Guid id)
        {
            var eliminado = await _cuentaCategoriaFlujo.Eliminar(id);
            if (eliminado == Guid.Empty)
                return NotFound();
            return NoContent();
        }
    }
}
