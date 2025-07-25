using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class TipoPagoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public bool Activestado { get; set; }
    }

    public class TipoPagoRequest : TipoPagoBase
    {
        public Guid Id { get; set; }
    }

    public class TipoPagoResponse : TipoPagoBase
    {
        public Guid Id { get; set; }
    }
}
