using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace ITC.InfoTrack.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class ConfigurationController : Controller
    {

        private readonly IDropDown _dropDown;
        private readonly IConfigurations _configurations;

        public ConfigurationController(IDropDown dropDown, IConfigurations configurations)
        {
            _dropDown = dropDown;
            _configurations = configurations;
        }



        [HttpGet]
        public IActionResult OrganizationSet()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetLoadType()
        {
            var group = await _dropDown.GetLevelSetting();
            var g = group.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });

            return Json(new { group = g });
        }

        [HttpGet]
        public async Task<IActionResult> GetTypeRootData()
        {
            var result = await _dropDown.getTypePropertyElement();
            var g = result.data.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g, status = result.status });
        }


        [HttpGet]
        public async Task<IActionResult> GetLoadRootData(int type)
        {
            var result = await _dropDown.getRootPropertyElement(type);
            var g = result.data.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> GetLoadChildenData(int type)
        {
            var result = await _dropDown.getChildenPropertyElement(type);
            var g = result.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g });
        }


        [HttpPost]
        public async Task<IActionResult> ConfigurationSave([FromBody] ConfigureInsertDto newItem)
        {
            var result = await _configurations.SaveOrganizationConficData(newItem);

            return Json(new { message = result.message, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> GetConfigurationData()
        {
            var result = await _configurations.GetOrganizationConficData();

            return Json(new { result = result });
        }

        [HttpPost]
        public async Task<IActionResult> ConfigurationDataDelete([FromBody] DeleteRequestDto dto)
        {


            var result = await _configurations.DeleteOrganizationConficData(dto.Id);

            return Json(new { message = result.message, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> ProfileConfiguration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLoadLevelRootData(int type)
        {
            var result = await _dropDown.getLevelRootPropertyElement(type);
            var g = result.data.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> Gethierarchy()
        {

            var results = await _configurations.GetHierarchyNodeAsync();

            return Json(results);
        }



        [HttpGet]
        public async Task<IActionResult> GettypeHierarchyData()
        {
            var results = await _configurations.GetTypeHierarchyAsync();
            return Json(results);
        }

        [HttpPost]
        public async Task<IActionResult> SaveLevelLoadRootData([FromBody] LevelDataDto data)
        {

            if (data == null) return BadRequest();
            var result = await _configurations.SaveLevelData(data);

            return Json(new { message = result.message, status = result.status });
        }


        public class DeleteRequestDto
        {
            public int Id { get; set; }
        }




    }
}
