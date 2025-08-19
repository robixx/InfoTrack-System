using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class DropDownDAO : IDropDown
    {
        private readonly DatabaseConnection _connection;
        public DropDownDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<DropDownDtos>> getAssetList(int BoothId)
        {
            try
            {
                var result = await _connection.BoothAsset
                    .Where(i => i.BoothId == BoothId && i.TerminalStatus == 1)
                    .Select(i => new DropDownDtos
                    {
                        Id = i.AssetId,
                        Name = i.TerminalName,
                    })
                    .ToListAsync();

                return result.OrderBy(i => i.Id).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDtos>> getboothList(long BranchId)
        {
            try
            {
                var result = await _connection.OfficeBooth
                    .Where(i => i.BranchId == BranchId && i.BoothStatus == 1)
                    .Select(i => new DropDownDtos
                    {
                        Id = i.BoothId,
                        Name = i.BoothName,
                    })
                    .ToListAsync();

                return result.OrderBy(i => i.Id).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDto>> getBranchList(int BranchId)
        {
            try
            {
                var branch = await _connection.ProfileWiseOrganization
                        .Where(i => i.PropertyId == BranchId ) // handle nullable input
                        .Select(i=>i.Id)
                        .FirstOrDefaultAsync(); // fetch first match or null

                var subbranchulist = await _connection.ProfileWiseOrganization
                    .Where(i => i.ParentId == branch)
                    .OrderBy(i=>i.OrderView)
                    .Select(i=>new DropDownDto
                    {
                        Id=i.Id,
                        Name=i.LevelName,
                    })
                    .ToListAsync();

                return subbranchulist;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDtos>> getChildenPropertyElement(int type)
        {
            try
            {
                var parentElements = await _connection.MetaDataElements
                             .Where(p => p.DataElementStatus == 1 && p.ParentPropertyId == type)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.DataElementId,
                                 Name = i.MetaElementValue
                             })
                             .ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find {ex.Message}");
            }
        }


        public async Task<List<DropDownDto>> GetCustomTypeWiseLocation(long OrgId, long TypeId)
        {
            try
            {
                var parameters = new[]
                {
                    new NpgsqlParameter("p_orgid", SqlDbType.BigInt) { Value = OrgId },
                    new NpgsqlParameter("p_typeid", SqlDbType.BigInt) { Value = TypeId }


                };
                var data = await _connection.CustomTypeWiseLoadedDto.FromSqlRaw("SELECT * FROM \"CustomType_wise_Loaded\"({0}, {1})", parameters)

                    .ToListAsync();

                var result = data.Select(i => new DropDownDto
                {
                    Id = i.Id,
                    Name = i.Name,
                }).ToList();

                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDtos>> GetMetaGroupTitle()
        {
            try
            {
                var parentElements = await _connection.MetaDataType
                             .Where(p => p.PropertyStatus == 1)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.PropertyId,
                                 Name = i.PropertyName
                             })
                             .Distinct()
                             .ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find {ex.Message}");
            }
        }

        public async Task<List<DropDownDtos>> GetMetaParentTitle()
        {
            try
            {
                var parentElements = await (from a in _connection.MetaDataElements
                                            join b in _connection.MetaDataType on a.PropertyId equals b.PropertyId
                                            where a.DataElementStatus == 1
                                            select new DropDownDtos
                                            {
                                                Id = a.DataElementId,
                                                Name = a.MetaElementValue + " ( " + b.PropertyName + " )",
                                            })
                             .ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find {ex.Message}");
            }
        }

        public async Task<List<DropDownDto>> getConfigBranchList()
        {
            try
            {
                var result = await _connection.ProfileWiseOrganization
                    .Where(i => i.IsActive == 1 && i.ParentId == 1)  // all branch
                    .Select(i => new DropDownDto
                    {
                        Id = i.PropertyId,
                        Name = i.LevelName
                    })
                    .ToListAsync();
                result.Add(new DropDownDto { Id = 0, Name = "Select Branch" });
                return result.OrderBy(i => i.Id).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<DropDownDtos>> GetLevelSetting()
        {
            try
            {

                var parentElements = await (
                                    from a in _connection.LevelSetting
                                    join v in _connection.MetaDataType on a.PropertyId equals v.PropertyId
                                    where a.IsActive == 1
                                    orderby a.OrderView
                                    select new DropDownDtos
                                    {
                                        Id = a.PropertyId,
                                        Name = v.PropertyName
                                    }
                                ).ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find {ex.Message}");
            }
        }
        public async Task<List<DropDownDtos>> GetOrganization()
        {
            try
            {

                var parentElements = await _connection.MetaDataType
                             .Where(p => p.PropertyStatus == 1 && p.RootValue == 1).OrderBy(i => i.Label)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.PropertyId,
                                 Name = i.PropertyName
                             })
                             .ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find {ex.Message}");
            }
        }

        public async Task<List<DropDownDtos>> GetOrganizationLocation()
        {
            try
            {
                var parentElements = await _connection.MetaDataElements
                             .Where(p => p.PropertyId == 3 && p.DataElementStatus == 1)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.DataElementId,
                                 Name = i.MetaElementValue
                             })
                             .ToListAsync();

                return parentElements;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDto>> GetRole()
        {
            try
            {
                var result = await _connection.Role
                    .Where(i => i.IsActive == true)
                    .Select(i => new DropDownDto
                    {
                        Id = i.RoleId,
                        Name = i.RoleName
                    })
                    .ToListAsync();
                result.Add(new DropDownDto { Id = 0, Name = "---Select RoleName---" });
                return result.OrderBy(i => i.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<DropDownDtos> data, bool status)> getRootMetaPropertyElement()
        {
            try
            {
                var parentElements = await _connection.MetaDataType
                             .Where(p => p.RootValue == 1)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.PropertyId,
                                 Name = i.PropertyName
                             })
                             .ToListAsync();

                return (parentElements, true);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<DropDownDtos> data, bool status)> getRootPropertyElement(int type)
        {
            try
            {
                var parentElements = await _connection.MetaDataElements
                             .Where(p => p.PropertyId == type && p.DataElementStatus == 1)
                             .Select(i => new DropDownDtos
                             {
                                 Id = i.DataElementId,
                                 Name = i.MetaElementValue
                             })
                             .ToListAsync();

                return (parentElements, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<DropDownDto> data, bool status)> getLevelRootPropertyElement(int type)
        {
            try
            {
                var parentElements = await _connection.LevelSetting
                             .Where(p => p.PropertyId == type && p.Status == 1)
                             .Select(i => new DropDownDto
                             {
                                 Id = i.LevelId,
                                 Name = i.LevelName
                             })
                             .ToListAsync();

                return (parentElements, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDto>> GetUser()
        {
            try
            {
                var result = await _connection.Users
                    .Where(i => i.UserStatus == 1)
                    .Select(i => new DropDownDto
                    {
                        Id = i.UserId,
                        Name = i.UserName
                    })
                    .ToListAsync();
                result.Add(new DropDownDto { Id = 0, Name = "---Select User---" });
                return result.OrderBy(i => i.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(List<DropDownDto> data, bool status)> getTypePropertyElement()
        {
            try
            {
                var parentElements = await (
                                    from b in _connection.LevelSetting
                                    join d in _connection.MetaDataType
                                        on b.PropertyId equals d.PropertyId
                                    where b.IsActive == 1 // <-- Add your actual condition here
                                    select new DropDownDto
                                    {
                                        Id = b.PropertyId,
                                        Name = d.PropertyName
                                    }
                                ).ToListAsync();
                return (parentElements, true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDtos>> getDynamicNameIdAsync(string id)
        {
            try
            {
                int parentId = Convert.ToInt32(id);

              
                // Step 1: get the first propertyId
                var acualvalue = await _connection.ProfileWiseOrganization
                    .Where(i => i.IsActive == 1 && i.ParentId == parentId)
                    .Select(p => p.PropertyId)
                    .FirstOrDefaultAsync();               

                // Step 2: get all propertyIds that should be excluded
                var excludedIds = await _connection.ProfileWiseOrganization
                    .Where(i => i.IsActive == 1 && i.PropertyId!= acualvalue)
                    .Select(p => p.PropertyId)
                    .ToListAsync();

                // Step 3: fetch MetaDataElements for acualvalue but not already in excludedIds
                var datalist = await _connection.MetaDataElements
                         .Where(i => i.PropertyId == acualvalue && !excludedIds.Contains(i.DataElementId))
                         .Select(d => new DropDownDtos
                         {
                             Id = d.DataElementId,
                             Name = d.MetaElementValue
                         })
                         .ToListAsync();




                return datalist;

            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DropDownDto>> getDistrictList(int subbranchId)
        {
            try
            {
                var branch = await _connection.ProfileWiseOrganization
                        .Where(i => i.Id == subbranchId) // handle nullable input
                        .Select(i => i.Id)
                        .FirstOrDefaultAsync(); // fetch first match or null

                var subbranchulist = await _connection.ProfileWiseOrganization
                    .Where(i => i.ParentId == branch)
                    .OrderBy(i => i.OrderView)
                    .Select(i => new DropDownDto
                    {
                        Id = i.Id,
                        Name = i.LevelName,
                    })
                    .ToListAsync();

                return subbranchulist;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
