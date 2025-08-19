using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Movimientos
{
    [Authorize(Roles = "1")]
    public class DetalleModel : PageModel
    {
        private IConfiguracion _configuracion;
        public MovimientoResponse movimiento { get; set; } = default!;
        public string nombreCuenta { get; set; } = string.Empty;
        public string nombreCategoria { get; set; } = string.Empty;
        public string nombreTipoMovimiento { get; set; } = string.Empty;

        public DetalleModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null) return;

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerMovimientoPorId");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                movimiento = JsonSerializer.Deserialize<MovimientoResponse>(resultado, opciones)!;
                nombreCuenta = await ObtenerNombrePorIdAsync("ObtenerCuentaPorId", movimiento.IdCuenta);
                nombreCategoria = await ObtenerNombrePorIdAsync("ObtenerCategoriaPorId", movimiento.IdCategoria);
                nombreTipoMovimiento = await ObtenerNombrePorIdAsync("ObtenerTipoMovimientoPorId", movimiento.IdTipoMovimiento);
            }
        }

        private async Task<string> ObtenerNombrePorIdAsync(string metodo, Guid id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", metodo);
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.GetAsync(string.Format(endpoint, id));
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(resultado);
                if (jsonDoc.RootElement.TryGetProperty("nombre", out var nombreProp))
                {
                    return nombreProp.GetString() ?? "No encontrado";
                }
            }
            return "No encontrado";
        }
    }
}
