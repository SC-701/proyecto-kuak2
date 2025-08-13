using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private readonly IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioRequest usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Guid> EditarUsuario(UsuarioRequest usuario)
        {
            return await _usuarioDA.EditarUsuario(usuario);
        }

        public async Task<Guid> EliminarUsuario(Guid IdUsuario)
        {
            return await _usuarioDA.EliminarUsuario(IdUsuario);
        }

        public async Task<UsuarioResponse> ObtenerUsuarioPorId(Guid IdUsuario)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(IdUsuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios()
        {
            return await _usuarioDA.ObtenerTodosLosUsuarios();
        }
    }
}
