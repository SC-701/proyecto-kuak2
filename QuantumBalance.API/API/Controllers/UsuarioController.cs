using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller, IUsuarioController
    {
        private IUsuarioFlujo _usuarioFlujo;
        private ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioFlujo usuarioFlujo, ILogger<UsuarioController> logger)
        {
            _usuarioFlujo = usuarioFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            Guid resultado = await _usuarioFlujo.CrearUsuario(usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("No se pudo crear el usuario. Entrada: {@Usuario}", usuario);
                return BadRequest("No se pudo crear el usuario.");
            }
            return CreatedAtAction(nameof(ObtenerUsuarioPorId),
                new { idUsuario = resultado },
                null);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario([FromQuery] Guid idUsuario, [FromBody] Usuario usuario)
        {
            if (idUsuario == Guid.Empty || usuario == null)
            {
                _logger.LogError("ID de usuario inválido o usuario nulo.");
                return BadRequest("ID de usuario inválido o usuario nulo.");
            }
            Guid resultado = await _usuarioFlujo.EditarUsuario(idUsuario, usuario);
            if (resultado == Guid.Empty)
            {
                _logger.LogError("Error al actualizar el usuario.");
                return BadRequest("Error al actualizar el usuario.");
            }
            return Ok(resultado);
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerUsuarioPorId([FromRoute] Guid idUsuario)
        {
            if (idUsuario == Guid.Empty)
            {
                _logger.LogError("ID de usuario inválido.");
                return BadRequest("ID de usuario inválido.");
            }
            Usuario usuario = await _usuarioFlujo.ObtenerUsuarioPorId(idUsuario);
            if (usuario == null)
            {
                _logger.LogWarning("Usuario no encontrado. ID: {IdUsuario}", idUsuario);
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            IEnumerable<Usuario> usuarios = await _usuarioFlujo.ObtenerUsuarios();
            if (usuarios == null || !usuarios.Any())
            {
                _logger.LogWarning("No se encontraron usuarios.");
                return NotFound("No se encontraron usuarios.");
            }
            return Ok(usuarios);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
