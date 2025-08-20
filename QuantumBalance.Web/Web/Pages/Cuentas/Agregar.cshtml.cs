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
            // Prellenar IdUsuario para el hidden usando el claim del usuario
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                if (cuenta == null)
                {
                    cuenta = new CuentaRequest();
                }
                cuenta.IdUsuario = userId;
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            // Asegurar IdUsuario desde los claims ANTES de validar el modelo
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Forbid();
            }

            if (cuenta == null)
            {
                cuenta = new CuentaRequest();
            }
            cuenta.IdUsuario = userId;

            // Si la API requiere IdCuenta en el create, generamos uno cuando esté vacío
            if (cuenta.IdCuenta == Guid.Empty)
            {
                cuenta.IdCuenta = Guid.NewGuid();
            }

            // Revalidar el modelo después de completar datos derivados (claims/ids)
            ModelState.Clear();
            TryValidateModel(cuenta);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "CrearCuenta");

            var cliente = new HttpClient();

            var token = HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(token))
            {
                return Forbid();
            }

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var respuesta = await cliente.PostAsJsonAsync(endpoint, cuenta);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}