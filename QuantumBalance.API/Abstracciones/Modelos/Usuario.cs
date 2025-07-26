using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class UsuarioBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad primer apellido es requerida")]
        [StringLength(100)]
        public string PrimerApellido { get; set; }

        [StringLength(100)]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "La propiedad email es requerida")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La propiedad contraseña es requerida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La propiedad moneda principal es requerida")]
        public string MonedaPrincipal { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public bool Estado { get; set; }
    }

    public class UsuarioRequest : UsuarioBase
    {
        public Guid idUsuario { get; set; }
    }

    public class UsuarioResponse : UsuarioBase
    {
        public Guid idUsuario { get; set; }
    }
}
