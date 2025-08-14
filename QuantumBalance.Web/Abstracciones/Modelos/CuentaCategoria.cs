using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CuentaCategoriaBase
    {
        [Required(ErrorMessage = "El IdCategoria es requerido")]
        public Guid IdCategoria { get; set; }

        [Required(ErrorMessage = "El IdCuenta es requerido")]
        public Guid IdCuenta { get; set; }
    }

    public class CuentaCategoriaRequest : CuentaCategoriaBase
    {
    }

    public class CuentaCategoriaResponse : CuentaCategoriaBase
    {
    }
}
