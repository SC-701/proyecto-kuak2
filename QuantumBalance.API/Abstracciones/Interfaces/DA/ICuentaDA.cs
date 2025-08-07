using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICuentaDA
    {
        Task<IEnumerable<CuentaResponse>> ObtenerTodasLasCuentas();
        Task<CuentaResponse> ObtenerCuentaPorId(Guid idCuenta);
        Task<Guid> CrearCuenta(CuentaRequest cuenta);
        Task<Guid> EditarCuenta(Guid idCuenta, CuentaRequest cuenta);
        Task<Guid> EliminarCuenta(Guid idCuenta);
    }
}
