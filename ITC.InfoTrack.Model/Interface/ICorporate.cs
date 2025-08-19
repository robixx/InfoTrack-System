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
        Task<CorporateOffice> OfficeDataFind(long corpId);
        Task<List<ScheduleDataDto>> ScheduleDataFind(long? branch, long? subbranchid, int? districtid, int? divisionid);
        Task<List<UserDto>> GetUserDateWise(string dateValue);

    }
}
