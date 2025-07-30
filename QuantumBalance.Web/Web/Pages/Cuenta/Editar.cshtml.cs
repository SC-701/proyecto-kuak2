using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Text.Json;

namespace Web.Pages.Cuentas
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public CuentaRequest Cuenta { get; set; } = new();

        public List<SelectListItem> Categorias { get; set; } = new();
        public List<SelectListItem> Usuarios { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToPage("Index");

            await CargarCategoriasAsync();
            await CargarUsuariosAsync();

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCuentaPorId");
            endpoint = string.Format(endpoint, id);

            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            var json = await response.Content.ReadAsStringAsync();
            var cuentaResponse = JsonSerializer.Deserialize<CuentaResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (cuentaResponse == null)
                return RedirectToPage("Index");

            Cuenta = new CuentaRequest
            {
                idCuenta = cuentaResponse.idCuenta,
                Nombre = cuentaResponse.Nombre,
                Descripcion = cuentaResponse.Descripcion,
                idCategoria = cuentaResponse.idCategoria,
                IdUsuario = Guid.Empty,
                PermitirSalarioNegativo = cuentaResponse.PermitirSalarioNegativo,
                FechaCreacion = cuentaResponse.FechaCreacion,
                FechaUltimaModificacion = cuentaResponse.FechaUltimaModificacion,
                Estado = cuentaResponse.Estado
            };

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

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarCuenta");
            endpoint = string.Format(endpoint, Cuenta.idCuenta);

            using var client = new HttpClient();
            var response = await client.PutAsJsonAsync(endpoint, Cuenta);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al actualizar la cuenta.");
                await CargarCategoriasAsync();
                await CargarUsuariosAsync();
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task CargarCategoriasAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var categorias = JsonSerializer.Deserialize<List<CategoriaResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CategoriaResponse>();

            Categorias = categorias.Select(c => new SelectListItem
            {
                Value = c.idCategoria.ToString(),
                Text = c.Nombre
            }).ToList();
        }

        private async Task CargarUsuariosAsync()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerUsuarios");
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var usuarios = JsonSerializer.Deserialize<List<UsuarioResponse>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<UsuarioResponse>();

            Usuarios = usuarios.Select(u => new SelectListItem
            {
                Value = u.idUsuario.ToString(),
                Text = $"{u.Nombre} {u.PrimerApellido}"
            }).ToList();
        }
    }
}
