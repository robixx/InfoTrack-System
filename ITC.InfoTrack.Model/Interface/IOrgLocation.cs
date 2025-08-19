using ITC.InfoTrack.Model.Entity;
using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IOrgLocation
    {
        Task<(string message, bool status)> SaveOrgLocation(InsertOrgDto model);
        Task<List<LocationShowDto>> getOrgLocation();
    }
}
