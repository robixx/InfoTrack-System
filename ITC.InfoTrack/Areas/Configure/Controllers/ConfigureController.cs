using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITC.InfoTrack.Areas.Configure.Controllers
{
    [Area("Configure")]
    [Authorize]
    public class ConfigureController : Controller
    {
        private readonly IDropDown _dropdown;
        private readonly IOrgLocation _orglocation;
        private readonly IConfigurations _configuration;
        public ConfigureController(IDropDown dropdown, IOrgLocation orgLocation, IConfigurations configuration)
        {
            _dropdown = dropdown;
            _orglocation = orgLocation;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult OrganizationCreate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrganizationConfigure()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetOrganizationTree()
        {

            var allOrganization = await _configuration.GetOrganizationTreeAsync();
            var response = new
            {
                success = true,
                message = "Organization tree fetched successfully",
                data = allOrganization
            };

            return Json(response);

        }


        [HttpGet]
        public async Task<IActionResult> GetDynamicOrganizationName(string id)
        {
            var result= await _dropdown.getDynamicNameIdAsync(id);
            var data = result.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTreeNode([FromBody] TreeNodeInsertDto newNodeData)
        {
            var result= await _configuration.TreeNodeModificationAsync(newNodeData);
            var response = new
            {
                status = result.status,
                message = result.message,
                
            };
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> LocationSet()
        {
            var result= await _orglocation.getOrgLocation();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetCustomLocation(long locationTypeId, long orgId)
        {
            var group = await _dropdown.GetCustomTypeWiseLocation(orgId, locationTypeId );
            var data = group.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> OrgLocationSave([FromBody] InsertOrgDto model)
        {
            var result= await _orglocation.SaveOrgLocation(model);
            return Json(new { message = result.message, status =result.status });
        }

        [HttpGet]
        public async Task<IActionResult> GetCoreLocation()
        {
            var group = await _dropdown.GetOrganization();
            var g = group.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            var parent = await _dropdown.GetOrganizationLocation();
            var gv = parent.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g, parent = gv });
        }


        [HttpGet]
        public async Task<IActionResult> Resource()
        {

            ViewBag.booth = new SelectList(await _dropdown.getBoothName(), "Id", "Name");
            ViewBag.Address = new SelectList(await _dropdown.getAddressName(), "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Resource(int boothId, int locationid)
        {
            if (boothId == 0 || locationid == 0)
            {
                TempData["Error"] = "Please select both booth and location.";
                return RedirectToAction("VisitLog"); // your view
            }

            var result=await _orglocation.loactionmapping(boothId, locationid);
            // Save to database
            // Example: _context.BoothLocations.Add(new BoothLocation { BoothId = boothId, LocationId = locationid });
            // await _context.SaveChangesAsync();

            TempData["Success"] = "Data saved successfully!";
            return RedirectToAction("Resource"); // reload or redirect
        }

    }
}
