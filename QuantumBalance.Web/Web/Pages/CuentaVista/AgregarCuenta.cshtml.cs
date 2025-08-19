using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net.Http.Json;

namespace Web.Pages.CuentaVista
{
    [Authorize(Roles = "1")]

    public class AgregarCuentaModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public CuentaCategoriaRequest cuenta { get; set; } = default!;

        public AgregarCuentaModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }


        public async Task<ActionResult> OnGet()
        {
            //?????
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || cuenta == null)
            {
                return Page();
            }
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCuenta");

            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, cuenta);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("/Index");
            
        }
    }
}
