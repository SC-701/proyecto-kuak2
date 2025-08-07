using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface ICuentaController
    {
        Task<IActionResult> ObtenerTodasLasCuentas();
        Task<IActionResult> ObtenerCuentaPorId(Guid IdCuenta);
        Task<IActionResult> CrearCuenta(CuentaRequest cuenta);
        Task<IActionResult> EditarCuenta(Guid IdCuenta, CuentaRequest cuenta);
        Task<IActionResult> EliminarCuenta(Guid IdCuenta);
    }
}
