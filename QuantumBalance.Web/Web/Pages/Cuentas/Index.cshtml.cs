using Microsoft.AspNetCore.Mvc.RazorPages;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private IConfiguracion _configuracion;
        public IList<CuentaResponse> Cuentas { get; set; } = default!;

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTodasLasCuentas");
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Cuentas = JsonSerializer.Deserialize<List<CuentaResponse>>(resultado, opciones);
            }
        }
    }
}
