using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
