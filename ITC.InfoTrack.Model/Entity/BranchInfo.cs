using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITC.InfoTrack.Model.Entity
{
    [Table("Branch_info")]
    public class BranchInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int BranchCode { get; set; }

        public int BBCode { get; set; }

        [MaxLength(100)]
        public string BranchName { get; set; }

        [MaxLength(100)]
        public string AgriBranches { get; set; }

        [MaxLength(100)]
        public string District { get; set; }

        [MaxLength(100)]
        public string Division { get; set; }

        [MaxLength(555)]
        public string Address { get; set; }

        public int IsActive { get; set; } = 1;

        public int IsBranch { get; set; } = 0;
    }
}
