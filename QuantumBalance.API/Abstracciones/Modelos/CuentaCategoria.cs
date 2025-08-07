using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    // Base class for shared properties
    public class CuentaCategoriaBase
    {
        [Required(ErrorMessage = "El IdCategoria es requerido")]
        public Guid IdCategoria { get; set; }

        [Required(ErrorMessage = "El IdCuenta es requerido")]
        public Guid IdCuenta { get; set; }
    }

    // Request class inherits from base class and adds the 'NombreCuenta' property
    public class CuentaCategoriaRequest : CuentaCategoriaBase
    {
        public string NombreCuenta { get; set; } // Represents the name/description of the account in this category
    }

    // Response class inherits from base class and adds the 'NombreCategoria' property
    public class CuentaCategoriaResponse : CuentaCategoriaBase
    {
        public string NombreCategoria { get; set; } // Represents the name of the category
    }
}
