using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IMenuSet
    {
        Task<List<MenuDto>> getMenuList(int UserId);
        Task<(string message, bool status)> SaveRoleWisePagePer(RolePermissionDto model);
    }
}
