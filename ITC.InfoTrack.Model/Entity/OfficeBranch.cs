using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("Office_Branch")]
    public class OfficeBranch
    {
        public long BranchId { get; set; }
        public long CorpId { get; set; }
        public string BranchName { get; set; }      
        public string BranchContactNumber { get; set; }
        public string BranchEmail { get; set; }
        public string BranchDivision { get; set; }
        public string BranchDistrict { get; set; }
        public string BranchThana { get; set; }        
        public int BranchStatus { get; set; }       
        public long PropertyId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
