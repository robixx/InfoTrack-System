using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class RoleDAO : IRole
    {
        private readonly DatabaseConnection _connection;
        public RoleDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<RoleDto>> GetRole()
        {
            try
            {

                var chekdata = await _connection.Role
                  .Select(i => new RoleDto
                  {
                      RoleId = i.RoleId,
                      RoleName = i.RoleName,
                      Description = i.Description,
                      IsActive = i.IsActive,
                      ViewOrder = i.ViewOrder,
                  })
                  .OrderBy(i => i.ViewOrder)
                  .ToListAsync();


                return chekdata;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> RolePermissionSave(InsertUserRoleDto insertUserRoleDtos)
        {
            try
            {
                if (insertUserRoleDtos == null)
                {
                    return ("No data provided", false);
                }
                var userId = insertUserRoleDtos.userId;
                var existingRoles = await _connection.RolePermission
                    .Where(rp => rp.UserId == userId)
                    .ToListAsync();

                if (existingRoles.Any())
                {
                    _connection.RolePermission.RemoveRange(existingRoles);
                }

                var newRoles = new RolePermission
                {
                    UserId = insertUserRoleDtos.userId,
                    RoleId = insertUserRoleDtos.roleId,
                    IsActive = insertUserRoleDtos.isActive
                };


                await _connection.RolePermission.AddRangeAsync(newRoles);
                await _connection.SaveChangesAsync();

                return ("Role permissions updated successfully", true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> RoleValueCreate(RoleDto role)
        {
            try
            {
                string message = "";
                bool status = false;
                if (role == null)
                {
                    return ("Role Insert Faild", false);

                }
                if (role.RoleId > 0)
                {
                    var chekdata = await _connection.Role
                        .Where(i => i.RoleId == role.RoleId)
                        .FirstOrDefaultAsync();
                    if (chekdata != null)
                    {
                        chekdata.RoleName = role.RoleName;
                        chekdata.Description = role.Description;
                        chekdata.IsActive = role.IsActive;
                        chekdata.ViewOrder = role.ViewOrder;
                        chekdata.UpdatedAt = DateTime.Now.Date;
                        int result = await _connection.SaveChangesAsync();
                    }
                    return ("Role Update Successfully", true);

                }
                else
                {
                    var avoidDuplicate = await _connection.Role
                        .Where(i => i.RoleId == role.RoleId)
                        .FirstOrDefaultAsync();
                    if (avoidDuplicate != null)
                    {
                        return ("Already Role Name has been taken.", false);
                    }
                    var meta = new Role
                    {
                        RoleName = role.RoleName,
                        Description = role.Description,
                        IsActive = role.IsActive,
                        CreatedAt = DateTime.Now,
                        ViewOrder = role.ViewOrder,

                    };
                    await _connection.Role.AddRangeAsync(meta);
                    await _connection.SaveChangesAsync();
                    message = "Role Save Successfully";
                    status = true;
                }

                return (message, status);
            }
            catch (Exception ex)
            {
                return ($"Error: {ex.Message}", false);
            }
        }

        public async Task<(List<RoleBaseMainMenuDto> data, bool status)> RoleWisePagePermission(int userid)
        {
            try
            {
                List<RoleBaseMainMenuDto> Menulist = new List<RoleBaseMainMenuDto>();
                var result = await _connection.RoleBaseSubMenuDto
                   .FromSqlRaw($"SELECT * FROM \"get_rolewise_page_Permission\"({userid})")
                   .ToListAsync();
                if (result != null)
                {
                    var menlist = result.Where(i => i.ParentId == 0).OrderBy(i => i.ViewOrder).ToList();
                    foreach (var item in menlist)
                    {
                        var sub = result.Where(i => i.ParentId == item.MenuId).OrderBy(i => i.ViewOrder).ToList();
                        List<RoleBaseSubMenuDto> submenu = new List<RoleBaseSubMenuDto>();
                        foreach (var item2 in sub)
                        {
                            submenu.Add(new RoleBaseSubMenuDto
                            {
                                MenuId = item2.MenuId,
                                MenuName = item2.MenuName,
                                ParentId = item2.ParentId,
                                IsAllowed = item2.IsAllowed,
                                ViewOrder = item2.ViewOrder,
                            });
                        }


                        Menulist.Add(new RoleBaseMainMenuDto
                        {
                            MenuId = item.MenuId,
                            MenuName = item.MenuName,
                            ParentId = item.ParentId,
                            IsAllowed = item.IsAllowed,
                            ViewOrder = item.ViewOrder,
                            roelbasesubMenu = submenu

                        });
                    }

                }
                return (Menulist, true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<ShowRolePageDto> data, bool status)> UserWiseRolePermission(int userid)
        {
            try
            {
                 List<ShowRolePageDto> dat_result= new List<ShowRolePageDto>();
                var roleInfo = await _connection.UserWiseRoleShowDto.FromSqlInterpolated($"select * from get_User_Wise_Role_Permission({userid})").ToListAsync();

                if (roleInfo == null)
                    return (null, false);

                foreach(var releIn in roleInfo)
                {
                    var menuNames = await (
                    from pagePermission in _connection.RoleBasePagePermission
                    join menu in _connection.MenuSetUp
                        on pagePermission.MenuId equals menu.MenuId
                    where pagePermission.RoleId == releIn.RoleId
                    select menu.MenuName
                    ).Distinct().ToListAsync();


                    var result = new ShowRolePageDto
                    {
                        UserId = releIn.UserId,
                        RoleId = releIn.RoleId,
                        RoleName = releIn.RoleName,
                        IsActive = releIn.IsActive,
                        ViewOrder = releIn.ViewOrder,
                        Description = releIn.Description,
                        MenuData= menuNames,
                    };

                    dat_result.Add(result);
                }              

                return (dat_result, true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
