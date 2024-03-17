using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeRosaWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]

    public class AdminController : Controller
    {
        [HttpGet]
        [Route("{controller}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
