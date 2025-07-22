using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Cuenta
    {
        [Key]
        public Guid IdCuenta { get; set; }

        [Required(ErrorMessage = "El nombre de la cuenta es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string NombreCuenta { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede tener más de 250 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ0-9\s.,;:()¡!¿?'-]*$", ErrorMessage = "La descripción contiene caracteres inválidos.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }
    }
}
