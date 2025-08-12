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
    public class UsuarioController : ControllerBase, IUsuarioController
    {
        private readonly IUsuarioFlujo _usuarioFlujo;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioFlujo usuarioFlujo, ILogger<UsuarioController> logger)
        {
            _usuarioFlujo = usuarioFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest usuario)
        {
            Guid resultado = await _usuarioFlujo.CrearUsuario(usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear el usuario. Entrada: {@Usuario}", usuario);
                return BadRequest("No se pudo crear el usuario.");
            }
            return CreatedAtAction(nameof(ObtenerUsuarioPorId),
                new { IdUsuario = resultado },
                null);
        }

        [HttpPut("{IdUsuario}")]
        public async Task<IActionResult> EditarUsuario([FromRoute] Guid IdUsuario, [FromBody] UsuarioRequest usuario)
        {
            if (IdUsuario == Guid.Empty || usuario == null)
            {
                _logger.LogError("ID de usuario inválido o usuario nulo.");
                return BadRequest("ID de usuario inválido o usuario nulo.");
            }
            usuario.IdUsuario = IdUsuario;
            Guid resultado = await _usuarioFlujo.EditarUsuario(usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al actualizar el usuario.");
                return BadRequest("Error al actualizar el usuario.");
            }
            return Ok(resultado);
        }

        [HttpDelete("{IdUsuario}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] Guid IdUsuario)
        {
            Guid resultado = await _usuarioFlujo.EliminarUsuario(IdUsuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al eliminar el usuario.");
                return BadRequest("No se pudo eliminar el usuario.");
            }
            return Ok(resultado);
        }

        [HttpGet("{IdUsuario}")]
        public async Task<IActionResult> ObtenerUsuarioPorId([FromRoute] Guid IdUsuario)
        {
            if (IdUsuario == Guid.Empty)
            {
                _logger.LogError("ID de usuario inválido.");
                return BadRequest("ID de usuario inválido.");
            }
            UsuarioResponse usuario = await _usuarioFlujo.ObtenerUsuarioPorId(IdUsuario);
            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado. ID: {IdUsuario}", IdUsuario);
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet()]
        public async Task<IActionResult> ObtenerTodosLosUsuarios()
        {
            IEnumerable<UsuarioResponse> usuarios = await _usuarioFlujo.ObtenerTodosLosUsuarios();

            if (usuarios == null || !usuarios.Any())
            {
                _logger.LogWarning("No se encontraron usuarios.");
                return NotFound("No se encontraron usuarios.");
            }
            return Ok(usuarios);
        }
    }
}
