using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class TipoMovimientoFlujo : ITipoMovimientoFlujo
    {
        private readonly ITipoMovimientoDA _tipoMovimientoDA;

        public TipoMovimientoFlujo(ITipoMovimientoDA tipoMovimientoDA)
        {
            _tipoMovimientoDA = tipoMovimientoDA;
        }

        public async Task<IEnumerable<TipoMovimientoResponse>> ObtenerTiposMovimiento()
        {
            return await _tipoMovimientoDA.ObtenerTiposMovimientos();
        }

        public async Task<TipoMovimientoResponse> ObtenerTipoMovimientoPorId(Guid idTipoMovimiento)
        {
            return await _tipoMovimientoDA.ObtenerTipoMovimientoPorId(idTipoMovimiento);
        }
    }
}
