﻿using Abstracciones.Modelos;
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
        Task<IEnumerable<CategoriaResponse>> ObtenerTodasLasCategorias();
        Task<CategoriaResponse> ObtenerCategoriaPorId(Guid idCategoria); 
        Task<Guid> CrearCategoria(CategoriaRequest categoria);
        Task<Guid> EditarCategoria(Guid idCategoria, CategoriaRequest categoria);
        Task<bool> EliminarCategoria(Guid idCategoria);
    }

}

