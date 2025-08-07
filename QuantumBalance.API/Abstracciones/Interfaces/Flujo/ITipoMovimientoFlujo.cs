using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ITipoMovimientoFlujo
    {
        Task<IEnumerable<TipoMovimientoResponse>> ObtenerTiposMovimiento(); 
        Task<TipoMovimientoResponse> ObtenerTipoMovimientoPorId(Guid idTipoMovimiento);
    }

}
