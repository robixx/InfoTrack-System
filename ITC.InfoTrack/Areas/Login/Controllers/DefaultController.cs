using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace ITC.InfoTrack.Areas.Login.Controllers
{
    [Area("Login")]
    public class DefaultController : Controller
    {
        private readonly IAuth _auth;
        public DefaultController(IAuth auth)
        {
            _auth = auth;
        }

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
        public async Task<IActionResult> DoLogin([FromBody] LoginRequest loginRequest)
        {
            var auth= await _auth.LoginAsync(loginRequest);
            if (auth == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Invalid username or password."
                });
            }
            return Json(new
            {
                status = true,
                redirectUrl = Url.Action("Index", "Dashboard", new { area = "Dashboard" })
            });
        }
    }
}
