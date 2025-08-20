using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class CalenderVisitScheduleDto
    {
        public int ScheduleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime DateOfVisit { get; set; }
        public TimeSpan TimeOfVisit { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string SubBranch { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public string DivisionName { get; set; } = string.Empty;
        public string BootName { get; set; } = string.Empty;
        public string LocationText { get; set; } = string.Empty;
        public string CommentValue { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
    }
}
