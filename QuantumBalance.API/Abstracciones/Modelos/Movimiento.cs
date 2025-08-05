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
        [Required(ErrorMessage = "El IdCuenta es requerido")]
        public Guid IdCuenta { get; set; }

        [Required(ErrorMessage = "El IdCategoria es requerido")]
        public Guid IdCategoria { get; set; }

        [Required(ErrorMessage = "El IdTipoMovimiento es requerido")]
        public Guid IdTipoMovimiento { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El monto es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }
    }

    public class MovimientoRequest : MovimientoBase
    {
        public Guid IdMovimiento { get; set; }
    }

    public class MovimientoResponse : MovimientoBase
    {
        public Guid IdMovimiento { get; set; }
    }
}
