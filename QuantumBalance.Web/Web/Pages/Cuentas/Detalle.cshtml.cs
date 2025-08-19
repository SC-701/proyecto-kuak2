using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public CuentaResponse cuenta { get; set; } = default!;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null) return;

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                cuenta = JsonSerializer.Deserialize<CuentaResponse>(resultado, opciones)!;
            }
        }
    }
}
