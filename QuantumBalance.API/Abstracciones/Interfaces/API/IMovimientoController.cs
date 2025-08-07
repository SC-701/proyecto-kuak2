using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.API
{
    public interface IMovimientoController
    {
        Task<IActionResult> ObtenerTodosLosMovimientos();
        Task<IActionResult> ObtenerMovimientoPorId(Guid IdMovimiento);
        Task<IActionResult> CrearMovimiento(MovimientoRequest movimiento);
        Task<IActionResult> EditarMovimiento(Guid IdMovimiento, MovimientoRequest movimiento);
        Task<IActionResult> EliminarMovimiento(Guid IdMovimiento);
    }
}
