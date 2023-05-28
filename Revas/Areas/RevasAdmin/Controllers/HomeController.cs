using Microsoft.AspNetCore.Mvc;

namespace Revas.Areas.RevasAdmin.Controllers
{
    [Area("RevasAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
