using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Cuentas
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public List<CuentaResponse> Cuentas { get; set; } = new();

        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGetAsync()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentas");

            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Cuentas = JsonSerializer.Deserialize<List<CuentaResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CuentaResponse>();
        }
    }
}
