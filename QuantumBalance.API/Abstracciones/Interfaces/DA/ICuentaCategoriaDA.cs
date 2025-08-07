using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICuentaCategoriaDA
    {
        Task<IEnumerable<CuentaCategoriaResponse>> ObtenerCuentaCategorias();
        Task<CuentaCategoriaResponse?> ObtenerCuentaCateoriaPorId(Guid idCuentaCategoria);
        Task<Guid> CrearCuentaCategoria(CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EditarCuentaCategoria(Guid idCuentaCategoria, CuentaCategoriaRequest cuentaCategoria);
        Task<Guid> EliminarCuentaCategoria(Guid idCuentaCategoria);
    }
}
