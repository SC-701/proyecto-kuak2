using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(150, ErrorMessage = "El correo no puede tener más de 150 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; }

    }
}
