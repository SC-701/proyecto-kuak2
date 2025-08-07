using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MovimientoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
