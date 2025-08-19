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
    public class CategoriaController : Controller, ICategoriaController
    {
        private readonly ICategoriaFlujo _categoriaFlujo;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ICategoriaFlujo categoriaFlujo, ILogger<CategoriaController> logger)
        {
            _categoriaFlujo = categoriaFlujo;
            _logger = logger;
        }

    [HttpPost]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> CrearCategoria([FromBody] CategoriaRequest categoria)
        {
            Guid nuevaCuenta = await _categoriaFlujo.CrearCategoria(categoria);

            if (nuevaCuenta == Guid.Empty)
            {
                _logger.LogError("No se pudo crear la categoría. Entrada: {@Categoria}", categoria);
                return BadRequest("No se pudo crear la categoría.");
            }

            return CreatedAtAction(nameof(ObtenerCategoriaPorId), new { IdCategoria = nuevaCuenta }, null);
        }

    [HttpPut]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> EditarCategoria([FromQuery] Guid IdCategoria, [FromBody] CategoriaRequest categoria)
        {
            if (IdCategoria == Guid.Empty || categoria == null)
            {
                _logger.LogError("ID de categoría inválido o categoría nula.");
                return BadRequest("ID de categoría inválido o categoría nula.");
            }

            Guid resultado = await _categoriaFlujo.EditarCategoria(IdCategoria, categoria);

            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al editar la categoría.");
                return BadRequest("Error al editar la categoría.");
            }

            return Ok(resultado);
        }

        [HttpDelete]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> EliminarCategoria([FromQuery] Guid IdCategoria)
        {
            try
            {
                bool success = await _categoriaFlujo.EliminarCategoria(IdCategoria);

                if (!success)
                {
                    _logger.LogError("Error al eliminar la categoría con ID {IdCategoria}.", IdCategoria);
                    return BadRequest("Error al eliminar la categoría.");
                }

                return NoContent();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                _logger.LogWarning("No se pudo eliminar la categoría {IdCategoria} porque está en uso en un movimiento.", IdCategoria);
                return BadRequest("No se puede eliminar esta categoría porque está siendo utilizada en un movimiento. Elimine primero los movimientos relacionados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar la categoría {IdCategoria}.", IdCategoria);
                return StatusCode(500, "Ocurrió un error inesperado al eliminar la categoría.");
            }
        }

        [HttpGet("{IdCategoria}")]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerCategoriaPorId([FromRoute] Guid IdCategoria)
        {
            CategoriaResponse categoria = await _categoriaFlujo.ObtenerCategoriaPorId(IdCategoria);

            if (categoria == null)
            {
                _logger.LogError($"No se encontraron categorías para el ID {IdCategoria}.");
                return NotFound("Categoría no encontrada.");
            }

            return Ok(categoria);
        }

    [HttpGet]
    [Authorize(Roles = "1")]
        public async Task<IActionResult> ObtenerTodasLasCategorias()
        {
            IEnumerable<CategoriaResponse> categorias = await _categoriaFlujo.ObtenerTodasLasCategorias();

            if (categorias == null || !categorias.Any())
            {
                _logger.LogError("No se encontraron categorías.");
                return NotFound("No se encontraron categorías.");
            }

            return Ok(categorias);
        }
    }
}
