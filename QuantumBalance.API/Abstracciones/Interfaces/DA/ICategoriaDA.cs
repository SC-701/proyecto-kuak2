using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ICategoriaDA
    {
        Task<IEnumerable<CategoriaResponse>> MostrarCategorias();
        Task<Guid> CrearCategoria(CategoriaRequest categoria);
        Task EditarCategoria(CategoriaRequest categoria);
        Task EliminarCategoria(Guid idCategoria);
    }
}

