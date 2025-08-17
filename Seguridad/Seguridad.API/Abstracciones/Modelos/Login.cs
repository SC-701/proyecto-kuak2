using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class LoginBase
    {
        [Required]
        [JsonPropertyName("nombreUsuario")]
        public string NombreUsuario { get; set; }
        [Required]
        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        [JsonPropertyName("correoElectronico")]
        public string CorreoElectronico { get; set; }
    }
    public class Login : LoginBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
