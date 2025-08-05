using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        [BindProperty]
        public CuentaRequest Cuenta { get; set; } = new();

        public List<SelectListItem> Categorias { get; set; } = new();
        public List<SelectListItem> Usuarios { get; set; } = new();

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await CargarCategoriasAsync();
            await CargarUsuariosAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await CargarCategoriasAsync();
                await CargarUsuariosAsync();
                return Page();
            }

            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarCuenta");

            using var client = new HttpClient();
            var response = await client.PostAsJsonAsync(endpoint, Cuenta);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar la cuenta");
                await CargarCategoriasAsync();
                await CargarUsuariosAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task CargarCategoriasAsync()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categorias = JsonSerializer.Deserialize<List<CategoriaResponse>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();

                Categorias = categorias.Select(c => new SelectListItem
                {
                    Value = c.idCategoria.ToString(),
                    Text = c.Nombre
                }).ToList();
            }
        }

        private async Task CargarUsuariosAsync()
        {
            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUsuarios");
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuarios = JsonSerializer.Deserialize<List<UsuarioResponse>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();

                Usuarios = usuarios.Select(u => new SelectListItem
                {
                    Value = u.idUsuario.ToString(),
                    Text = $"{u.Nombre} {u.PrimerApellido}"
                }).ToList();
            }
        }
    }
}
