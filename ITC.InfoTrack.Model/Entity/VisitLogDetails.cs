using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("VisitLog_Details")]
    public class VisitLogDetails
    {
        [Key]
        public int VisitLogDetailsId { get; set; }   // Serial / Primary Key
        public int VisitLogId { get; set; }         // Foreign key to VisitLog
        public int AssignedLog { get; set; } = 0;    // Default 0
        public string ImageName { get; set; }       // varchar
    }
}
