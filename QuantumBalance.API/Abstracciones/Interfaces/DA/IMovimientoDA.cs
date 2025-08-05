using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface IMovimientoDA
    {
        Task<IEnumerable<MovimientoResponse>> MostrarMovimientos();
        Task<Guid> CrearMovimiento(MovimientoRequest movimiento);
        Task EditarMovimiento(MovimientoRequest movimiento);
        Task EliminarMovimiento(Guid idMovimiento);
    }
}
