using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ITC.InfoTrack.Areas.Corporate.Controllers
{
    [Area("Corporate")]
    public class CorporateController : Controller
    {

        private readonly ICorporate _corporate;
        private readonly IDropDown _dropdown;
        public CorporateController(ICorporate corporate, IDropDown dropDown)
        {
            _corporate = corporate;
            _dropdown = dropDown;
        }


        [HttpGet]
        public async Task<IActionResult> CorporateOfficeRegister()
        {
            var result = await _corporate.getOfficeList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> OfficeDataSave([FromBody] CorporateOfficeDto model)
        {
            var result = await _corporate.addOfficeInformation(model);
            return Ok(new { message = result.Message, status = result.Status });
        }

        [HttpGet]
        public async Task<IActionResult> OfficeDataEdit(int corpId)
        {
            long copid = Convert.ToInt64(corpId);
            var data = await _corporate.OfficeDataFind(copid);
            
            return Ok(new { data = data, status = true });
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleCreate()
        {
            ViewBag.office = new SelectList(await _dropdown.getOfficeList(), "Id", "Name");
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetsubBranches(int branchId)
        {
            long branchid = Convert.ToInt64(branchId);
            var branches =  await _dropdown.getBranchList(branchid);

            return Json(branches);
        }
        [HttpGet]
        public async Task<IActionResult> GetBooths(long branchId)
        {

            var booths = await _dropdown.getboothList(branchId);
            return Json(booths);
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets(int boothId)
        {

            var assets = await _dropdown.getAssetList(boothId);
            return Json(assets);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredData( int? branchId, int? subbranch, int? district , int? division)
        {
            
            long branch = Convert.ToInt64(branchId);
            int subbranchid = subbranch ?? 0;
            int districtid = district ?? 0;
            int divisionid = division ?? 0;
            var data = await _corporate.ScheduleDataFind( branch, subbranchid, districtid, divisionid);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult>GetLoadeduser(string dateValue)
        {
            var result= await _corporate.GetUserDateWise(dateValue);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> SaveScheduleList([FromBody] List<ScheduleEntryDto> scheduleData)
        {
            return View();
        }
    }
}
