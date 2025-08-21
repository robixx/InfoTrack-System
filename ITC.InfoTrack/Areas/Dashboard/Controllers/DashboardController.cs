using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authentication;
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
        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return   View("~/Views/Home/Index.cshtml");
            }
           
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            // Sign out the user
            await HttpContext.SignOutAsync("CookieAuth");

            // Redirect to login page
            return RedirectToAction("Index", "Default", new { area = "Login" });
        }
    }
}
