using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class MovimientoBase
    {
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }
    }

    public class MovimientoRequest : MovimientoBase
    {
        public Guid IdMovimiento { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdTipoMovimiento { get; set; }
    }

    public class MovimientoResponse : MovimientoBase
    {
        public Guid IdMovimiento { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdTipoMovimiento { get; set; }
    }
}
