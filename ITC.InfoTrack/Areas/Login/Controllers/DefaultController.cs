using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace ITC.InfoTrack.Areas.Login.Controllers
{
    [Area("Login")]
    public class DefaultController : Controller
    {
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        [AllowAnonymous]
        //[Route("Authenticate")]
        public async Task<IActionResult> DoLogin()
        {
            return View();
        }
    }
}
