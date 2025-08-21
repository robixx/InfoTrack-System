using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace ITC.InfoTrack.Areas.Security.Controllers
{
    [Area("Security")]
    [Authorize]
    public class SecurityController : Controller
    {
        private readonly IMetaData _metadata;
        private readonly IDropDown _dropdown;
        private readonly IRole _role;
        private readonly IMenuSet _menuSet;
        public SecurityController(IMetaData metadata, IDropDown dropdown, IRole role, IMenuSet menuSet)
        {
            _metadata = metadata;
            _dropdown = dropdown;
            _role = role;
            _menuSet = menuSet;
        }
        [HttpGet]
        public async Task<IActionResult> MetadataConfigure()
        {
            var metalist = await _metadata.getMetaElement();
            return View(metalist);
        }

        [HttpPost]
        public async Task<IActionResult> MetaElementSave([FromBody] MetaElementInsertDto model)
        {
            var result = await _metadata.SaveMetaElement(model);
            return Json(new { message = result.message, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> MetaElementEdit(int metaId)
        {
            var result = await _metadata.EditMetaElement(metaId);
            return Json(new { data = result.data, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> MetadataElement()
        {
            var result = await _metadata.getMetaPropertyElement();
            return View(result);
        }


        //Meata Element Property dropdown
        [HttpGet]
        public async Task<IActionResult> GetMetaElement()
        {
            var group = await _dropdown.GetMetaGroupTitle();
            var g = group.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            var parent = await _dropdown.GetMetaParentTitle();
            var gv = parent.Select(p => new
            {
                value = p.Id,
                text = p.Name,
            });
            return Json(new { group = g, parent = gv });
        }

        [HttpPost]
        public async Task<IActionResult> MetaElementPropertySave([FromBody] MetaDataPropertyElementInsertDto model)
        {
            var result = await _metadata.SaveMetaElementProperty(model);
            return Json(new { message = result.message, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> MetadataPropertyElementEdit(int dataelementid)
        {
            var result = await _metadata.EditMetaPropertyElement(dataelementid);
            return Json(new { data = result.data, status = result.status });
        }


        [HttpGet]
        public async Task<IActionResult> OrganizationRootSet()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMetaRootdata()
        {
            var result = await _dropdown.getRootMetaPropertyElement();
            return Json(new { group = result.data, status = result.status });
        }


        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RoleCreate()
        {
            var result= await _role.GetRole();
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> RoleSave([FromBody] RoleDto model)
        {
            var result= await _role.RoleValueCreate(model);
            return Json(new { message = result.message, status = result.status });
        }
        [HttpGet]
        public async Task<IActionResult> RoleManage()
        {
            ViewBag.user = new SelectList(await _dropdown.GetUser(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserRolePermission(int id)
        {
            var result = await _role.UserWiseRolePermission(id);
            return Json(new { data = result.data, status = result.status });
        }

        [HttpPost]
        public async Task<IActionResult> UserRolePermissionSave([FromBody] InsertUserRoleDto selectedRole)
        {
            var result= await _role.RolePermissionSave(selectedRole);
            return Json(new { message = result.message, status = result.status });
        }

        [HttpGet]
        public async Task<IActionResult> RolePermission()
        {
            ViewBag.role = new SelectList(await _dropdown.GetRole(), "Id", "Name");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> RolePagePermission(int id)
        {
            var result = await _role.RoleWisePagePermission(id);
            return Json(new { data = result.data, status = result.status });
        }

        [HttpPost]
        public async Task<IActionResult> SaveRolePagePermission([FromBody] RolePermissionDto model)
        {
            var result = await _menuSet.SaveRoleWisePagePer(model);
            return Json(new { message = result.message, status = result.status });
        }
    }
}
