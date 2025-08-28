using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITC.InfoTrack.Areas.ManagementApproval.Controllers
{
    [Area("ManagementApproval")]
    [Authorize]
    public class ManagementApprovalController : Controller
    {

        [HttpGet]
        public IActionResult LogApproval()
        {
            return View();
        }
    }
}
