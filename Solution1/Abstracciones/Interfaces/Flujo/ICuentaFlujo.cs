using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICuentaFlujo
    {
        Task<Guid> CrearCuenta(Cuenta cuenta);
        Task<Guid> ActualizarCuenta(Guid idCuenta, Cuenta cuenta);
        Task<Guid> EliminarCuenta(Guid idCuenta);
        Task<Cuenta> ObtenerCuentaPorId(Guid idCuenta);
        Task<IEnumerable<Cuenta>> ObtenerCuentas();
        Task<Cuenta> DetalleCuenta(Guid idCuenta);
    }
}
