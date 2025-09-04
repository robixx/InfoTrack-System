using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Areas.Security.Controllers
{
    [Area("Security")]
    [Authorize]
    public class DataMappingController : Controller
    {


        private readonly IDropDown _dropdown;
        private readonly IWorker _worker;

        public DataMappingController(IDropDown dropdown, IWorker worker)
        {
            _dropdown = dropdown;
            _worker = worker;
        }

        [HttpGet]
        public async Task<IActionResult> BranchMapping()
        {
            ViewBag.division = new SelectList(await _dropdown.getDivision(), "Id", "Name");

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> getallDataElementData(int id) 
        {
            var result = await _worker.getDataMapAsync(id);

            return Json(new {data=result});
        }

        [HttpPost]
        public async Task<IActionResult> SaveDataMapping( [FromForm] int DivisionId, [FromForm] List<DataMappingDto> SelectedItems)
        {
            // Parse JSON array from SelectedItems
           var result= await _worker.SaveDataMapAsync(DivisionId, SelectedItems);

            return Json(new { status = result.status, message = result.message });
        }
    }
}
