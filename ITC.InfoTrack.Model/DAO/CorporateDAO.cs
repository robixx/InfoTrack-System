using ITC.InfoTrack.Model.DataBase;
using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.Interface;
using ITC.InfoTrack.Model.ViewModel;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ITC.InfoTrack.Model.DAO
{
    public class CorporateDAO : ICorporate
    {
        private readonly DatabaseConnection _connection;
        public CorporateDAO(DatabaseConnection connection)
        {
            _connection = connection;
        }



        public async Task<List<CorporateOffice>> getOfficeList()
        {
            try
            {
                List<CorporateOffice> offlist = new List<CorporateOffice>();
                var result = await _connection.CorporateOffice
                            .Where(i => i.CorpStatus == 1)
                            .ToListAsync();
                if (result != null)
                {
                    offlist = result;
                }
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<(string Message, bool Status)> addOfficeInformation(CorporateOfficeDto model)
        {
            try
            {
                string message = "";
                bool status = false;
                if (model == null)
                {
                    return ("Invalid Model", false);

                }
                if (model.CorpId > 0)
                {
                    var find_value = await _connection.CorporateOffice
                       .Where(i => i.CorpId == model.CorpId && i.CorpStatus == 1)
                       .FirstAsync();

                    if (find_value != null)
                    {
                        find_value.CorpName = model.CorpName?.Trim();
                        find_value.CorpAddress = model.CorpAddress?.Trim();
                        find_value.CorpEmail = model.CorpEmail?.Trim();
                        find_value.CorpContactNumber = model.CorpContactNumber?.Trim();
                        find_value.CorpType = model.CorpType?.Trim();
                        find_value.UpdatedBy = 1;
                        find_value.UpdatedDate = DateTime.Now.Date;

                        // Optionally force update
                        //_connection.Entry(find_value).State = EntityState.Modified;

                        int result = await _connection.SaveChangesAsync();
                    }
                    message = "Office Update Successfully";
                    status = true;
                }
                else
                {
                    var checkDuplicate = await _connection.CorporateOffice
                       .Where(i => i.CorpName == model.CorpName.Trim() && i.CorpStatus == 1)
                       .FirstAsync();

                    if (checkDuplicate != null)
                    {
                        return ("Already Company Name has been taken.", false);
                    }

                    var data = new CorporateOffice
                    {
                        CorpName = model.CorpName,
                        CorpAddress = model.CorpAddress,
                        CorpContactNumber = model.CorpContactNumber,
                        CorpEmail = model.CorpEmail,
                        CorpStatus = 1,
                        CorpType = model.CorpType,
                        CreateDate = DateTime.Now.Date,
                        CreatedBy = 1,
                    };
                    await _connection.CorporateOffice.AddRangeAsync(data);
                    await _connection.SaveChangesAsync();
                    message = "Save Successfully";
                    status = true;

                }
                return (message, status);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CorporateOffice> OfficeDataFind(long corpId)
        {
            try
            {
                var result = await _connection.CorporateOffice.FirstOrDefaultAsync(i => i.CorpId == corpId);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ScheduleDataDto>> ScheduleDataFind(long? branchid, long? subbranch, int? district, int? division)
        {
            try
            {
                var parameters = new[]
                {
                    new NpgsqlParameter("p_corpId", SqlDbType.BigInt) { Value = branchid ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_branchId", SqlDbType.BigInt) { Value = subbranch ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_boothId", SqlDbType.Int) { Value = district ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_assetId", SqlDbType.Int) { Value = division ?? (object)DBNull.Value },

                };
                var data = await _connection.ScheduleDataDto.FromSqlRaw("SELECT * FROM get_schedule_data_set({0}, {1}, {2}, {3})", parameters).ToListAsync();

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserDto>> GetUserDateWise(string dateValue)
        {
            try
            {
                DateTime? date = Convert.ToDateTime(dateValue);
                var visitIds = await _connection.VisitScheduleDetails
                            .Where(e => e.CreateDate != date)
                            .Select(e => e.VisitId)
                            .ToListAsync();

                var userlist = await _connection.Users
                    .Where(i => !visitIds.Contains(i.UserId))
                    .Select(n=> new UserDto
                    {
                        UserId= n.UserId,
                        UserName= n.UserName,
                    })
                    .ToListAsync();

                return userlist;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<OrganizationHierarchyDto>> GetOrganizationHierarchyAsync(int? branchId, int? subbranch, int? district, int? division)
        {
            try
            {
                var parameters = new[]
               {
                    new NpgsqlParameter("p_branch_property_id", NpgsqlTypes.NpgsqlDbType.Integer)
                        { Value = branchId ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_subbranch_property_id", NpgsqlTypes.NpgsqlDbType.Integer)
                        { Value = subbranch ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_district_property_id", NpgsqlTypes.NpgsqlDbType.Integer)
                        { Value = district ?? (object)DBNull.Value },
                    new NpgsqlParameter("p_division_property_id", NpgsqlTypes.NpgsqlDbType.Integer)
                        { Value = division ?? (object)DBNull.Value }

                };
                var data = await _connection.OrganizationHierarchyDto.FromSqlRaw("SELECT * FROM get_hierarchy_by_property({0}, {1}, {2}, {3})", parameters).ToListAsync();

                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
