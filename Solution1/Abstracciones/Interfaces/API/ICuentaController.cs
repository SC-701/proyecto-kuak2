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
        Task<IActionResult> CrearCuenta(Cuenta cuenta);
        Task<IActionResult> ActualizarCuenta(Guid idCuenta, Cuenta cuenta);
        Task<IActionResult> EliminarCuenta(Guid idCuenta);
        Task<IActionResult> ObtenerCuentaPorId(Guid idCuenta);
        Task<IActionResult> ObtenerCuentas();
        Task<IActionResult> DetalleCuenta(Guid idCuenta);


    }
}
