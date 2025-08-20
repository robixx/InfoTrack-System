using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ITC.InfoTrack.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]       
        public IActionResult Index()
        {
            string userName = User.Identity.Name;
            if(User.Identity.IsAuthenticated)
            {
                return View("~/Views/Dashboard/Index.cshtml");
            }
            else
            {
                return   View("~/Views/Home/Index.cshtml");
            }
           
        }
    }
}
