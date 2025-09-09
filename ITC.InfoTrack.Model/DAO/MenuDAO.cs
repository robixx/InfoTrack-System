using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class MenuDAO : IMenuSet
    {
        private readonly DatabaseConnection _connection;
        public MenuDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<MenuDto>> getMenuList(int UserId)
        {
            try
            {
                List<MenuDto> Menulist = new List<MenuDto>();

                var parameters = new[]
                {
                    new NpgsqlParameter("p_userId", SqlDbType.Int) { Value = UserId },
                    

                };

                var list = await _connection.MenuSetUp
                           .FromSqlRaw("select * from  get_menulist({0})",parameters)
                           .ToListAsync();
                if (list != null)
                {
                    var menlist = list.Where(i => i.ParentId == 0).OrderBy(i => i.ViewOrder).ToList();
                    foreach (var item in menlist)
                    {
                        var sub = list.Where(i => i.ParentId == item.MenuId).OrderBy(i=>i.ViewOrder).ToList();
                        List<SubMenuDto> submenu = new List<SubMenuDto>();
                        foreach (var item2 in sub)
                        {
                            submenu.Add(new SubMenuDto
                            {
                                MenuId = item2.MenuId,
                                MenuName = item2.MenuName,
                                RouteName = item2.RouteName,
                                AreaName = item2.AreaName,
                                ControllerName = item2.ControllerName,
                                ActionName = item2.ActionName,
                                IsMainMenu = item2.IsMainMenu,
                                ParentId = item2.ParentId,
                                IsActive = item2.IsActive,
                                ViewOrder = item2.ViewOrder,
                            });
                        }


                        Menulist.Add(new MenuDto
                        {
                            MenuId = item.MenuId,
                            MenuName = item.MenuName,
                            ParentId = item.ParentId,
                            ViewOrder = item.ViewOrder,
                            subMenu = submenu

                        });
                    }

                }
                return Menulist;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> SaveRoleWisePagePer(RolePermissionDto model)
        {
            if(model==null || !model.permissions.Any())
            {
                return ($"{model.RoleName} Invalid or empty permission data.", false);
            }
            try
            {
                var checkduplicate = await _connection.RoleBasePagePermission
                    .Where(i => i.RoleId == model.RoleId)
                    .ToListAsync();
                if (checkduplicate.Any())
                {
                     _connection.RoleBasePagePermission.RemoveRange(checkduplicate);
                }
                var newPermissions = new List<RoleBasePagePermission>();

                foreach (var menu in model.permissions)
                {
                    // Add parent menu permission
                    if (menu.IsAllowed)
                    {
                        newPermissions.Add(new RoleBasePagePermission
                        {
                            MenuId = int.Parse(menu.MenuId),
                            RoleId = model.RoleId,
                            IsAllowed = 1
                        });
                    }

                    // Add submenus
                    foreach (var submenu in menu.RolebaseSubMenu)
                    {
                        if (submenu.IsAllowed)
                        {
                            newPermissions.Add(new RoleBasePagePermission
                            {
                                MenuId = int.Parse(submenu.MenuId),
                                RoleId = model.RoleId,
                                IsAllowed = 1
                            });
                        }
                    }
                }

                // 4. Save new entries
                await _connection.RoleBasePagePermission.AddRangeAsync(newPermissions);
                await _connection.SaveChangesAsync();

                return ($"{model.RoleName} Role Menu Permission Updated Successfully", true);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
