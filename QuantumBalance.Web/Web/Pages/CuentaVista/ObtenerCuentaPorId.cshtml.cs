using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.CuentaVista
{
    [Authorize(Roles = "1")]

    public class ObtenerCuentaPorIdModel : PageModel
    {

        private IConfiguracion _configuracion;

        public CuentaResponse cuenta { get; set; } = default!;

        public ObtenerCuentaPorIdModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task OnGetAsync(Guid? IdCuenta)
        {
            string endPoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value
                );

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endPoint, IdCuenta));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                cuenta = JsonSerializer.Deserialize<CuentaResponse>(resultado, opciones);
            }
        }

    }
}
