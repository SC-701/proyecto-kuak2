using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class UsuarioBase
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(255)]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El segundo apellido es requerido")]
        [StringLength(255)]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(255, ErrorMessage = "La contraseña no puede exceder los 255 caracteres")]
        public string Contrasena { get; set; }
    }
    public class UsuarioRequest : UsuarioBase
    {
        public Guid IdUsuario { get; set; }
    }

    public class UsuarioResponse : UsuarioBase
    {
        public Guid IdUsuario { get; set; }
    }
}
