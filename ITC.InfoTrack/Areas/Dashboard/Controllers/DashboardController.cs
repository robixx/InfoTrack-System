using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ITC.InfoTrack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class DashboardController : Controller
    {
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Index.cshtml");
        }
    }
}
