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
    public class EditarModel : PageModel
    {
        private IConfiguracion _configuracion;
        [BindProperty]
        public CategoriaResponse Categoria { get; set; } = default!;
        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }
        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id == null)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriaPorId");
            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Categoria = JsonSerializer.Deserialize<CategoriaResponse>(resultado, opciones)!;
            }
            return Page();
        }
        public async Task<ActionResult> OnPost()
        {
            if (Categoria.IdCategoria == Guid.Empty)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCategoria");
            var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value);
            var categoriaRequest = new CategoriaRequest
            {
                IdCategoria = Categoria.IdCategoria,
                Nombre = Categoria.Nombre,
                Descripcion = Categoria.Descripcion
            };
            var respuesta = await cliente.PutAsJsonAsync(string.Format(endpoint, Categoria.IdCategoria), categoriaRequest);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}
