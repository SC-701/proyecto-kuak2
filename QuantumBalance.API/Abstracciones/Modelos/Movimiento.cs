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
        [Required(ErrorMessage = "La propiedad descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La propiedad monto original es requerida")]
        public decimal MontoOriginal { get; set; }

        [Required(ErrorMessage = "La propiedad moneda original es requerida")]
        public string MonedaOriginal { get; set; }

        public decimal MontoCrc { get; set; }
        public decimal TasaCambio { get; set; }
        public DateTime Fecha { get; set; }
        public string ComprobanteUrl { get; set; }
        public bool Estado { get; set; }
    }

    public class MovimientoRequest : MovimientoBase
    {
        public Guid Id { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
        public Guid IdTipoPago { get; set; }
    }

    public class MovimientoResponse : MovimientoRequest { }
}
