using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("Office_Booth")]
    public class OfficeBooth
    {
        public int BoothId { get; set; }
        public long CorpId { get; set; }
        public long BranchId { get; set; }
        public string BoothName { get; set; }
        public int PropertyId { get; set; }
        public int? BoothStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
