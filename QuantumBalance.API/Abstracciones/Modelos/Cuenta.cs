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
        [Required(ErrorMessage = "El ID del usuario es requerido")]
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo es requerido")]
        public string Tipo { get; set; }
    }

    public class CuentaRequest : CuentaBase
    {
        public Guid IdCuenta { get; set; }
    }

    public class CuentaResponse : CuentaBase
    {
        public Guid IdCuenta { get; set; }
    }
}
