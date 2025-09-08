using ITC.InfoTrack.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IWorker
    {
        Task<(string message, bool status)> SaveWorkerLogAsync(VisitLogInsertDto model, List<IFormFile> files, string LoginUserName);
        Task<(string message, bool status)> SaveWithoutScheduleLogAsync(VisitLogInsertDto model, List<IFormFile> files, string LoginUserName, int UserId);
        Task<List<DataMappingDto>> getDataMapAsync(int Id);
        Task<(string message, bool status)> SaveDataMapAsync(int divisionId, List<DataMappingDto> dto);
        Task<List<VisitLogGallarayDto>> GetGallaryDataAsync(int userId, int RoleId);
    }
}
