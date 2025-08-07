using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flujo
{
    public class CuentaFlujo : ICuentaFlujo
    {
        private readonly ICuentaDA _cuentaDA;

        public CuentaFlujo(ICuentaDA cuentaDA)
        {
            _cuentaDA = cuentaDA;
        }

        public async Task<Guid> CrearCuenta(CuentaRequest cuenta)
        {
            return await _cuentaDA.CrearCuenta(cuenta);
        }

        public async Task<Guid> EditarCuenta(Guid idCuenta, CuentaRequest cuenta)
        {
            return await _cuentaDA.EditarCuenta(idCuenta, cuenta);
        }

        public async Task<Guid> EliminarCuenta(Guid id)
        {
            return await _cuentaDA.EliminarCuenta(id);
        }

        public async Task<CuentaResponse> ObtenerCuentaPorId(Guid idCuenta)
        {
            return await _cuentaDA.ObtenerCuentaPorId(idCuenta);
        }


        public async Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas()
        {
            return await _cuentaDA.ObtenerTodasLasCuentas();
        }
    }
}
