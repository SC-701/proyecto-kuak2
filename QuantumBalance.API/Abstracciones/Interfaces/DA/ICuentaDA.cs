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
        Task<IEnumerable<CuentaResponse>> MostrarCuentas();
        Task<Guid> CrearCuenta(CuentaRequest cuenta);
        Task EditarCuenta(CuentaRequest cuenta);
        Task EliminarCuenta(Guid idCuenta);
    }
}
