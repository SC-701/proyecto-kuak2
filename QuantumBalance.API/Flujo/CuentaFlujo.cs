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
    public class CuentaFlujo : ICuentaFlujo
    {
        private ICuentaDA _cuentaDA;
        public CuentaFlujo(ICuentaDA cuentaDA)
        {
            _cuentaDA = cuentaDA;
        }

        public async Task<Guid> CrearCuenta(Cuenta cuenta)
        {
            return await _cuentaDA.CrearCuenta(cuenta);
        }
        public async Task<Guid> ActualizarCuenta(Guid idCuenta, Cuenta cuenta)
        {
            return await _cuentaDA.ActualizarCuenta(idCuenta, cuenta);
        }
        public async Task<Guid> EliminarCuenta(Guid idCuenta)
        {
            return await _cuentaDA.EliminarCuenta(idCuenta);
        }
        public async Task<Cuenta> ObtenerCuentaPorId(Guid idCuenta)
        {
            return await _cuentaDA.ObtenerCuentaPorId(idCuenta);
        }
        public async Task<IEnumerable<Cuenta>> ObtenerCuentas()
        {
            return await _cuentaDA.ObtenerCuentas();
        }
        public async Task<Cuenta> DetalleCuenta(Guid idCuenta)
        {
            return await _cuentaDA.DetalleCuenta(idCuenta);
        }

    }
}
