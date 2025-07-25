using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CuentaBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public bool PermitirSalarioNegativo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Estado { get; set; }
    }

    public class CuentaRequest : CuentaBase
    {
        public Guid idCuenta { get; set; }
        public Guid IdUsuario { get; set; }
    }

    public class CuentaResponse : CuentaBase
    {
        public Guid idCuenta { get; set; }
        public string Usuario { get; set; }
    }
}
