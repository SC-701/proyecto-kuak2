using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace Web.Pages.Movimiento
{
    [Authorize(Roles = "1")]
    public class CrearMovimientoModel : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public MovimientoRequest Movimiento { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> TipoMovimientos { get; set; } = default!;


        public CrearMovimientoModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<ActionResult> OnGet()
        {
            //que devuelve??????
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Movimiento == null)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearMovimiento");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value
            );
            var respuesta = await cliente.PostAsJsonAsync(endpoint, Movimiento);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("/Index");
        }

        private async Task ObtenerTiposMovimientoAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerTiposMovimientos");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultadoDeserializado = JsonSerializer.Deserialize<List<TipoMovimiento>>(resultado, opciones);
                TipoMovimientos = resultadoDeserializado.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Nombre.ToString()
                                  }).ToList();
            }
        }
    }
}
