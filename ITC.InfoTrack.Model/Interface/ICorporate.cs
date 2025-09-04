using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface ICorporate
    {
        Task<List<CorporateOffice>> getOfficeList();
        Task<(string Message, bool Status)> addOfficeInformation(CorporateOfficeDto model);
        Task<(string Message, bool Status)> SaveScheduleListAsync(List<ScheduleEntryDto> scheduleData);
        Task<CorporateOffice> OfficeDataFind(long corpId);
        Task<List<ScheduleDataDto>> ScheduleDataFind(long? branch, long? subbranchid, int? districtid, int? divisionid);
        Task<List<UserDto>> GetUserDateWise(string dateValue);
        Task<List<OrganizationHierarchyDto>> GetOrganizationHierarchyAsync(int? branchId, int? subbranch, int? district, int? division);
        Task<List<CalenderVisitScheduleDto>> GetCalenderVisitScheduleAsync();
        Task<List<GetVisitLogScheduleDto>> GetVisitLogScheduleAsync(int loginUser);
        Task<List<DataCollectionResultDto>> GetDataCollectionResultAsync();
        Task<(string Message, bool Status)> SavedatacollectionAsync( DataCollection modle, int userid);

        Task<VisitScheduleDetails> getSheduledata(int sehedularId);
 


    }
}
