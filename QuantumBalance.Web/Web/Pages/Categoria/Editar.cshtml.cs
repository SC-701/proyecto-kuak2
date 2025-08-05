using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Web.Pages.Categoria
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public CategoriaResponse Categoria { get; set; }

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de la categor�a no puede estar vac�o.");
                return;
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategoriaPorId");

            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string resultado = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Categoria = JsonSerializer.Deserialize<CategoriaResponse>(resultado, options);
        }

        public async Task<ActionResult> OnPost()
        {
            if(Categoria.idCategoria == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "El ID de la categor�a no puede estar vac�o.");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCategoria");  

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PutAsJsonAsync<CategoriaRequest>(string.Format(endpoint, Categoria.idCategoria), new CategoriaRequest
            {
                idCategoria = Categoria.idCategoria,
                Nombre = Categoria.Nombre,
                Descripcion = Categoria.Descripcion,
            });
            response.EnsureSuccessStatusCode();

            return RedirectToPage("./Index");
        }
    }
}