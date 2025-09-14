using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Threading.Tasks;

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
        private readonly IUser _user;
        private readonly string _imagePath;
        public SecurityController(IMetaData metadata, IDropDown dropdown, IRole role, IMenuSet menuSet, IUser user, IConfiguration configuration)
        {
            _metadata = metadata;
            _dropdown = dropdown;
            _role = role;
            _menuSet = menuSet;
            _user = user;
            _imagePath = configuration["ImageStorage:TokenImagePath"]; 
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
        public IActionResult OrganizationRootSet()
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
            var result = await _user.getUserList();
            foreach (var product in result)
            {
                if (product.ImageUrl != null && product.ImageUrl.Any())
                {
                    product.ImageUrl = $"/getimages/{product.ImageUrl}";
                }
                else
                {
                    // Optional: default image if none uploaded
                    product.ImageUrl = "/images/default.png";
                }
            }
            return View(result);
        }


        [HttpGet("getimages/{fileName:regex(.+)}")]
        public IActionResult Shows(string fileName)
        {
            string fullPath;

            if (string.IsNullOrEmpty(fileName))
            {
                // Use default image if filename is null or empty
                fullPath = Path.Combine(_imagePath, "default.png");
            }
            else
            {
                fullPath = Path.Combine(_imagePath, fileName);
                // If file doesn't exist, use default image
                if (!System.IO.File.Exists(fullPath))
                {
                    fullPath = Path.Combine(_imagePath, "default.png");
                }
            }

            if (!System.IO.File.Exists(fullPath))
                return NotFound(); // in case even default image is missing

            var fileExt = Path.GetExtension(fullPath).ToLower();
            var contentType = fileExt switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            var bytes = System.IO.File.ReadAllBytes(fullPath);
            return File(bytes, contentType);
        }




        [HttpPost]
        public async Task<IActionResult> UserCreate([FromForm] CreateUserDto dto)
        {
            var result= await _user.saveUserData(dto);

            return Json(new { message= result.message, status=result.status});
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



        [HttpGet]
        public async Task<IActionResult> UserProfile(int loginId=0)
        {
            if (loginId == 0)
            {
                loginId = Convert.ToInt32(User.FindFirst("UserId")?.Value ?? "0");
            }
            

            var result_value = await _user.getUserList();
            var result = result_value.FirstOrDefault(i => i.UserId == loginId);
            
          
                if (result.ImageUrl != null && result.ImageUrl.Any())
                {
                result.ImageUrl = $"/getimages/{result.ImageUrl}";
                }
                else
                {
                // Optional: default image if none uploaded
                result.ImageUrl = "/images/default.png";
                }
           
            return View(result);
        }
    }
}
