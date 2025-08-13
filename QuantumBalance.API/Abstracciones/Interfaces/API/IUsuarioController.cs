using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> ObtenerTodosLosUsuarios();
        Task<IActionResult> ObtenerUsuarioPorId(Guid IdUsuario);
        Task<IActionResult> CrearUsuario(UsuarioRequest usuario);
        Task<IActionResult> EditarUsuario(Guid IdUsuario, UsuarioRequest usuario);
        Task<IActionResult> EliminarUsuario(Guid IdUsuario);
    }
}
