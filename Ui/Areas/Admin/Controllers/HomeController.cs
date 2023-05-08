using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        #region Home

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #endregion

    }
}
