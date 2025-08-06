using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IUsuarioFlujo
    {
        Task<IEnumerable<UsuarioResponse>> ObtenerTodosLosUsuarios();
        Task<UsuarioResponse> ObtenerUsuarioPorId(Guid IdUsuario);
        Task<Guid> CrearUsuario(UsuarioRequest usuario);
        Task<Guid> EditarUsuario(Guid IdUsuario, UsuarioRequest usuario);
        Task<Guid> EliminarUsuario(Guid IdUsuario);
    }
}
