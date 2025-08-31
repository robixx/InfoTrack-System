using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class DashboardCardSummaryDto
    {
        
        public int TotalSchedule { get; set; }

       
        public int TotalVisit { get; set; }

        
        public int TotalPending { get; set; }

     
        public int TotalDone { get; set; }
    }
}
