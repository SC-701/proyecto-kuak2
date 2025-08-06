using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICuentaCategoriaController
    {
        Task<IActionResult> ObtenerTodasLasCuentasCategorias();
        Task<IActionResult> ObtenerCuentaCategoriaPorId(Guid IdCategoria);
        Task<IActionResult> CrearCuentaCategoria(CuentaCategoriaRequest cuentaCategoria);
        Task<IActionResult> EditarCuentaCategoria(Guid IdCategoria, CuentaCategoriaRequest cuentaCategoria);
        Task<IActionResult> EliminarCuentaCategoria(Guid IdCategoria);
    }
}
