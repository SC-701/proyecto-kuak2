using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Linq;
using System.Security.Claims;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]
    public class Editar : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaResponse cuenta { get; set; } = default!;

        [BindProperty]
        public CuentaRequest cuentaRequest { get; set; } = default!;

        public Editar(IConfiguracion configuracion)
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
            var token = HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(token))
            {
                return Forbid();
            }
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

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

        public async Task<IActionResult> OnPost()
        {
            if (cuentaRequest == null || cuentaRequest.IdCuenta == Guid.Empty)
                return NotFound();

            // Asegurar IdUsuario desde los claims antes de validar/enviar
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Forbid();
            }

            cuentaRequest.IdUsuario = userId;

            // Revalidar el modelo con los datos completados
            ModelState.Clear();
            TryValidateModel(cuentaRequest);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
            var cliente = new HttpClient();
            var token = HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(token))
            {
                return Forbid();
            }
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await cliente.PutAsJsonAsync(
                string.Format(endpoint, cuentaRequest.IdCuenta),
                cuentaRequest
            );
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}