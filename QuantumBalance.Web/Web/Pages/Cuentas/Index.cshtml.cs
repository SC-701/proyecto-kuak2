using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]

    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
    public IList<CuentaResponse> Cuentas { get; set; } = new List<CuentaResponse>();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodasLasCuentas");
            var cliente = new HttpClient();
            var token = HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(token))
            {
                // Sin token, no intentamos llamar; dejamos la lista vacía.
                return;
            }

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Cuentas = JsonSerializer.Deserialize<List<CuentaResponse>>(resultado, opciones) ?? new List<CuentaResponse>();
                return;
            }

            if (respuesta.StatusCode == HttpStatusCode.NotFound || respuesta.StatusCode == HttpStatusCode.NoContent)
            {
                // Sin datos: dejamos lista vacía sin lanzar excepción.
                Cuentas = new List<CuentaResponse>();
                return;
            }

            // Para cualquier otro error, lanzar para que el middleware muestre detalles
            respuesta.EnsureSuccessStatusCode();
        }
    }
}