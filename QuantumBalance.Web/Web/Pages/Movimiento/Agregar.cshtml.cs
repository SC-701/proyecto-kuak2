using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Movimientos
{
    [Authorize(Roles = "1")]
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public MovimientoRequest movimiento { get; set; } = default!;
        public List<SelectListItem> cuentas { get; set; } = new();
        public List<SelectListItem> categorias { get; set; } = new();
        public List<SelectListItem> tiposMovimiento { get; set; } = new();
        public bool FaltanDatos { get; set; }
        public string MensajeAdvertencia { get; set; } = string.Empty;


        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet()
        {
            await CargarCombosAsync();

            if (!cuentas.Any() || !categorias.Any())
            {
                FaltanDatos = true;
                if (!cuentas.Any() && !categorias.Any())
                    MensajeAdvertencia = "No hay cuentas ni categorías registradas para crear un movimiento.";
                else if (!cuentas.Any())
                    MensajeAdvertencia = "No hay cuentas registradas para crear un movimiento.";
                else if (!categorias.Any())
                    MensajeAdvertencia = "No hay categorías registradas para crear un movimiento.";
            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await CargarCombosAsync();

            if (!cuentas.Any() || !categorias.Any())
            {
                FaltanDatos = true;
                MensajeAdvertencia = !cuentas.Any() && !categorias.Any()
                    ? "No hay cuentas ni categorías registradas para crear un movimiento."
                    : !cuentas.Any()
                        ? "No hay cuentas registradas para crear un movimiento."
                        : "No hay categorías registradas para crear un movimiento.";

                ModelState.AddModelError(string.Empty, "No se puede crear un movimiento sin cuentas y categorías registradas.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearMovimiento");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, movimiento);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }


        private async Task CargarCombosAsync()
        {
            cuentas = await ObtenerListaAsync<CuentaResponse>(
                "ObtenerTodasLasCuentas",
                c => c.IdCuenta,
                c => c.Nombre
            );

            categorias = await ObtenerListaAsync<CategoriaResponse>(
                "ObtenerTodasLasCategorias",
                c => c.IdCategoria,
                c => c.Nombre
            );

            tiposMovimiento = await ObtenerListaAsync<TipoMovimiento>(
                "ObtenerTiposMovimientos",
                t => t.idtipomovimiento,
                t => t.Nombre
            );
        }
        private async Task<List<SelectListItem>> ObtenerListaAsync<TApi>(string metodo,Func<TApi, Guid> getId,Func<TApi, string> getNombre)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", metodo);
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue( "Bearer", HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value);

            using var respuesta = await cliente.GetAsync(endpoint);
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var elementosApi = JsonSerializer.Deserialize<List<TApi>>(json, opciones) ?? new List<TApi>();

                return elementosApi
                    .Select(e => new SelectListItem
                    {
                        Value = getId(e).ToString(),
                        Text = getNombre(e)
                    })
                    .ToList();
            }

            if (respuesta.StatusCode == HttpStatusCode.NotFound ||
                respuesta.StatusCode == HttpStatusCode.NoContent)
            {
                return new List<SelectListItem>();
            }
            return new List<SelectListItem>();
        }

    }
}
