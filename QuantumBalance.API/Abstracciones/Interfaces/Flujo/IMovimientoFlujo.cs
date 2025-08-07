using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IMovimientoFlujo
    {
        Task<IEnumerable<MovimientoResponse>> ObtenerTodosLosMovimientos();
        Task<MovimientoResponse> ObtenerMovimientoPorId(Guid idMovimiento);
        Task<Guid> CrearMovimiento(MovimientoRequest movimiento);
        Task<Guid> EditarMovimiento(Guid idMovimiento, MovimientoRequest movimiento);
        Task<Guid> EliminarMovimiento(Guid idMovimiento);
    }
}
