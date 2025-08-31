using ITC.InfoTrack.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Interface
{
    public interface IDashboard
    {
        Task<DashboardCardSummaryDto> getDashboardSummary(int dataValue);
    }
}
