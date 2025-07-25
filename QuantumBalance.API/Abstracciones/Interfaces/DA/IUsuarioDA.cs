using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IUsuarioDA
    {
        Task<Guid> CrearUsuario(Usuario usuario);
        Task<Guid> EditarUsuario(Guid idUsuario, Usuario usuario);
        Task<Usuario> ObtenerUsuarioPorId(Guid idUsuario);
        Task<IEnumerable<Usuario>> ObtenerUsuarios();
    }
}
