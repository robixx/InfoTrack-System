using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class VisitLogGallarayDto
    {
        public int VisitLogId { get; set; }
        public int ScheduleId { get; set; }
        public string Comments { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageName { get; set; }
        public int TypeId { get; set; }
        public int ValueTypeId { get; set; }
        public string SourceName { get; set; }     // From vt.MetaElementValue
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }   // From div.MetaElementValue
    }
}
