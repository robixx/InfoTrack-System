using DocumentFormat.OpenXml.Office.CustomUI;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

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
        public async Task<IActionResult> TokenCreate()
        {
            ViewBag.type = new SelectList(await _drop.GetTokenType(), "Id", "Name"); 
            ViewBag.district = new SelectList(await _drop.getDistrict(), "Id", "Name"); 
            ViewBag.division = new SelectList(await _drop.getDivision(), "Id", "Name"); 

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CategoryWiseData()
        {
            ViewBag.category = new SelectList(await _drop.getCategory(), "Id", "Name");
            var data = await _categorydata.getCategoryWiseData();
            return View(data);

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


        [HttpGet]
        public async Task<IActionResult> TypeWiseElementName(string typeId)
        {
            int id= Convert.ToInt32(typeId);
            var result= await _drop.getRootPropertyElement(id);
            return Json( new {status=result.status, data=result.data });
        }

        [HttpGet]
        public async Task<IActionResult> TypeWiseTokenData()
        {
            var data = await _categorydata.getCategoryWiseData();
            return Json(new { status = true, data = data });
        }


        [HttpPost]
        public async Task<IActionResult> SaveTokenData(MainFormViewModelDto model)
        {
            var userIdClaim = User.FindFirst("UserId");
            int loginUserId = Convert.ToInt32(userIdClaim.Value);
            var result = await _categorydata.SaveTokenGenerateData(model, loginUserId);
            return Json(new {message=result.message , success = result.success });
        }

        [HttpGet]
        public async Task<IActionResult> GetTokenList()
        {
            var result= await _categorydata.getTokenData();
            return View(result);

        }
    }
}
