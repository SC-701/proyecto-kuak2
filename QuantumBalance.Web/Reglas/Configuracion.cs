using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private IConfiguration _configuracion;

        public Configuracion(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public string ObtenerMetodo(string seccion, string nombrePropiedad)
        {
            var seccionConfig = _configuracion.GetSection(seccion).Get<ApiEndpoint>();

            if (seccionConfig == null)
                throw new InvalidOperationException($"No se encontró la sección '{seccion}' en appsettings.json");

            string urlBase = seccionConfig.UrlBase;

            var metodo = seccionConfig.Metodos
                .FirstOrDefault(m => m.Nombre == nombrePropiedad);

            if (metodo == null)
                throw new InvalidOperationException($"No se encontró el método '{nombrePropiedad}' en la sección '{seccion}'");

            return $"{urlBase}/{metodo.Valor}";
        }
    }
}
