using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class UsuarioFlujo
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }
        public async Task<Guid> CrearUsuario(Usuario usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }
        public async Task<Guid> EditarUsuario(Guid idUsuario, Usuario usuario)
        {
            return await _usuarioDA.EditarUsuario(idUsuario, usuario);
        }
        public async Task<Usuario> ObtenerUsuarioPorId(Guid idUsuario)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(idUsuario);
        }
        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _usuarioDA.ObtenerUsuarios();
        }

    }
}
