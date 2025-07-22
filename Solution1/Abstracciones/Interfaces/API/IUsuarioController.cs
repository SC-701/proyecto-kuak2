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
        Task<IActionResult> CrearUsuario(Usuario usuario);
        Task<IActionResult> EditarUsuario(Guid IdUsuario, Usuario usuario);
        Task<IActionResult> ObtenerUSuarioPorId(Guid idUsuario);
        Task<IActionResult> ObtenerUsuarios();
    }
}
