using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("Corporate_Office")]
    public class CorporateOffice
    {
        [Key]
        public long CorpId { get; set; }
        public string CorpName { get; set; }
        public string CorpAddress { get; set; }
        public string CorpContactNumber { get; set; }
        public string CorpEmail { get; set; }
        public string CorpType { get; set; }
        public int CorpStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; } = 0;
        public DateTime? UpdatedDate { get; set; }
    }
}
