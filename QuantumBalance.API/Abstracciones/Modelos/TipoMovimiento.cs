using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class TipoMovimientoBase
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(255)]
        public string Nombre { get; set; }
    }

    public class TipoMovimientoRequest : TipoMovimientoBase
    {
        public Guid IdTipoMovimiento { get; set; }
    }

    public class TipoMovimientoResponse : TipoMovimientoBase
    {
        public Guid IdTipoMovimiento { get; set; }
    }
}
