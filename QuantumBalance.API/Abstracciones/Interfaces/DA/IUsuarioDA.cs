using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
   
    public interface IUsuarioDA
    {
        Task<IEnumerable<UsuarioResponse>> MostrarUsuarios();
        Task<Guid> CrearUsuario(UsuarioRequest usuario);
        Task EditarUsuario(UsuarioRequest usuario);
        Task EliminarUsuario(Guid idUsuario);
    }
}
