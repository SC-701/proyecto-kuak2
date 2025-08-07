using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

public class CategoriaFlujo : ICategoriaFlujo
{
    private readonly ICategoriaDA _categoriaDA;

    public CategoriaFlujo(ICategoriaDA categoriaDA)
    {
        _categoriaDA = categoriaDA;
    }

    public async Task<Guid> CrearCategoria(CategoriaRequest categoria)
    {
        return await _categoriaDA.CrearCategoria(categoria);
    }

    public async Task<Guid> EditarCategoria(Guid idCategoria, CategoriaRequest categoria)
    {
        return await _categoriaDA.EditarCategoria(idCategoria, categoria);
    }

    public async Task<Guid> EliminarCategoria(Guid idCategoria)
    {
        return await _categoriaDA.EliminarCategoria(idCategoria);
    }

    public async Task<CategoriaResponse> ObtenerCategoriaPorId(Guid idCategoria)
    {
        return await _categoriaDA.ObtenerCategoriaPorId(idCategoria);
    }

    public async Task<IEnumerable<CategoriaResponse>> ObtenerTodasLasCategorias()
    {
        return await _categoriaDA.ObtenerTodasLasCategorias();
    }
}
