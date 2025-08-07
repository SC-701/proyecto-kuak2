using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

public class CuentaCategoriaFlujo : ICuentaCategoriaFlujo
{
    private readonly ICuentaCategoriaDA _cuentaCategoriaDA;

    public CuentaCategoriaFlujo(ICuentaCategoriaDA cuentaCategoriaDA)
    {
        _cuentaCategoriaDA = cuentaCategoriaDA;
    }

    public async Task<IEnumerable<CuentaCategoriaResponse>> ObtenerTodas()
    {
        return await _cuentaCategoriaDA.ObtenerCuentaCategorias();
    }

    public async Task<CuentaCategoriaResponse?> ObtenerPorId(Guid idCuentaCategoria)
    {
        return await _cuentaCategoriaDA.ObtenerCuentaCateoriaPorId(idCuentaCategoria);
    }

    public async Task<Guid> Crear(CuentaCategoriaRequest cuentaCategoria)
    {
        return await _cuentaCategoriaDA.CrearCuentaCategoria(cuentaCategoria);
    }

    public async Task<Guid> Editar(Guid idCuentaCategoria, CuentaCategoriaRequest cuentaCategoria)
    {
        return await _cuentaCategoriaDA.EditarCuentaCategoria(idCuentaCategoria, cuentaCategoria);
    }

    public async Task<Guid> Eliminar(Guid idCuentaCategoria)
    {
        return await _cuentaCategoriaDA.EliminarCuentaCategoria(idCuentaCategoria);
    }
}
