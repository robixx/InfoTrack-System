using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ITC.InfoTrack.Areas.Corporate.Controllers
{
    [Area("Corporate")]
    [Authorize]
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
            ViewBag.branchname = new SelectList(await _dropdown.getConfigBranchList(), "Id", "Name");
            ViewBag.type = new SelectList(await _dropdown.GetTokenType(), "Id", "Name");
            ViewBag.district = new SelectList(await _dropdown.getDistrict(), "Id", "Name");
            ViewBag.division = new SelectList(await _dropdown.getDivision(), "Id", "Name");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetsubBranches(int branchId)
        {
            
            var branches =  await _dropdown.getBranchList(branchId);

            return Json(branches);
        }


        [HttpGet]
        public async Task<IActionResult> GetDistrictCode(int subbranchId)
        {

            var branches = await _dropdown.getDistrictList(subbranchId);

            return Json(branches);
        }

        [HttpGet]
        public async Task<IActionResult> GetDivisionCode(int districtid)
        {

            var branches = await _dropdown.getDistrictList(districtid);

            return Json(branches);
        }



        [HttpGet]
        public async Task<IActionResult> GetBooths(long branchId)
        {
            long branchid= Convert.ToInt64(branchId);
            var booths = await _dropdown.getboothList(branchid);
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
            
            int branch = branchId??0;
            int subbranchid = subbranch ?? 0;
            int districtid = district ?? 0;
            int divisionid = division ?? 0;
            var data = await _corporate.GetOrganizationHierarchyAsync(branch, subbranchid, districtid, divisionid);
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
            var result= await _corporate.SaveScheduleListAsync(scheduleData);
            return  Json(new { message = result.Message, status = result.Status });
        }

        [HttpGet]
        public async Task<IActionResult>GetCalaenderData()
        {
            var result= await _corporate.GetCalenderVisitScheduleAsync();
            return Json(result);

        }

        [HttpGet]
        public async Task<IActionResult> VisitLog()
        {
            
            return View();

        }
    }
}
