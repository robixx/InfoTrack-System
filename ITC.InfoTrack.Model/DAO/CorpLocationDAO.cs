using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.DAO
{
    public class CorpLocationDAO : IOrgLocation
    {
        private readonly DatabaseConnection _connection;
        public CorpLocationDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<LocationShowDto>> getOrgLocation()
        {
            try
            {

                

                var result = await (from ol in _connection.OrgLocation
                                    join og in _connection.Organization on ol.OrgId equals og.OrgId
                                    join md in _connection.MetaDataElements on ol.LocationTypeId equals md.DataElementId
                                    join b in _connection.OfficeBranch
                                    on new { AssignPropertyId =(long) ol.AssignPropertyId, OrgId = (long)ol.OrgId }
                                    equals new { AssignPropertyId = b.BranchId, OrgId = b.CorpId }
                                    select new LocationShowDto
                                    {
                                        LocationId = ol.LocationId,
                                        Organization = og.OrgName,
                                        LocationAddress = ol.LocationAddress,
                                        IsActive = ol.LocationStatus,
                                        CreateDate = ol.InsertDate,
                                        LocationTypeName = md.MetaElementValue,
                                        BranchName=b.BranchName
                                    }).OrderBy(i=>i.LocationId)
                                    .ToListAsync();
                return result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        public async Task<(string message, bool status)> SaveOrgLocation(InsertOrgDto model)
        {
            try
            {
                if (model == null)
                {
                    return ("Location Data not Valid", false);
                }
                var savedata = new OrgLocation
                {
                    OrgId = model.orgId,
                    LocationAddress = model.orgaddress,
                    LocationName = model.orgaddress,
                    LocationTypeId = model.locationType,
                    AssignPropertyId = model.customId,
                    InsertBy = 1,
                    LocationStatus = model.isActive==true?1:0,
                    InsertDate = DateTime.Now,
                };
                _connection.OrgLocation.Add(savedata);
                await _connection.SaveChangesAsync();
                return ("Location Save Successfully !..", true);

            }
            catch (Exception ex)
            {
                return (ex.InnerException.Message, false);
            }

        }


        public async Task<(bool status, string message)> loactionmapping(int bootid, int locationid)
        {
            try
            {
                var Location = new LocationMapping
                {
                    LocationId = locationid,
                    TypeId = bootid,
                    LocationType = 7,
                    IsActive = 1
                };
                await _connection.LocationMapping.AddRangeAsync(Location);
                await _connection.SaveChangesAsync();

                return (true, "Success");

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
