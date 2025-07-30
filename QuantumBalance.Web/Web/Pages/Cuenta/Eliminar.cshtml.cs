using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public CuentaResponse Cuenta { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToPage("Index");

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            endpoint = string.Format(endpoint, id);

            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            var json = await response.Content.ReadAsStringAsync();
            Cuenta = JsonSerializer.Deserialize<CuentaResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new CuentaResponse();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Cuenta.idCuenta == Guid.Empty)
                return RedirectToPage("Index");

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarCuenta");
            endpoint = string.Format(endpoint, Cuenta.idCuenta);

            using var client = new HttpClient();
            var response = await client.DeleteAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar la cuenta.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
