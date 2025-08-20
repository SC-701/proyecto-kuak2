using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    [Authorize(Roles = "1")]
    public class Agregar : PageModel
    {
        private IConfiguracion _configuracion;

        [BindProperty]
        public CuentaRequest cuenta { get; set; } = new();

        public Agregar(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || cuenta == null)
            {
                return Page();
            }

            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearCuenta");

            var cliente = new HttpClient();

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, cuenta);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}