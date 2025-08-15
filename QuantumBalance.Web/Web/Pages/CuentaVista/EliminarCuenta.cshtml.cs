using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;
using System.Linq;

namespace Web.Pages.CuentaVista
{
    [Authorize(Roles = "1")]
    public class EliminarCuentaModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public CuentaResponse cuenta { get; set; } = default!;

        public EliminarCuentaModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGetAsync(Guid? IdCuenta)
        {
            if (IdCuenta == null || IdCuenta == Guid.Empty)
            {
                return NotFound();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value
            );

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, IdCuenta));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var cuentaApi = JsonSerializer.Deserialize<CuentaResponse>(json, opciones);
                if (cuentaApi == null)
                {
                    return NotFound();
                }
                cuenta = cuentaApi;
                return Page();
            }

            if (respuesta.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            respuesta.EnsureSuccessStatusCode();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? IdCuenta)
        {
            if (IdCuenta == null || IdCuenta == Guid.Empty)
            {
                // intenta usar Id desde el modelo si no vino por parámetro
                if (cuenta == null || cuenta.IdCuenta == Guid.Empty)
                {
                    return Page();
                }
                IdCuenta = cuenta.IdCuenta;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCuenta");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value
            );

            var respuesta = await cliente.DeleteAsync(string.Format(endpoint, IdCuenta));
            respuesta.EnsureSuccessStatusCode();

            // eliminado 

            return RedirectToPage("/Index");
        }
    }
}
