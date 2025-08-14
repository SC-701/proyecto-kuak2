using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CategoriaBase
    {
        [Required(ErrorMessage = "La propiedad 'Nombre' es requerida")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad 'Descripcion' es requerida")]
        [StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres")]
        public string Descripcion { get; set; }
    }

    public class CategoriaRequest : CategoriaBase
    {
        public Guid IdCategoria { get; set; } 
    }

    public class CategoriaResponse : CategoriaBase
    {
        public Guid IdCategoria { get; set; }
    }

}
