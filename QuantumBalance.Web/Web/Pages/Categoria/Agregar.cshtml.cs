using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Categoria
{
    [Authorize(Roles = "1")]
    public class AgregarModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public CategoriaRequest categoria { get; set; } = default!;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCategoria");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, categoria);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("/Categoria/Index");
        }
    }
}