using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class DetalleModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public DetalleModel(IConfiguracion configuracion)
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
    }
}
