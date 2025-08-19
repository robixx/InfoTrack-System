using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class RolePermissionDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<MenuPermissionDto> permissions { get; set; }
    }
    public class SubMenuPermissionDto
    {
        public string MenuId { get; set; }
        public bool IsAllowed { get; set; }
    }

    public class MenuPermissionDto
    {
        public string MenuId { get; set; }
        public bool IsAllowed { get; set; }
        public List<SubMenuPermissionDto> RolebaseSubMenu { get; set; }
    }
}
