using DocumentFormat.OpenXml.Office.CustomUI;
using ITC.InfoTrack.Model.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITC.InfoTrack.Areas.TokenGenerate.Controllers
{

    [Area("TokenGenerate")]
    [Authorize]
    public class TokenGenerateController : Controller
    {
        private readonly IDropDown _drop;
        private readonly ICategoryData _categorydata;
        public TokenGenerateController(IDropDown drop, ICategoryData categoryData)
        {
            _drop = drop;
            _categorydata = categoryData;
        }

        [HttpGet]
        public IActionResult TokenCreate()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CategoryWiseData()
        {
            ViewBag.category = new SelectList(await _drop.getCategory(), "Id", "Name");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SaveCategoryWiseData( int categoryId, string valueName)
        {
            
            var userIdClaim = User.FindFirst("UserId");
            int loginUserId = Convert.ToInt32(userIdClaim.Value);
            var result= await _categorydata.SaveCategoryData(categoryId, valueName, loginUserId);
            if (categoryId == 0 && valueName==null)
            {
                return RedirectToAction("CategoryWiseData");
            }
            return Json(new
            {
                status = result.status,
                message=result.message,

            });

        }
    }
}
