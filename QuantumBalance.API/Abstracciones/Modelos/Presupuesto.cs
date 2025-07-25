using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class PresupuestoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad monto límite es requerida")]
        public decimal MontoLimite { get; set; }

        public decimal MontoGastado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
    }

    public class PresupuestoRequest : PresupuestoBase
    {
        public Guid Id { get; set; }
        public Guid IdCuenta { get; set; }
        public Guid IdCategoria { get; set; }
    }

    public class PresupuestoResponse : PresupuestoRequest { }
}
