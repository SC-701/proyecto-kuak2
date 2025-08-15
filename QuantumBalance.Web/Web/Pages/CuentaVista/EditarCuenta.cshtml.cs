using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Linq;

namespace Web.Pages.CuentaVista
{
    [Authorize(Roles = "1")]
    public class EditarCuentaModel : PageModel
    {
    private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaResponse cuenta { get; set; } = default!;

        [BindProperty]
        public CuentaRequest cuentaRequest { get; set; } = default!;

        public EditarCuentaModel(IConfiguracion configuracion)
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
                HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value
            );

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, IdCuenta));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var cuentaApi = JsonSerializer.Deserialize<CuentaResponse>(resultado, opciones);
                if (cuentaApi == null)
                {
                    return NotFound();
                }

                cuenta = cuentaApi;
                cuentaRequest = new CuentaRequest
                {
                    IdCuenta = cuentaApi.IdCuenta,
                    IdUsuario = cuentaApi.IdUsuario,
                    Nombre = cuentaApi.Nombre,
                    Descripcion = cuentaApi.Descripcion,
                    Tipo = cuentaApi.Tipo
                };
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || cuentaRequest == null || cuentaRequest.IdCuenta == Guid.Empty)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value
            );

            // PUT api/Cuenta/{IdCuenta}
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, cuentaRequest.IdCuenta), cuentaRequest);
            respuesta.EnsureSuccessStatusCode();

            // cuenta editada
            return RedirectToPage("/CuentaVista/ObtenerCuentaPorId", new { IdCuenta = cuentaRequest.IdCuenta });
        }
    }
}
