using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class ConfigurationDAO : IConfigurations
    {
        private readonly DatabaseConnection _connection;
        public ConfigurationDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<(string message, bool status)> DeleteOrganizationConficData(int id)
        {
            try
            {
                var isdelete = await _connection.OrganizationConfigure
                      .FirstOrDefaultAsync(i => i.Id == id);

                if (isdelete == null)
                {
                    return ("Data not found.", false);
                }

                _connection.OrganizationConfigure.Remove(isdelete);
                await _connection.SaveChangesAsync();

                return ("Data deleted successfully.", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Dictionary<long, int?>> GetHierarchyAsync()
        {
            try
            {
                return await _connection.LevelSetting
                        .Where(l => l.IsActive == 1)
                        .ToDictionaryAsync(l => l.LevelId, l => (int?)l.ParentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<HierarchyNodeDto>> GetHierarchyNodeAsync()
        {
            try
            {
                var flatList = await (from l in _connection.LevelSetting
                                      join h in _connection.MetaDataType
                                      on l.PropertyId equals h.PropertyId
                                      select new HierarchyNodeDto
                                      {
                                          Id = l.LevelId,         
                                          Name = l.LevelName,
                                          Type = h.PropertyName,                                         
                                          ParentId = l.ParentId,
                                          PropertyId= l.PropertyId,
                                      }).ToListAsync();
                var hierarchyTree = BuildTree(flatList);

                return hierarchyTree;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<HierarchyNodeDto> BuildTree(List<HierarchyNodeDto> nodes, int parentId = 0)
        {
            int proerid = parentId;
            return nodes
                .Where(n => n.ParentId == proerid)
                .Select(n => new HierarchyNodeDto
                {
                    Id = n.Id,
                    Name = n.Type,
                    Type = n.Name,                   
                    ParentId = n.ParentId,
                    Children = BuildTree(nodes, n.PropertyId)
                })
                .ToList();
        }

        public async Task<List<OrganizationConfigureDto>> GetOrganizationConficData()
        {
            try
            {
                var result = await (from o in _connection.OrganizationConfigure
                                    join n in _connection.MetaDataElements on
                                    new { basedata = o.SetupId, maindata = o.NameId }
                                    equals new { basedata = n.PropertyId, maindata = n.DataElementId }
                                    select new OrganizationConfigureDto
                                    {
                                        Id = o.Id,
                                        SetupId = o.SetupId,
                                        Name = n.MetaElementValue,
                                        Type = o.Type,
                                        Email = o.Email,
                                        Phone = o.Phone,
                                        Level = o.Level,
                                        ParentId = o.ParentId,
                                        Address = o.Address,

                                    }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(string message, bool status)> SaveLevelData(LevelDataDto model)
        {
            try
            {
                if (model == null)
                    return ("Model Not Valid", false);
                var isDuplicateCheck = await _connection.LevelSetting
                    .Where(i=>i.ParentId==model.ParentId && i.PropertyId==model.TypeId).CountAsync();
                if (isDuplicateCheck > 0)
                {
                    return ($"{model.typeName} Already Created ", false);
                }

                var result = new LevelSetting
                {
                    LevelName = model.typeName,
                    PropertyId = model.TypeId,
                    ParentId = model.ParentId,
                    OrderView = model.OrderView,
                    IsActive = 1,

                };
                await  _connection.LevelSetting.AddAsync(result);
                await _connection.SaveChangesAsync();
                return ($"{model.typeName} create Successfully", true);

            }catch(Exception ex)
            {
                return (ex.InnerException.Message, false);
            }
        }

        public async Task<(string message, bool status)> SaveOrganizationConficData(ConfigureInsertDto model)
        {
            try
            {
                if (model == null)
                {

                    return ("Model Not Valid", false);

                }

                var checkDuplicate = await _connection.OrganizationConfigure
                   .Where(p => p.SetupId == model.SetupId && p.ParentId == model.ParentId && p.NameId == model.NameId).FirstOrDefaultAsync();
                if (checkDuplicate != null)
                {
                    return ($"{model.Type} Already Create", false);
                }
                var checkParentid = await GetParentId(model.SetupId, model.Type, model.NameId);
                if (checkParentid.parentid == -1)
                {
                    return ("Model Not Valid", false);
                }

                var inserdata = new OrganizationConfigure
                {
                    SetupId = model.SetupId,
                    Type = model.Type,
                    ParentId = checkParentid.parentid == 0 ? null : checkParentid.parentid,
                    Level = checkParentid.level,
                    Phone = model.Phone,
                    Email = model.Email,
                    NameId = model.NameId,
                    Address = ""
                };
                _connection.OrganizationConfigure.Add(inserdata);
                await _connection.SaveChangesAsync();
                return ("Configure success", true);

            }
            catch (Exception ex)
            {
                return ($"Error" + ex.InnerException.Message, false);
            }
        }


        private async Task<(int parentid, int level)> GetParentId(int id, string type, int nameid)
        {

            switch (type.ToLower())
            {
                case "organization":

                    return (0, 0); // Organization is the parent

                case "branch":
                    var branchparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == nameid && b.PropertyId == id)
                        .Select(b => b.ParentPropertyId)
                        .FirstOrDefaultAsync();
                    var tbranchparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == branchparentid)
                        .Select(b => new { b.PropertyId, b.DataElementId })
                        .FirstOrDefaultAsync();
                    var bdata = await _connection.OrganizationConfigure
                        .Where(i => i.SetupId == tbranchparentid.PropertyId && i.NameId == tbranchparentid.DataElementId)
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync();
                    return (bdata, 1);

                case "subbranch":

                    var subbranchparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == nameid && b.PropertyId == id)
                        .Select(b => b.ParentPropertyId)
                        .FirstOrDefaultAsync();
                    var tsbranchparentid = await _connection.MetaDataElements
                       .Where(b => b.DataElementId == subbranchparentid)
                       .Select(b => new { b.PropertyId, b.DataElementId })
                       .FirstOrDefaultAsync();
                    var sbdata = await _connection.OrganizationConfigure
                        .Where(i => i.SetupId == tsbranchparentid.PropertyId && i.NameId == tsbranchparentid.DataElementId)
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync();

                    return (sbdata, 2); // Booth is the parent

                case "district":
                    var districtparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == nameid && b.PropertyId == id)
                        .Select(b => b.ParentPropertyId)
                        .FirstOrDefaultAsync();
                    var subDistrictparentid = await _connection.MetaDataElements
                       .Where(b => b.DataElementId == districtparentid)
                       .Select(b => new { b.PropertyId, b.DataElementId })
                       .FirstOrDefaultAsync();
                    var dis = await _connection.OrganizationConfigure
                        .Where(i => i.SetupId == subDistrictparentid.PropertyId && i.NameId == subDistrictparentid.DataElementId)
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync();
                    return (dis, 3);

                case "division":
                    var divisionparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == nameid && b.PropertyId == id)
                        .Select(b => b.ParentPropertyId)
                        .FirstOrDefaultAsync();
                    var subdivisionparentid = await _connection.MetaDataElements
                       .Where(b => b.DataElementId == divisionparentid)
                       .Select(b => new { b.PropertyId, b.DataElementId })
                       .FirstOrDefaultAsync();
                    var divi = await _connection.OrganizationConfigure
                        .Where(i => i.SetupId == subdivisionparentid.PropertyId && i.NameId == subdivisionparentid.DataElementId)
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync();
                    return (divi, 4);

                case "booth":

                    var bobranchparentid = await _connection.MetaDataElements
                        .Where(b => b.DataElementId == nameid && b.PropertyId == id)
                        .Select(b => b.ParentPropertyId)
                        .FirstOrDefaultAsync();
                    var tobranchparentid = await _connection.MetaDataElements
                       .Where(b => b.DataElementId == bobranchparentid)
                       .Select(b => new { b.PropertyId, b.DataElementId })
                       .FirstOrDefaultAsync();
                    var bobdata = await _connection.OrganizationConfigure
                        .Where(i => i.SetupId == tobranchparentid.PropertyId && i.NameId == tobranchparentid.DataElementId)
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync();
                    return (bobdata, 5); // Booth is the parent

                default:
                    return (-1, -1);
            }




        }

        public async Task<Dictionary<string, string>> GetTypeHierarchyAsync()
        {
            try
            {
                var list = await _connection.LevelSetting.ToListAsync();

                var hierarchy = list.ToDictionary(
                    t => t.PropertyId.ToString(),
                    t => t.ParentId == 0 ? null : t.ParentId?.ToString()
                );

                return hierarchy;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<OrgNodeDto>> GetOrganizationTreeAsync()
        {
            try
            {
                var allOrganizations = await _connection.ProfileWiseOrganization
                   .OrderBy(x => x.OrderView)
                   .Select(x => new OrgNodeDto
                   {
                       Id = x.Id,
                       LevelName = x.LevelName,
                       ParentId = x.ParentId,
                       IsActive = x.IsActive,
                       OrderView = x.OrderView,
                       Status = x.Status,
                       PropertyId = x.PropertyId,
                       Icon = x.Icon,
                       Type=x.type,
                       Children = new List<OrgNodeDto>()
                   })
                   .ToListAsync();

                var tree = BuildTree(allOrganizations);
                return tree;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private List<OrgNodeDto> BuildTree(List<OrgNodeDto> allItems)
        {
            // Create lookup dictionary for quick access
            var lookup = allItems.ToDictionary(x => x.Id, x => x);

            List<OrgNodeDto> rootItems = new();

            foreach (var item in allItems)
            {
                if (item.ParentId.HasValue && lookup.TryGetValue(item.ParentId.Value, out var parent))
                {
                    // Add as child
                    parent.Children ??= new List<OrgNodeDto>();
                    parent.Children.Add(item);
                }
                else
                {
                    // No parent found → treat as root
                    rootItems.Add(item);
                }
            }

            return rootItems;
        }


    }
}
