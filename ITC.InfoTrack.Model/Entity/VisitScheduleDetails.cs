using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("VisitSchedule_Details")]
    public class VisitScheduleDetails
    {
        [Key]
        public long VisitScheduleDetailsId { get; set; }
        public long VisitId { get; set; }
        public long PoolId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? BranchId { get; set; }
        public int? SubBranchId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }
        public int? BoothId { get; set; }
        public string Comments { get; set; }
        public string LocationText { get; set; }
        public string Priority { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
