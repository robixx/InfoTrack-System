using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class VisitLogInsertDto
    {
        public int ScheduleId { get; set; } = 0;       // default 0
        public int ResourceId { get; set; } = 0;       // default 0
        public int CreateBy { get; set; } = 0;     // default 0      
        public int SourceId { get; set; } = 0;     // default 0      
        public int Elementid { get; set; } = 0;     // default 0      
        public int DistrictId { get; set; } = 0;     // default 0      
        public string Comments { get; set; }
    }
}
