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
        public async Task<IActionResult> EliminarCategoria([FromQuery] Guid IdCategoria)
        {
            bool success = await _categoriaFlujo.EliminarCategoria(IdCategoria);

            if (!success)
            {
                _logger.LogError("Error al eliminar la categoría con ID {IdCategoria}.", IdCategoria);
                return BadRequest("Error al eliminar la categoría.");
            }

            return NoContent();
        }

        [HttpGet("{IdCategoria}")]
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
