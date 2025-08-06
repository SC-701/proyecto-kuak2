﻿using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.DA
{
    public interface ITipoMovimientoDA
    {
        Task<IEnumerable<TipoMovimientoResponse>> ObtenerTiposMovimientos();
        Task<TipoMovimientoResponse> ObtenerTipoMovimientoPorId(Guid IdTipoMovimiento);


    }
}
