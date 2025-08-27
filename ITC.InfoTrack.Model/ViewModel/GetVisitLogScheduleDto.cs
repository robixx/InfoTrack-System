using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.ViewModel
{
    public class GetVisitLogScheduleDto
    {
        public long ScheduleId { get; set; }      // bigint (assuming PK)
        public int AssignUserId { get; set; }     // int
        public string UserName { get; set; }      // text/varchar
        public DateTime DateOfVisit { get; set; } // date
        public TimeSpan TimeOfVisit { get; set; } // time
        public int TokenId { get; set; }          // int
        public string Comments { get; set; }      // text
        public string Priority { get; set; }      // text (if numeric, change to int)
        public string LocationText { get; set; }  // text
        public int TypeId { get; set; }           // int
        public int ValueTypeId { get; set; }      // int
        public string Address { get; set; }       // from COALESCE
    }
}
