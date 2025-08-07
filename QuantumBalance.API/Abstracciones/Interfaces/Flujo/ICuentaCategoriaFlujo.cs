using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ICuentaCategoriaFlujo
    {
        Task<IEnumerable<CuentaCategoriaResponse>> ObtenerTodas();
        Task<CuentaCategoriaResponse?> ObtenerPorId(Guid idCuentaCategoria);
        Task<Guid> Crear(CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> Editar(Guid idCuentaCategoria, CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> Eliminar(Guid idCuentaCategoria);
    }
}
