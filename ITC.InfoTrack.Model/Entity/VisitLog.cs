using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class VisitLog
    {
        [Key]
        public int VisitLogId { get; set; }            // corresponds to VisitLogId (PK)
        public int ScheduleId { get; set; } = 0;       // default 0
        public int ResourceId { get; set; } = 0;       // default 0
        public int AssignedLog { get; set; } = 0;      // default 0
        public DateTime? AssignDateTime { get; set; }  // timestamp
        public TimeSpan? VisitTime { get; set; }       // time
        public TimeSpan? CheckOutTime { get; set; }    // time
        public DateTime? CreateDate { get; set; }      // timestamp
        public string? Comments { get; set; }          // varchar
    }
}
