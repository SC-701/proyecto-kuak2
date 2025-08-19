using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        [BindProperty]
        public CuentaRequest cuenta { get; set; } = default!;
        public List<SelectListItem> usuarios { get; set; } = new();

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            await CargarCombosAsync();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                cuenta = JsonSerializer.Deserialize<CuentaRequest>(resultado, opciones)!;
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (cuenta.IdCuenta == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var url = string.Format(endpoint, cuenta.IdCuenta);
            var respuesta = await cliente.PutAsJsonAsync(url, cuenta);
            if (respuesta.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            var error = await respuesta.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Error al editar la cuenta: {error}");
            return Page();
        }

        private async Task CargarCombosAsync()
        {
            usuarios = await ObtenerListaAsync<UsuarioResponse>(
                "ObtenerTodosLosUsuarios",
                u => u.IdUsuario,
                u => u.Nombre
            );
        }

        private async Task<List<SelectListItem>> ObtenerListaAsync<TApi>(
            string metodo,
            Func<TApi, Guid> getId,
            Func<TApi, string> getNombre)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", metodo);
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.GetAsync(endpoint);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var elementosApi = JsonSerializer.Deserialize<List<TApi>>(resultado, opciones);
                return elementosApi
                    .Select(e => new SelectListItem
                    {
                        Value = getId(e).ToString(),
                        Text = getNombre(e)
                    })
                    .ToList();
            }
            return new List<SelectListItem>();
        }
    }
}
