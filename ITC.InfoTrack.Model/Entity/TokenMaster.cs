using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    public class TokenMaster
    {
        [Key]
        public int TokenId { get; set; }
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public int TypeId { get; set; }
        public int SourceId { get; set; }
        public int? CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
