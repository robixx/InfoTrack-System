using ITC.InfoTrack.Model.ViewModel;
using ITC.InfoTrack.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IRole
    {
        Task<(string message, bool status)>RoleValueCreate(RoleDto role);
        Task<List<RoleDto>>GetRole();
        Task<(List<ShowRolePageDto> data, bool status)> UserWiseRolePermission(int userid);
        Task<(List<RoleBaseMainMenuDto> data, bool status)> RoleWisePagePermission(int userid);
        Task<(string message, bool status)> RolePermissionSave(InsertUserRoleDto insertUserRoleDtos);
    }
}
