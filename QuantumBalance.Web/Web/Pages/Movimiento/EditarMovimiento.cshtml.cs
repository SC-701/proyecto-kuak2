using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Movimiento
{
    [Authorize(Roles = "1")]

    public class EditarMovimientoModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
